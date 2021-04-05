using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using tetblaris.Features;

namespace tetblaris.Pages
{
    public partial class Tetris : ComponentBase
    {

        [Inject]
        IJSRuntime _jsRuntime { get; set; }

        // set by the @ref attribute
        protected ElementReference gameBoardDiv;

        protected bool playAudio { get; set; }

        IGame Game { get; set; }

        public Tetris()
        {
            //create a new game
            Game = new Game();

        }
        protected override async Task OnInitializedAsync()
        {
            //subscribe to property changed
            Game.PropertyChanged += (o, e) => StateHasChanged();
        }


        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                //play the sound
                await ToggleAudio();

                //focus on the gameBoardDiv
                await _jsRuntime.InvokeVoidAsync("SetFocusToElement", gameBoardDiv);

            }
        }

        protected async Task KeyDown(KeyboardEventArgs e)
        {
            bool isStateChanged = false;

            switch (e.Key.ToLower())
            {
                case "s":
                    if (this.Game.State != tetblaris.Models.Enums.GameState.Playing)
                    {
                        await this.Game.Start();
                    }
                    break;
                case "p":
                    if (this.Game.State == tetblaris.Models.Enums.GameState.Playing)
                    {
                        this.Game.Pause();
                    }
                    break;
                case "m":
                    await ToggleAudio();
                    break;
                case "arrowright":
                    this.Game.CurrentTetromino.MoveRight();
                    isStateChanged = true;
                    break;
                case "arrowleft":
                    this.Game.CurrentTetromino.MoveLeft();
                    isStateChanged = true;
                    break;
                case "arrowdown":
                case " ":
                    int addlScore = this.Game.CurrentTetromino.Drop();
                    this.Game.SkipDelay = true;
                    isStateChanged = true;
                    break;
                case "arrowup":
                    this.Game.CurrentTetromino.Rotate();
                    isStateChanged = true;
                    break;
                default:
                    break;
            }
            if (isStateChanged)
            {
                StateHasChanged();
            }
        }

        protected async Task ToggleAudio()
        {
            playAudio = !playAudio;

            if (playAudio)
                await _jsRuntime.InvokeAsync<string>("PlayAudio", "tetris_theme");
            else
                await _jsRuntime.InvokeAsync<string>("PauseAudio", "tetris_theme");

            //Focus the browser on the board div
            await _jsRuntime.InvokeVoidAsync("SetFocusToElement", gameBoardDiv);
        }
    }
}