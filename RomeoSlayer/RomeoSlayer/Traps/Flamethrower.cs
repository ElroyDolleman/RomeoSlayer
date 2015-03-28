using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using E2DEngine.Graphics;

using RomeoSlayer.Resources;

namespace RomeoSlayer.Traps
{
    class Flamethrower : Trap
    {
        private enum State
        {
            Burning,
            Off,
            CoolDown
        }

        private State currentState;

        private Sprite flamethrower = (Sprite)Assets.flamethrower.Clone();
        private Sprite On = (Sprite)Assets.flamethrowerOnAnimation.Clone();
        private Sprite Off = (Sprite)Assets.flamethrowerOffAnimation.Clone();

        private double timer;

        private int burnTime = 1000;
        private int coolDown = 3000;

        public Flamethrower(Vector2 position)
            : base((Sprite)Assets.flamethrowerOffAnimation.Clone())
        {
            this.position = new Vector2(position.X, position.Y - 140);
            this.hitbox = new Rectangle(20, 128, 96, 217);

            flamethrower.position = position;
            flamethrower.origin = new Vector2(30, 160);

            this.currentState = State.Off;
        }

        public override void Update(GameTime gameTime)
        {
            base.UpdateAnimation(gameTime);

            switch(currentState)
            {
                case State.Off:
                    if (currentState != State.Off)
                    {
                        currentState = State.Off;
                        active = false;
                    }

                    if (active)
                        goto case State.Burning; 
                    break;

                case State.Burning:
                    if (currentState != State.Burning)
                    {
                        CopyAnimation(On);
                        this.origin = this.On.origin;

                        currentState = State.Burning;
                        timer = 0;
                    }

                    timer += gameTime.ElapsedGameTime.TotalMilliseconds;

                    if (timer > burnTime)
                        goto case State.CoolDown;
                    break;

                case State.CoolDown:
                    if (currentState != State.CoolDown)
                    {
                        CopyAnimation(Off);
                        this.origin = this.Off.origin;

                        currentState = State.CoolDown;
                        timer = 0;
                    }

                    timer += gameTime.ElapsedGameTime.TotalMilliseconds;

                    if (timer > coolDown)
                        goto case State.Off;
                    break;
            }
        }

        public override bool CollideWithRomeo(AI.RomeoAI romeo)
        {
            return (currentState != State.CoolDown) && base.CollideWithRomeo(romeo);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            flamethrower.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }
    }
}
