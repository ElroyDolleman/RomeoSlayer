#define DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using E2DEngine.Graphics;

using RomeoSlayer.Resources;

namespace RomeoSlayer.AI
{
    class JumpingRomeo : RomeoAI
    {
        private enum State
        {
            Jumping,
            Landing,
            Falling,
            GoUp
        }

        private State currentState, previousState;
        private Dictionary<State, Sprite> animations;

        public float jumpPower;
        public readonly float gravity;
        public readonly float maxGravity;

        private float fallSpeed;

        private float groundPositionY; // The y position where romeo lands on the ground

        public JumpingRomeo(Sprite sprite, Vector2 position, float velocity = 2f, float jumpPower = 10f, float gravity = .5f, float maxGravity = 8f)
            : base(sprite, position, velocity)
        {
            this.groundPositionY = position.Y;

            this.jumpPower = jumpPower;
            this.gravity = gravity;
            this.maxGravity = maxGravity;

            this.fallSpeed = gravity;

            this.currentState = State.GoUp;
            this.previousState = State.GoUp;

            animations = new Dictionary<State, Sprite>();
            animations.Add(State.GoUp, (Sprite)Assets.jumpingRomeoGoUpAnimation.Clone());
            animations.Add(State.Jumping, (Sprite)Assets.jumpingRomeoJumpAnimation.Clone());
            animations.Add(State.Falling, (Sprite)Assets.jumpingRomeoFallAnimation.Clone());
            animations.Add(State.Landing, (Sprite)Assets.jumpingRomeoLandAnimation.Clone());
        }

        public override void Initialize()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if (deadState != DeadStates.Fall)
                base.UpdateAnimation(gameTime);

            if (!isDead)
            {
                switch (currentState)
                {
                    case State.Landing:
                    case State.GoUp:
                        if (previousState != currentState)
                        {
                            previousState = currentState;
                            CopyAnimation(animations[currentState]);
                        }

                        if (CurrentFrame == TotalFrames)
                        {
                            if (currentState == State.GoUp)
                                currentState = State.Jumping;
                            else
                                currentState = State.GoUp;
                        }
                        break;

                    case State.Jumping:
                        if (previousState != currentState)
                        {
                            fallSpeed = -jumpPower;

                            previousState = currentState;
                            CopyAnimation(animations[currentState]);
                        }

                        Jumping();
                        MoveForward();

                        if (fallSpeed >= 0)
                            currentState = State.Falling;
                        break;

                    case State.Falling:
                        if (previousState != currentState)
                        {
                            previousState = currentState;
                            CopyAnimation(animations[currentState]);
                        }

                        if (CurrentFrame == TotalFrames)
                            CurrentFrame = TotalFrames - 1;

                        Jumping();
                        MoveForward();

                        if (position.Y == groundPositionY)
                            currentState = State.Landing;
                        break;
                }
            }

            base.Update(gameTime);
        }

        public void Jumping()
        {
            this.position.Y = Math.Min(position.Y + fallSpeed, groundPositionY);

            this.fallSpeed = Math.Min(maxGravity, fallSpeed + gravity);
        }

#if DEBUG
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.DrawString(Debug.spriteFont, "" + currentState, position, Color.Black);
            spriteBatch.DrawRectangle(this.position - Vector2.One * 2, 4, 4, Color.Purple);
        }
#endif
    }
}
