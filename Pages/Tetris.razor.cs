using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace tetblaris.Pages
{
    public partial class Tetris : ComponentBase
    {
        
        [Inject]
        IJSRuntime _jsRuntime {get;set;}

        protected ElementReference gameBoardDiv;  // set by the @ref attribute
        
        protected bool playAudio {get; set;}
        
        protected override async Task OnInitializedAsync()
        {   
            
        }


        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                //play the sound
                await ToggleAudio();

                await _jsRuntime.InvokeVoidAsync("SetFocusToElement", gameBoardDiv);

            }
        }

        protected async Task KeyDown(KeyboardEventArgs e)
        {
            if(e.Key == "m")
            {
                await ToggleAudio();
            }
            StateHasChanged();
        }

        protected async Task ToggleAudio()
        {
            playAudio = !playAudio;

            if(playAudio)
                await _jsRuntime.InvokeAsync<string>("PlayAudio", "tetris_theme");
            else
                await _jsRuntime.InvokeAsync<string>("PauseAudio", "tetris_theme");

            //Focus the browser on the board div
            await _jsRuntime.InvokeVoidAsync("SetFocusToElement", gameBoardDiv);
        }
    }
}