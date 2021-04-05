using System.Collections.Generic;
using tetblaris.Models;
using tetblaris.Models.Enums;

namespace tetblaris.Tetrominos
{
    public interface ITetromino
    {
        TetrominoStyle Style { get; }
        List<IGameBoardCell> CoveredCells { get; }
        string CssClass { get; }
        TetrominoOrientation Orientation { get; set; }

        void Rotate();

        void MoveLeft();
        void MoveRight();
        void MoveDown();

        int Drop();

        bool CanMoveDown();

        bool CanMoveRight();
        bool CanMoveLeft();

        bool CanMove();

    }
}