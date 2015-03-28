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
#endregion

namespace RomeoSlayer.AI
{
    class RomeoAI : E2DGameObject
    {
        public enum DeadStates
        {
            NotDead,
            Fall,
            Burn,
            Crushed,
            Slayed
        }

        protected DeadStates deadState;
        public bool isDead;
        public bool destroyAble;

        public float velocity;

        public Sprite burnAnimation, squashAnimation;

        public RomeoAI(Sprite sprite, Vector2 position, float velocity = 4f)
            : base(sprite)
        {
            this.velocity = velocity;
            this.position = position;
            this.isDead = false;
            this.destroyAble = false;
            this.deadState = DeadStates.NotDead;
        }

        public virtual void Initialize()
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (isDead)
            {
                switch (deadState)
                {
                    case DeadStates.Fall:
                        if (sourceRectangle.Height > 0)
                        {
                            sourceRectangle.Height = Math.Max(sourceRectangle.Height - 6, 0);
                            position.Y += 6;
                        }
                        else
                            destroyAble = true;
                        break;
                    case DeadStates.Burn:
                        if (CurrentFrame == TotalFrames)
                            destroyAble = true;
                        break;
                    case DeadStates.Crushed:
                        if (CurrentFrame == TotalFrames)
                            destroyAble = true;
                        break;
                    case DeadStates.Slayed:
                        destroyAble = true;
                        break;
                }
            }
        }

        public virtual void MoveForward()
        {
            this.position.X -= velocity;
        }

        public void Die(DeadStates deadState)
        {
            if (!isDead)
            {
                isDead = true;

                switch (deadState)
                {
                    case DeadStates.Burn:
                        if (burnAnimation != null)
                            CopyAnimation(burnAnimation);
                        break;
                    case DeadStates.Crushed:
                        if (squashAnimation != null)
                            CopyAnimation(squashAnimation);
                        break;
                }

                this.deadState = deadState;
            }
        }

#if DEBUG
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.DrawRectangle(this.hitbox, Color.Red * .3f);
            spriteBatch.DrawString(Resources.Debug.spriteFont, "Hitbox", new Vector2(hitbox.X, hitbox.Y), Color.Black);

            spriteBatch.DrawRectangle(this.hitbox.Center.ToVector2() - Vector2.One * 2, 4, 4, Color.Blue);
        }
#endif
    }
}
