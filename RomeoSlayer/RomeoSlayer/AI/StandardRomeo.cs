using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using E2DEngine.Graphics;

namespace RomeoSlayer.AI
{
    class StandardRomeo : RomeoAI
    {
        public enum Animation
        {
            Walk,
            Idle,
            Fall
        }

        public Animation currentAnimation;

        public StandardRomeo(Sprite sprite, Vector2 position, float velocity = 4)
            : base(sprite, position, velocity)
        {
            currentAnimation = Animation.Walk;

            
        }

        public override void Initialize()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if (!isDead || deadState != DeadStates.Fall)
                base.UpdateAnimation(gameTime);

            if (!isDead)
                MoveForward();

            base.Update(gameTime);
        }
    }
}
