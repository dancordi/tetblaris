namespace tetblaris.Models
{
    public interface IGameBoardCell
    {
        int Row { get; }
        int Column { get; }
        bool IsTaken { get; }
        string CssClass { get; }
        void ChangeRow(int newRow);
        void Take(string cssClass);
    }
}