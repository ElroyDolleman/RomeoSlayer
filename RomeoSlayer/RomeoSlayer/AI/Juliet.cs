#define DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using E2DEngine.Entities;
using E2DEngine.Graphics;

using RomeoSlayer.Resources;

namespace RomeoSlayer.AI
{
    class Juliet : E2DGameObject
    {
        public Juliet(Vector2 position)
            : base(Assets.juliet)
        {
            this.position = position;
        }

        public override void Update(GameTime gameTime)
        {
            base.UpdateAnimation(gameTime);
        }

#if DEBUG
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.DrawRectangle(position - Vector2.One * 2, 4, 4, Color.Red);
        }
#endif

    }
}
