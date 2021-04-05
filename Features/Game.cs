using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using tetblaris.Models;
using tetblaris.Models.Enums;
using tetblaris.Tetrominos;

namespace tetblaris.Features
{
    internal class Game : IGame
    {
        /// <summary>
        /// Game state
        /// </summary>
        /// <value></value>
        public GameState State { get; set; }

        /// <summary>
        /// Gameboard
        /// </summary>
        /// <value></value>
        public IGameBoard GameBoard { get; }

        //The standard delay is how long the game waits before dropping the current tetromino by one row.
        int standardDelay = 1000;

        //This flag is set if the player "hard drops" a tetromino all the way to the bottom
        public bool SkipDelay { get; set; } = false;

        public ITetromino CurrentTetromino { get; internal set; }

        //Represents the next three tetromino styles.
        //The actual tetrominos will be created only when they become the current tetromino.
        public TetrominoStyle NextStyle { get; internal set; }
        public TetrominoStyle SecondNextStyle { get; internal set; }
        public TetrominoStyle ThirdNextStyle { get; internal set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public Game()
        {
            this.State = GameState.NotStarted;
            //create a new gameboard
            GameBoard = new GameBoard();
        }

        public TetrominoStyle GenerateNextTetraminoStyle(params TetrominoStyle[] unusableStyles)
        {
            Random rand = new Random(DateTime.Now.Millisecond);

            //Randomly generate one of the eight possible tetrominos
            var style = Randomize(rand);

            //Re-generate the new tetromino until 
            //its style is not one of the upcoming styles.
            while (unusableStyles.Contains(style))
                style = Randomize(rand);

            return style;
        }

        public async Task Start()
        {
            //set the state to playing
            this.State = GameState.Playing;

            Console.WriteLine("game start");

            //Generate the styles of the first three tetrominos that will be dropped
            NextStyle = NextTetrominoStyle();
            SecondNextStyle = NextTetrominoStyle(NextStyle);
            ThirdNextStyle = NextTetrominoStyle(NextStyle, SecondNextStyle);

            //game loop
            while (this.State != GameState.GameOver)
            {
                if (this.State == GameState.Playing)
                {
                    //Create the next tetromino to be dropped from the already-determined nextStyle,
                    //and move the styles "up" in line
                    CurrentTetromino = CreateFromStyle(NextStyle);

                    Console.WriteLine("CurrentTetromino=" + CurrentTetromino.Style);

                    NextStyle = SecondNextStyle;
                    SecondNextStyle = ThirdNextStyle;
                    ThirdNextStyle = NextTetrominoStyle(CurrentTetromino.Style, NextStyle, SecondNextStyle);

                    //Update the display
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Game)));

                    //Run the current tetromino until it can't move anymore
                    await RunCurrentTetromino();

                    //If any rows are filled, remove them from the board
                    await ClearCompleteRows();

                    //If the score is high enough, move the user to a new level
                    //LevelChange();

                    //evaluate the gameover condition
                    if (GameBoard.HasAnyTetrominoInRow(GameBoard.Rows))
                    {
                        this.State = GameState.GameOver;
                    }
                }

                //delay
                await Delay(150);
            }
        }

        public void Pause()
        {
            this.State = GameState.Paused;
        }

        internal TetrominoStyle Randomize(Random random)
        {
            int min = 1; int max = 8;
            return (TetrominoStyle)random.Next(min, max);
        }

        internal TetrominoStyle NextTetrominoStyle(params TetrominoStyle[] unusableStyles)
        {
            Random rand = new Random(DateTime.Now.Millisecond);

            //Randomly generate one of the eight possible tetrominos
            var style = Randomize(rand);

            //Re-generate the new tetromino until it is of a style that is not one of the upcoming styles.
            while (unusableStyles.Contains(style))
                style = Randomize(rand);

            return style;
        }

        internal ITetromino CreateFromStyle(TetrominoStyle style)
        {
            switch (style)
            {
                case TetrominoStyle.Straight:
                    return new Straight(this.GameBoard);
                // case TetrominoStyle.TShaped:
                //     return null;
                // case TetrominoStyle.LeftZigZag:
                //     return null;
                // case TetrominoStyle.RightZigZag:
                //     return null;
                // case TetrominoStyle.LShaped:
                //     return null;
                // case TetrominoStyle.ReverseLShaped:
                //     return null;
                case TetrominoStyle.Block:
                default:
                    return new Block(this.GameBoard);
            }
            // return style switch
            // {
            //     TetrominoStyle.Block => new Block(),
            //     TetrominoStyle.Straight => new Straight(grid),
            //     TetrominoStyle.TShaped => new TShaped(grid),
            //     TetrominoStyle.LeftZigZag => new LeftZigZag(grid),
            //     TetrominoStyle.RightZigZag => new RightZigZag(grid),
            //     TetrominoStyle.LShaped => new LShaped(grid),
            //     TetrominoStyle.ReverseLShaped => new ReverseLShaped(grid),
            //     _ => new Block(),
            // };
        }




        //Delays the game up to the passed-in amount of milliseconds in 50 millisecond intervals
        internal async Task Delay(int millis)
        {
            int totalDelay = 0;
            while (totalDelay < millis && !SkipDelay)
            {
                totalDelay += 50;
                await Task.Delay(50);
            }
            SkipDelay = false;
        }

        public async Task RunCurrentTetromino()
        {
            //While the tetromino can still move down
            while (CurrentTetromino.CanMoveDown())
            {
                //Wait for the standard delay
                await Delay(standardDelay);

                //Move the tetromino down one row
                CurrentTetromino.MoveDown();

                //Update the display
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Game)));

                //If the tetromino can no longer move down BUT can still move in other directions,
                //delay for an additional half-second to let the user move if they want.
                if (!CurrentTetromino.CanMoveDown() && CurrentTetromino.CanMove())
                    await Delay(500);
            }

            //"Solidify" the current tetromino by adding its covered squares to the board's cells
            this.GameBoard.TakeCells(CurrentTetromino.CoveredCells);
        }

        public async Task ClearCompleteRows()
        {
            int completedRows = this.GameBoard.ClearCompletedRows();

            //If there are any complete rows
            if (completedRows > 0)
            {
                //Update the display
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Game)));

                //Calculate the score for the completed row(s)
                // switch (completedRows)
                // {
                //     case 1:
                //         score += 40 * level;
                //         break;

                //     case 2:
                //         score += 100 * level;
                //         break;

                //     case 3:
                //         score += 300 * level;
                //         break;

                //     case 4:
                //         score += 1200 * level;
                //         break;
                // }

                await Task.Delay(1000);
            }
        }
    }
}