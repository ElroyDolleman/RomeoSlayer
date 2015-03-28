using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using E2DEngine.Graphics;

using RomeoSlayer.Resources;

namespace RomeoSlayer.Traps
{
    class SandSack : Trap
    {
        float fallSpeed, maxFallSpeed;
        float gravity;
        float groundPos;

        float lieTime;
        double timer;

        float beginPos;

        private enum SackState
        {
            Fall,
            PullBack,
            LetItGo,
            OnGround
        }

        SackState state;

        public SandSack(Vector2 position)
            : base((Sprite)Assets.sandSack.Clone())
        {
            this.position = position;
            this.hitbox = new Rectangle(8, 580, 88, 150);

            this.fallSpeed = .5f;
            this.groundPos = 576;
            this.maxFallSpeed = 20;
            this.gravity = this.fallSpeed;

            this.lieTime = 1000;
            this.timer = 0;

            this.beginPos = position.Y;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (active)
            {
                switch (state)
                {
                    case SackState.Fall:
                        gravity = Math.Min(gravity + 1, maxFallSpeed);
                        position.Y = Math.Min(position.Y + gravity, groundPos);

                        if (position.Y == groundPos)
                            state = SackState.OnGround;
                        break;
                    case SackState.OnGround:
                        timer += gameTime.ElapsedGameTime.TotalMilliseconds;

                        if (CurrentFrame != 2)
                            CurrentFrame = 2;

                        if (timer >= lieTime)
                        {
                            timer = 0;
                            state = SackState.PullBack;
                        }
                        break;
                    case SackState.PullBack:
                        position.Y--;

                        if (CurrentFrame != 1)
                            CurrentFrame = 1;

                        if (position.Y < beginPos)
                        {
                            position.Y = beginPos;
                            active = false;
                            state = SackState.Fall;
                        }
                        break;
                    case SackState.LetItGo:
                        // TODO: Make a little nice movement that it looks like it realy lets it go like disney frozen.
                        break;
                }
            }
        }

        public override bool CollideWithRomeo(AI.RomeoAI romeo)
        {
            return base.CollideWithRomeo(romeo) && (state == SackState.Fall);
        }
    }
}
