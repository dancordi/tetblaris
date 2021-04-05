using System.Collections.Generic;

namespace tetblaris.Models
{
    public interface IGameBoard
    {
        int Cols { get; }
        int Rows { get; }
        bool HasAnyTetrominoInRow(int row);
        int ClearCompletedRows();
        IGameBoardRow GetRow(int i);
        void TakeCell(IGameBoardCell cell);
        void TakeCells(List<IGameBoardCell> cells);
    }
}