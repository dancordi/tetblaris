using System.Collections.Generic;
using tetblaris.Models;
using tetblaris.Models.Enums;

namespace tetblaris.Tetrominos
{
    /// <summary>
    /// A right-zig-zag tetromino
    ///   X X    X
    /// X X      X X
    ///            X
    /// </summary>
    public class RightZigZag : Tetromino
    {
        public RightZigZag(IGameBoard gameBoard) : base(gameBoard) { }

        public override TetrominoStyle Style => TetrominoStyle.RightZigZag;

        public override string CssClass => "tetris-green-cell";

        public override List<IGameBoardCell> CoveredCells
        {
            get
            {
                var cells = new List<IGameBoardCell>();
                cells.Add(new GameBoardCell(CenterPieceRow, CenterPieceColumn, CssClass));
                switch (Orientation)
                {
                    case TetrominoOrientation.LeftRight:
                        cells.Add(new GameBoardCell(CenterPieceRow, CenterPieceColumn - 1, CssClass));
                        cells.Add(new GameBoardCell(CenterPieceRow + 1, CenterPieceColumn, CssClass));
                        cells.Add(new GameBoardCell(CenterPieceRow + 1, CenterPieceColumn + 1, CssClass));
                        break;

                    case TetrominoOrientation.DownUp:
                        cells.Add(new GameBoardCell(CenterPieceRow, CenterPieceColumn + 1, CssClass));
                        cells.Add(new GameBoardCell(CenterPieceRow + 1, CenterPieceColumn, CssClass));
                        cells.Add(new GameBoardCell(CenterPieceRow - 1, CenterPieceColumn + 1, CssClass));
                        break;

                    case TetrominoOrientation.RightLeft:
                        cells.Add(new GameBoardCell(CenterPieceRow, CenterPieceColumn + 1, CssClass));
                        cells.Add(new GameBoardCell(CenterPieceRow - 1, CenterPieceColumn, CssClass));
                        cells.Add(new GameBoardCell(CenterPieceRow - 1, CenterPieceColumn - 1, CssClass));
                        break;

                    case TetrominoOrientation.UpDown:
                        cells.Add(new GameBoardCell(CenterPieceRow, CenterPieceColumn - 1, CssClass));
                        cells.Add(new GameBoardCell(CenterPieceRow - 1, CenterPieceColumn, CssClass));
                        cells.Add(new GameBoardCell(CenterPieceRow + 1, CenterPieceColumn - 1, CssClass));
                        break;
                }
                return cells;
            }
        }
    }
}