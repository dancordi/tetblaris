using System.Collections.Generic;
using System.Linq;
using tetblaris.Components;

namespace tetblaris.Models
{
    public class GameBoard : IGameBoard
    {
        /// <summary>
        /// number of board's columns
        /// </summary>
        /// <value></value>
        public int Cols { get; }

        /// <summary>
        /// number of board's rows
        /// </summary>
        /// <value></value>
        public int Rows { get; }

        /// <summary>
        /// Gameboard's rows
        /// </summary>
        private readonly List<IGameBoardRow> GameBoardRows;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rows">number of rows of the board</param>
        /// <param name="cols">number of columns of the board</param>
        public GameBoard(int rows, int cols)
        {
            this.Rows = rows;
            this.Cols = cols;
            //create the GameBoardRows 
            GameBoardRows = new List<IGameBoardRow>();
            for (int i = 0; i < this.Rows; i++)
            {
                GameBoardRows.Add(new GameBoardRow(i, this.Cols));
            }
        }

        /// <summary>
        /// Default ctor 20x10
        /// </summary>
        public GameBoard() : this(20, 10)
        {

        }

        /// <summary>
        /// Whether a tetromino has a piece in a given row
        /// </summary>
        /// <param name="row">row where to find any tetromino's piece</param>
        /// <returns></returns>
        public bool HasAnyTetrominoInRow(int row)
        {
            if (row < 0 || row >= this.Rows)
            {
                return false;
            }
            if (GameBoardRows[row].HasAnyCellTaken())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Clears all the completed rows
        /// </summary>
        /// <returns>number of completed rows</returns>
        public int ClearCompletedRows()
        {
            int completedRows = 0;
            //For each row
            for (int i = 0; i < GameBoardRows.Count; i++)
            {
                var row = GameBoardRows[i];

                if (row.HasAllCellsTaken())
                {
                    //grid.Cells.SetCssClass(i, "tetris-clear-row");
                    CompleteRow(i);
                    //stay on the same row because 
                    //the row has been removed
                    i--;
                    completedRows++;
                }
            }
            return completedRows;
        }


        /// <summary>
        /// Complete a row and shift down by 1 the following rows
        /// </summary>
        /// <param name="row">row to compleate</param>
        internal void CompleteRow(int row)
        {
            if (row >= 0 && row < this.Rows)
            {
                //remove complete row
                GameBoardRows.RemoveAt(row);
                //change the following row number
                for (int r = row; r < this.Rows - 1; r++)
                {
                    GameBoardRows[r].ChangeRow(r);
                }
                //create the new top row
                GameBoardRows.Add(new GameBoardRow(this.Rows - 1, this.Cols));
            }
        }

        /// <summary>
        /// Get the row by its row number
        /// </summary>
        /// <param name="rowNumber">row number</param>
        /// <returns></returns>
        public IGameBoardRow GetRow(int rowNumber)
        {
            if (rowNumber >= 0 && rowNumber < this.Rows)
            {
                return GameBoardRows[rowNumber];
            }
            return null;
        }

        public void TakeCell(IGameBoardCell cell)
        {
            var row = GetRow(cell.Row);
            if (row == null)
            {
                return;
            }
            row.Cells[cell.Column].Take(cell.CssClass);
        }

        public void TakeCells(List<IGameBoardCell> cells)
        {
            foreach (var cell in cells)
            {
                TakeCell(cell);
            }
        }
    }
}