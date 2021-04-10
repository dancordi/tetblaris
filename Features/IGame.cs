using System.ComponentModel;
using System.Threading.Tasks;
using tetblaris.Models;
using tetblaris.Models.Enums;
using tetblaris.Tetrominos;

namespace tetblaris.Features
{
    internal interface IGame : INotifyPropertyChanged
    {

        GameState State { get; set; }
        IGameBoard GameBoard { get; }
        ITetromino CurrentTetromino { get; }
        TetrominoStyle NextStyle { get; }
        TetrominoStyle SecondNextStyle { get; }
        TetrominoStyle ThirdNextStyle { get; }
        bool SkipDelay { get; set; }
        Task Start();
        void Pause();
        void Resume();

        TetrominoStyle GenerateNextTetraminoStyle(params TetrominoStyle[] unusableStyles);
    }
}