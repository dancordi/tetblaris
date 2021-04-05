using System.Collections.Generic;
using tetblaris.Models;
using tetblaris.Models.Enums;

namespace tetblaris.Tetrominos
{
    /// <summary>
    /// A reverse-L-shaped tetromino, also called a J-shaped tetromino
    /// X        X X    X X X      X
    /// X X X    X          X      X
    ///          X               X X
    /// </summary>
    public class ReverseLShaped : Tetromino
    {
        public ReverseLShaped(IGameBoard gameBoard) : base(gameBoard) { }

        public override TetrominoStyle Style => TetrominoStyle.ReverseLShaped;

        public override string CssClass => "tetris-darkblue-cell";

        public override List<IGameBoardCell> CoveredCells
        {
            get
            {
                var cells = new List<IGameBoardCell>();
                cells.Add(new GameBoardCell(CenterPieceRow, CenterPieceColumn, CssClass));

                switch (Orientation)
                {
                    case TetrominoOrientation.LeftRight:
                        cells.Add(new GameBoardCell(CenterPieceRow, CenterPieceColumn + 1, CssClass));
                        cells.Add(new GameBoardCell(CenterPieceRow, CenterPieceColumn + 2, CssClass));
                        cells.Add(new GameBoardCell(CenterPieceRow + 1, CenterPieceColumn, CssClass));
                        break;

                    case TetrominoOrientation.DownUp:
                        cells.Add(new GameBoardCell(CenterPieceRow, CenterPieceColumn + 1, CssClass));
                        cells.Add(new GameBoardCell(CenterPieceRow - 1, CenterPieceColumn, CssClass));
                        cells.Add(new GameBoardCell(CenterPieceRow - 2, CenterPieceColumn, CssClass));
                        break;

                    case TetrominoOrientation.RightLeft:
                        cells.Add(new GameBoardCell(CenterPieceRow, CenterPieceColumn - 1, CssClass));
                        cells.Add(new GameBoardCell(CenterPieceRow, CenterPieceColumn - 2, CssClass));
                        cells.Add(new GameBoardCell(CenterPieceRow - 1, CenterPieceColumn, CssClass));
                        break;

                    case TetrominoOrientation.UpDown:
                        cells.Add(new GameBoardCell(CenterPieceRow, CenterPieceColumn - 1, CssClass));
                        cells.Add(new GameBoardCell(CenterPieceRow + 1, CenterPieceColumn, CssClass));
                        cells.Add(new GameBoardCell(CenterPieceRow + 2, CenterPieceColumn, CssClass));
                        break;
                }

                return cells;
            }
        }
    }
}