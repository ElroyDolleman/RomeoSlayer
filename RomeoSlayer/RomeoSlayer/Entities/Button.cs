#define DEBUG

#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using E2DEngine.Input;
using E2DEngine.Graphics;
#endregion

namespace RomeoSlayer.Entities
{
    class Button : Sprite
    {
        public delegate void OnClickEvent(Button button);
        public OnClickEvent OnClick;

        public Rectangle normalSource;
        public Rectangle clickSource;
        public Rectangle hoverSource;
        E2DMouseState mouseState;

        public Rectangle clickArea;

        public Button(Sprite sprite, Vector2 position, E2DMouseState mouseState)
            : base(sprite)
        {
            this.position = position;
            this.mouseState = mouseState;
            
            this.clickArea = new Rectangle((int)(position.X - origin.X), (int)(position.Y - origin.Y), sourceRectangle.Width, sourceRectangle.Height);
            this.normalSource = sourceRectangle;
        }

        public Button(Sprite sprite, Vector2 position, E2DMouseState mouseState, Rectangle clickArea)
            : this(sprite, position, mouseState)
        {
            this.clickArea = clickArea;
        }

        public Button(Sprite sprite, Vector2 position, E2DMouseState mouseState, Rectangle clickArea, Rectangle click, Rectangle hover)
            : this(sprite, position, mouseState, clickArea)
        {
            this.clickSource = click;
            this.hoverSource = hover;
        }

        public override void Update(GameTime gameTime)
        {
            if (mouseState.LeftButtonReleased)
                if (clickArea.Contains(mouseState.Position))
                    OnClick(this);

            if (mouseState.LeftButtonHold)
            {
                if (clickSource != Rectangle.Empty && sourceRectangle != clickSource)
                    if (clickArea.Contains(mouseState.Position))
                        sourceRectangle = clickSource;
            }
            else // Not holding left button
            {
                if (!clickArea.Contains(mouseState.Position))
                {
                    if (sourceRectangle != normalSource)
                        sourceRectangle = normalSource;
                }
                else if (hoverSource != Rectangle.Empty && sourceRectangle != hoverSource)
                    sourceRectangle = hoverSource;
            }
        }

#if DEBUG
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.DrawRectangle(clickArea, Color.RoyalBlue * .25f);

            spriteBatch.DrawString(Resources.Debug.spriteFont, "click area", new Vector2(clickArea.X, clickArea.Y), Color.Black);
            spriteBatch.DrawString(Resources.Debug.spriteFont, "X: " + sourceRectangle.X, new Vector2(clickArea.X, clickArea.Y + 20), Color.Black);
            spriteBatch.DrawString(Resources.Debug.spriteFont, "Y: " + sourceRectangle.Y, new Vector2(clickArea.X, clickArea.Y + 40), Color.Black);
            spriteBatch.DrawString(Resources.Debug.spriteFont, "W: " + sourceRectangle.Width, new Vector2(clickArea.X, clickArea.Y + 60), Color.Black);
            spriteBatch.DrawString(Resources.Debug.spriteFont, "H: " + sourceRectangle.Height, new Vector2(clickArea.X, clickArea.Y + 80), Color.Black);
        }
#endif
    }
}
