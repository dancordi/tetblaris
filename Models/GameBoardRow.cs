using System.Linq;

namespace tetblaris.Models
{
    public partial class GameBoardRow : IGameBoardRow
    {
        public int Row { get; private set; }
        public int Cols { get; private set; }
        public IGameBoardCell[] Cells { get { return _Cells; } }

        private readonly IGameBoardCell[] _Cells;

        public GameBoardRow(int row, int cols)
        {
            this.Row = row;
            this.Cols = cols;
            this._Cells = new GameBoardCell[cols];
            for (int i = 0; i < cols; i++)
            {
                this._Cells[i] = new GameBoardCell(row, i);
            }
        }

        public void ChangeRow(int row)
        {
            //change the row's cells' row
            for (int i = 0; i < this.Cols; i++)
            {
                this._Cells[i].ChangeRow(row);
            }
        }

        public bool HasCellTaken(int col)
        {
            if (col >= 0 && col < _Cells.Length)
            {
                return _Cells[col].IsTaken;
            }
            return false;
        }

        public bool HasAnyCellTaken()
        {
            return _Cells.Any(_ => _.IsTaken);
        }

        public bool HasAllCellsTaken()
        {
            return (_Cells.Where(_ => _.IsTaken).Count() == _Cells.Count());
        }
    }
}