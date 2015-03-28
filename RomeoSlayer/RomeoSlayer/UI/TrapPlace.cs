#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using E2DEngine.Input;
using E2DEngine.Entities;
using E2DEngine.Graphics;
#endregion

namespace RomeoSlayer.UI
{
    class TrapPlace : E2DGameObject
    {
        private enum State
        {
            ShowIcons,
            CloseIcons,
            OpenIcons,
        }

        E2DMouseState mouseState;
        readonly int distance;

        public TrapPlace(Vector2 position, E2DMouseState mouseState)
            : base(Sprite.Empty)
        {
            this.position = position;
            this.mouseState = mouseState;
            this.distance = this.sourceRectangle.Width;
        }

        public void Update(GameTime gameTime)
        {
            if (mouseState.LeftButtonPressed)
                if (Vector2.Distance(this.position, mouseState.Position) < distance)
                {

                }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
