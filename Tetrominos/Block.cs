using System.Collections.Generic;
using tetblaris.Models;
using tetblaris.Models.Enums;

namespace tetblaris.Tetrominos
{
    /// <summary>
    /// A square or "block" tetromino. Block tetrominos do not rotate.
    /// X X
    /// X X
    /// </summary>
    public class Block : Tetromino
    {

        public Block(IGameBoard gameBoard) : base(gameBoard)
        {
            this.Orientation = Models.Enums.TetrominoOrientation.LeftRight;
        }

        public override string CssClass { get { return "tetris-yellow-cell"; } }

        public override TetrominoStyle Style { get { return Models.Enums.TetrominoStyle.Block; } }

        /// <summary>
        /// the covered cells near the centerpiece for this tetromino 
        /// </summary>
        /// <value></value>
        public override List<IGameBoardCell> CoveredCells
        {
            get
            {
                var coveredCells = new List<IGameBoardCell>();
                coveredCells.Add(new GameBoardCell(this.CenterPieceRow, this.CenterPieceColumn, CssClass));
                coveredCells.Add(new GameBoardCell(this.CenterPieceRow - 1, this.CenterPieceColumn, CssClass));
                coveredCells.Add(new GameBoardCell(this.CenterPieceRow, this.CenterPieceColumn + 1, CssClass));
                coveredCells.Add(new GameBoardCell(this.CenterPieceRow - 1, this.CenterPieceColumn + 1, CssClass));
                return coveredCells;
            }
        }
    }
}