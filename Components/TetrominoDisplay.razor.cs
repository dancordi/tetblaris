using Microsoft.AspNetCore.Components;
using tetblaris.Models.Enums;
using System;
namespace tetblaris.Components
{
    public partial class TetrominoDisplay : ComponentBase
    {
        [Parameter]
        public TetrominoStyle Style { get; set; }

        public TetrominoDisplay()
        {

        }

        string imgURL
        {
            get
            {
                string s = "../images/";
                switch (Style)
                {
                    case TetrominoStyle.Block:
                        s += "tetromino-block.png";
                        break;

                    case TetrominoStyle.Straight:
                        s += "tetromino-straight.png";
                        break;

                    case TetrominoStyle.TShaped:
                        s += "tetromino-tshaped.png";
                        break;

                    case TetrominoStyle.LeftZigZag:
                        s += "tetromino-leftzigzag.png";
                        break;

                    case TetrominoStyle.RightZigZag:
                        s += "tetromino-rightzigzag.png";
                        break;

                    case TetrominoStyle.LShaped:
                        s += "tetromino-lshaped.png";
                        break;

                    case TetrominoStyle.ReverseLShaped:
                        s += "tetromino-reverselshaped.png";
                        break;
                }
                return s;
            }
        }
    }
}