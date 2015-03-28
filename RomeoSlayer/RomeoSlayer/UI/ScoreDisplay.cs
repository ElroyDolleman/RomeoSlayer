using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using E2DEngine.Helpers;
using E2DEngine.Graphics;

using RomeoSlayer.Resources;

namespace RomeoSlayer.UI
{
    class ScoreDisplay
    {
        List<Sprite> display;
        
        public Vector2 position;

        public ScoreDisplay(Vector2 position)
        {
            display = new List<Sprite>();

            for (int i = 0; i < 5; i++)
                display.Add((Sprite)Assets.number.Clone());

            this.position = position;
        }

        public void Draw(SpriteBatch spriteBatch, int score)
        {
            int[] digits = MathHelp.GetDigits(score);
            int length = Math.Min(digits.GetUpperBound(0)+1, display.Count-1);

            Array.Reverse(digits);

            for (int i = 0; i < length; i++)
            {
                Sprite number = display[i];

                number.position.X = this.position.X - (number.sourceRectangle.Width * number.scale.X) * (i+1);
                number.position.Y = this.position.Y;
                number.CurrentFrame = digits[i]+1;

                number.Draw(spriteBatch);
            }

            
        }
    }
}
