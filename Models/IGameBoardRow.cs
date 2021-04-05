namespace tetblaris.Models
{
    public interface IGameBoardRow
    {
        int Row { get; }
        int Cols { get; }
        IGameBoardCell[] Cells { get; }

        void ChangeRow(int row);
        bool HasCellTaken(int col);
        bool HasAnyCellTaken();
        bool HasAllCellsTaken();
    }
}