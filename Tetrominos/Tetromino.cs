using System;
using System.Collections.Generic;
using System.Linq;
using tetblaris.Models;
using tetblaris.Models.Enums;

namespace tetblaris.Tetrominos
{
    /// <summary>
    /// Base Tetromino class
    /// </summary>
    public abstract class Tetromino : ITetromino
    {
        /// <summary>
        /// The current orientation of this tetromino. 
        /// Tetrominos rotate about their center.
        /// </summary>
        public TetrominoOrientation Orientation { get; set; }

        /// <summary>
        /// The X-coordinate of the center piece.
        /// </summary>
        public int CenterPieceRow { get; set; }

        /// <summary>
        /// The Y-coordinate of the center piece.
        /// </summary>
        public int CenterPieceColumn { get; set; }

        /// <summary>
        /// A collection of all spaces currently occupied by this tetromino.
        /// This collection is calculated by each style.
        /// </summary>
        public abstract List<IGameBoardCell> CoveredCells { get; }

        /// <summary>
        /// The CSS class that is unique to this style of tetromino.
        /// </summary>
        public abstract string CssClass { get; }

        /// <summary>
        /// The style of this tetromino 
        /// e.g. Straight, Block, T-Shaped, etc.
        /// </summary>
        public abstract TetrominoStyle Style { get; }


        /// <summary>
        /// Reference to a GameBoard
        /// </summary>
        /// <value></value>
        IGameBoard _GameBoard { get; set; }


        /// <summary>
        /// Create an instance of a Tetromino
        /// </summary>
        /// <param name="gameBoard">gameboard where this tetromino belongs to</param>
        public Tetromino(IGameBoard gameBoard)
        {
            this._GameBoard = gameBoard;
            //place the tetramino in the top row/centered
            this.CenterPieceRow = this._GameBoard.Rows - 1;
            this.CenterPieceColumn = this._GameBoard.Cols / 2 - 1;
            Console.WriteLine($"Tetromino start from [{this.CenterPieceRow},{this.CenterPieceColumn}]");
        }

        /// <summary>
        /// Rotate the tetromino over it's center piece
        /// </summary>
        public void Rotate()
        {
            //change the orientation
            switch (Orientation)
            {
                case TetrominoOrientation.UpDown:
                    Orientation = TetrominoOrientation.RightLeft;
                    break;

                case TetrominoOrientation.RightLeft:
                    Orientation = TetrominoOrientation.DownUp;
                    break;

                case TetrominoOrientation.DownUp:
                    Orientation = TetrominoOrientation.LeftRight;
                    break;

                case TetrominoOrientation.LeftRight:
                    Orientation = TetrominoOrientation.UpDown;
                    break;
            }
        }

        /// <summary>
        /// Move the tetromino to the left by 1 cell
        /// </summary>
        public void MoveLeft()
        {
            if (CanMoveLeft())
            {
                CenterPieceColumn--;
            }
        }

        /// <summary>
        /// Move the tetromino to the right by 1 cell
        /// </summary>
        public void MoveRight()
        {
            if (CanMoveRight())
            {
                CenterPieceColumn++;
            }
        }

        /// <summary>
        /// Move the tetromino to down by 1 cell
        /// </summary>
        public void MoveDown()
        {
            if (CanMoveDown())
            {
                CenterPieceRow--;
                Console.WriteLine("Moving down " + CenterPieceRow);
            }
        }

        /// <summary>
        /// Drop the tetromino until it can't move down
        /// </summary>
        /// <returns></returns>
        public int Drop()
        {
            int scoreCounter = 0;
            while (CanMoveDown())
            {
                MoveDown();
                scoreCounter++;
            }
            return scoreCounter;
        }

        /// <summary>
        /// Check whether the tetromino can move down by 1 cell or not
        /// </summary>
        /// <returns>true if it can move, false otherwise</returns>
        public bool CanMoveDown()
        {
            //get the min row of the tetromino
            int minRow = CoveredCells.Min(_ => _.Row);
            //If any of the covered spaces are currently in the lowest row, the piece cannot move down.
            Console.WriteLine("CanMoveDown - minRow=" + minRow);
            if (minRow == 0)
            {
                return false;
            }
            //get all the lowest tetromino's cells
            var lowestCells = CoveredCells.Where(_ => _.Row == minRow);

            //For each of the covered spaces, get the space immediately below
            foreach (var lowestCell in lowestCells)
            {
                if (_GameBoard.GetRow(lowestCell.Row - 1).HasCellTaken(lowestCell.Column))
                {
                    return false;
                }
            }
            return true;
        }

        public bool CanMoveRight()
        {
            //get the max col of the tetromino
            int maxCol = CoveredCells.Max(_ => _.Column);
            Console.WriteLine("CanMoveRight - maxCol=" + maxCol);
            //If any of the covered spaces are currently in the right border, the piece cannot move right.
            if (maxCol == _GameBoard.Cols - 1)
            {
                return false;
            }
            //get all the righest tetromino's cells
            var righestCells = CoveredCells.Where(_ => _.Column == maxCol);
            //For each of the covered spaces, get the space immediately below
            foreach (var rightestCell in righestCells)
            {
                if (_GameBoard.GetRow(rightestCell.Row).HasCellTaken(rightestCell.Column + 1))
                {
                    return false;
                }
            }
            return true;
        }

        public bool CanMoveLeft()
        {
            //get the min col of the tetromino
            int minCol = CoveredCells.Min(_ => _.Column);
            //If any of the covered spaces are currently in the left border, the piece cannot move left.
            if (minCol == 0)
            {
                return false;
            }
            //get all the leftest tetromino's cells
            var leftestCells = CoveredCells.Where(_ => _.Column == minCol);
            //For each of the covered spaces, get the space immediately below
            foreach (var leftestCell in leftestCells)
            {
                if (_GameBoard.GetRow(leftestCell.Row).HasCellTaken(leftestCell.Column - 1))
                {
                    return false;
                }
            }
            return true;
        }

        public bool CanMove()
        {
            return CanMoveDown() || CanMoveLeft() || CanMoveRight();
        }
    }
}