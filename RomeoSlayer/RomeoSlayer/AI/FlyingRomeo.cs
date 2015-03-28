using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using E2DEngine.Graphics;

using RomeoSlayer.Resources;

namespace RomeoSlayer.AI
{
    class FlyingRomeo : RomeoAI
    {
        bool isFalling;
        List<Sprite> ropes;

        public FlyingRomeo(Vector2 position, float velocity)
            : base((Sprite)Assets.flyingRomeoFlyAnimation.Clone(), position, velocity)
        {
            ropes = new List<Sprite>();
            isFalling = false;

            for (int i = 0; i < 1; i++)
            {
                ropes.Add((Sprite)Assets.rope.Clone());
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (deadState != DeadStates.Crushed)
            {
                base.Update(gameTime);
                base.UpdateAnimation(gameTime);
            }

            if (!isDead)
                MoveForward();

            if (deadState == DeadStates.Crushed && !isFalling)
            {
                isFalling = true;
                CopyAnimation(Assets.fallingHangRomeo);
            }

            if (isFalling)
            {
                position.Y = Math.Min(position.Y + 10, 580);

                if (CurrentFrame < 3)
                    base.UpdateAnimation(gameTime);

                if (position.Y == 580)
                {
                    base.UpdateAnimation(gameTime);

                    if (CurrentFrame == TotalFrames)
                        destroyAble = true;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (!isDead)
                for (int i = 0; i < ropes.Count; i++)
                {
                    ropes[i].position.X = this.position.X+1;
                    ropes[i].position.Y = this.position.Y - this.sourceRectangle.Height - (i+1 * 188);

                    ropes[i].Draw(spriteBatch);
                    spriteBatch.DrawRectangle(ropes[i].position - Vector2.One * 2, 4, 4, Color.Red);
                }
        }
    }
}
