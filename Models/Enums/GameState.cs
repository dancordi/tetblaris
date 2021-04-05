namespace tetblaris.Models.Enums
{
    /// <summary>
    /// Represents the current state of the Tetris game
    /// </summary>
    internal enum GameState
    {
        NotStarted, 
        Playing, //Game playing normally
        Paused,
        GameOver
    }
}