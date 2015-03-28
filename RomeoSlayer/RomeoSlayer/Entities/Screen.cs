#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using E2DEngine.Graphics;
#endregion

namespace RomeoSlayer.Entities
{
    class Screen
    {
        public E2DTexture background;

        public Screen()
        {

        }

        public virtual void Initialize()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            try
            {
                spriteBatch.DrawE2DTexture(background, Vector2.Zero);
            }
            catch (System.NullReferenceException)
            {
                throw new Exception("background is probably null");
            }
        }
    }
}
