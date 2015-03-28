#define DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using E2DEngine.Entities;
using E2DEngine.Graphics;

using RomeoSlayer.AI;

namespace RomeoSlayer.Traps
{
    class Trap : E2DGameObject
    {
        public bool active;

        public Trap(Sprite sprite)
            : base(sprite)
        {
            active = false;
        }

        public virtual bool CollideWithRomeo(RomeoAI romeo)
        {
            return hitbox.Intersects(romeo.hitbox);
        }

#if DEBUG
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.DrawRectangle(hitbox, this.colorEffect * .2f);

            spriteBatch.DrawRectangle(this.position - Vector2.One * 2, 4, 4, Color.Red);
        }
#endif
    }
}
