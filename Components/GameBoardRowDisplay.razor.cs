using Microsoft.AspNetCore.Components;
using tetblaris.Models;
using tetblaris.Tetrominos;

namespace tetblaris.Components
{
    public partial class GameBoardRowDisplay : ComponentBase
    {
        [Parameter]
        public IGameBoardRow Row { get; set; }

        [Parameter]
        public ITetromino Tetromino { get; set; }


    }
}