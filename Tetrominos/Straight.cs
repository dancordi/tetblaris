using System.Collections.Generic;
using tetblaris.Models;
using tetblaris.Models.Enums;

namespace tetblaris.Tetrominos
{
    /// <summary>
    /// A straight-line tetromino
    /// X X X X    X
    ///            X
    ///            X
    ///            X
    /// </summary>
    public class Straight : Tetromino
    {
        public Straight(IGameBoard gameBoard) : base(gameBoard)
        {
        }

        public override List<IGameBoardCell> CoveredCells
        {
            get
            {
                var cells = new List<IGameBoardCell>();
                cells.Add(new GameBoardCell(this.CenterPieceRow, this.CenterPieceColumn, CssClass));
                switch (Orientation)
                {
                    case TetrominoOrientation.LeftRight:
                        cells.Add(new GameBoardCell(this.CenterPieceRow, this.CenterPieceColumn - 1, CssClass));
                        cells.Add(new GameBoardCell(this.CenterPieceRow, this.CenterPieceColumn - 1, CssClass));
                        cells.Add(new GameBoardCell(this.CenterPieceRow, this.CenterPieceColumn + 1, CssClass));
                        break;
                    case TetrominoOrientation.RightLeft:
                        cells.Add(new GameBoardCell(this.CenterPieceRow, this.CenterPieceColumn - 1, CssClass));
                        cells.Add(new GameBoardCell(this.CenterPieceRow, this.CenterPieceColumn + 1, CssClass));
                        cells.Add(new GameBoardCell(this.CenterPieceRow, this.CenterPieceColumn + 2, CssClass));
                        break;
                    case TetrominoOrientation.UpDown:
                        cells.Add(new GameBoardCell(this.CenterPieceRow - 1, this.CenterPieceColumn, CssClass));
                        cells.Add(new GameBoardCell(this.CenterPieceRow - 2, this.CenterPieceColumn, CssClass));
                        cells.Add(new GameBoardCell(this.CenterPieceRow + 1, this.CenterPieceColumn, CssClass));
                        break;
                    case TetrominoOrientation.DownUp:
                        cells.Add(new GameBoardCell(this.CenterPieceRow - 1, this.CenterPieceColumn, CssClass));
                        cells.Add(new GameBoardCell(this.CenterPieceRow + 1, this.CenterPieceColumn, CssClass));
                        cells.Add(new GameBoardCell(this.CenterPieceRow + 2, this.CenterPieceColumn, CssClass));
                        break;
                }
                return cells;
            }
        }

        public override string CssClass { get { return "tetris-lightblue-cell"; } }

        public override TetrominoStyle Style { get { return TetrominoStyle.Straight; } }
    }
}