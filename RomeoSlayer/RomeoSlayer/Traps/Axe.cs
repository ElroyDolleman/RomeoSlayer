#define DEBUG

#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using E2DEngine.Helpers;
using E2DEngine.Entities;
using E2DEngine.Graphics;

using RomeoSlayer.AI;
using RomeoSlayer.Resources;
#endregion

namespace RomeoSlayer.Traps
{
    class Axe : E2DGameObject
    {
        // CoolDown
        public const int COOL_DOWN = 30 * 1000;

        private float coolDownTimer;
        private bool isActivated;

        public float CoolDownTimer 
        { 
            get { return coolDownTimer; }
            private set { coolDownTimer = MathHelper.Clamp(value, 0, COOL_DOWN); }
        }

        // Velocity
        const float maxVelocity = 3.2f;
        const float acceleration = .03855f;
        readonly float baseVelocity;

        float velocity;

        float Velocity
        {
            get { return velocity; }
            set { velocity = MathHelper.Clamp(value, 0, maxVelocity); }
        }

        Vector2 slayPosition;
        float slayDistance;

        public Axe(Vector2 position)
            : base(Assets.axe)
        {
            this.position = position;
            this.rotation = MathHelper.ToRadians(135);

            this.baseVelocity = 2f;
            this.velocity = baseVelocity;

            this.isActivated = false;
            this.coolDownTimer = COOL_DOWN;

            slayDistance = 340;
            slayPosition = position + new Vector2(0, slayDistance);
        }

        public override void Update(GameTime gameTime)
        {
            if (CoolDownTimer != COOL_DOWN)
                CoolDownTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (isActivated)
            {
                if (MathHelper.ToDegrees(rotation) == MathHelper.Clamp(MathHelper.ToDegrees(rotation), 1, 135))
                    this.Velocity += acceleration;
                else
                    this.Velocity -= acceleration;

                this.rotation -= MathHelper.ToRadians(Velocity);

                if (MathHelper.ToDegrees(rotation) < -130)
                {
                    rotation = MathHelper.ToRadians(135);
                    this.velocity = baseVelocity;

                    this.isActivated = false;
                }

                slayPosition.X = position.X + (float)Math.Cos((MathHelper.ToDegrees(rotation) +90) * (Math.PI / 180)) * slayDistance;
                slayPosition.Y = position.Y + (float)Math.Sin((MathHelper.ToDegrees(rotation) +90) * (Math.PI / 180)) * slayDistance;
            }

            
        }

        public bool CollideWithRomeo(RomeoAI romeo)
        {
            return  (Vector2.Distance(romeo.hitbox.LeftTopCorner().ToVector2(), slayPosition) < 236) ||
                    (Vector2.Distance(romeo.hitbox.RightTopCorner().ToVector2(), slayPosition) < 236) ||
                    (Vector2.Distance(romeo.hitbox.LeftBottomCorner().ToVector2(), slayPosition) < 236) ||
                    (Vector2.Distance(romeo.hitbox.RightBottomCorner().ToVector2(), slayPosition) < 236);
        }

        public void Activate()
        {
            if (CoolDownTimer == COOL_DOWN)
            {
                CoolDownTimer = 0;
                isActivated = true;
            }
        }

#if DEBUG
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.DrawString(Debug.spriteFont, "Rotation: " + MathHelper.ToDegrees(rotation), new Vector2(200, 70), Color.Black);

            spriteBatch.DrawRectangle(slayPosition - Vector2.One * 6, 12, 12, Color.Red);
        }
#endif
    }
}
