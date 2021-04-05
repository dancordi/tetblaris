namespace tetblaris.Models
{
    public class GameBoardCell : IGameBoardCell
    {
        public int Row { get; private set; }
        public int Column { get; private set; }
        public bool IsTaken { get; private set; }
        public string CssClass { get; private set; }

        public GameBoardCell(int row, int column) : this(row, column, string.Empty)
        {
            Row = row;
            Column = column;
        }

        public GameBoardCell(int row, int column, string css)
        {
            Row = row;
            Column = column;
            CssClass = css;
            IsTaken = false;
        }

        public void Take(string cssClass)
        {
            this.IsTaken = true;
            this.CssClass = cssClass;
        }

        public void ChangeRow(int newRow)
        {
            this.Row = newRow;
        }
    }
}