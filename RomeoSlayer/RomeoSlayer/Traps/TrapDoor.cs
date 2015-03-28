#define DEBUG

#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using E2DEngine.Helpers;
using E2DEngine.Graphics;

using RomeoSlayer.Resources;
#endregion

namespace RomeoSlayer.Traps
{
    class TrapDoor : Trap
    {
#if DEBUG
        int place;
#endif

        Rectangle sourceRectOpen;
        Rectangle sourceRectClose;

        int openTime;
        int coolDown;

        double timer;

        public TrapDoor(Vector2 position, int place)
            : base(Assets.trapDoorsClose)
        {
            this.position = position;
            this.CurrentFrame = place;

            sourceRectClose = this.sourceRectangle;
            sourceRectOpen = this.sourceRectangle;
            sourceRectOpen.Y += this.sourceRectangle.Height;

            timer = 0;
            openTime = 1000;
            coolDown = 1000;

            switch (place)
            {
                default:
                case 1:
                    hitbox = new Rectangle(60, 38, 106, 49);
                    break;
                case 2:
                    hitbox = new Rectangle(30, 38, 116, 50);
                    break;
                case 3:
                    hitbox = new Rectangle(44, 38, 112, 50);
                    break;
                case 4:
                    hitbox = new Rectangle(42, 38, 120, 50);
                    break;
                case 5:
                    hitbox = new Rectangle(18, 38, 132, 50);
                    break;
            }

#if DEBUG
            this.place = place;
#endif
        }

        public override void Update(GameTime gameTime)
        {
            if (active)
            {
                timer += gameTime.ElapsedGameTime.TotalMilliseconds;

                if (timer >= openTime && this.sourceRectangle == sourceRectOpen)
                    this.sourceRectangle = sourceRectClose;
                    
                else if (timer <= openTime && this.sourceRectangle == sourceRectClose)
                    this.sourceRectangle = sourceRectOpen;

                if (timer >= openTime + coolDown)
                {
                    timer = 0;
                    active = false;
                }
            }
        }

        public override bool CollideWithRomeo(AI.RomeoAI romeo)
        {
            if (base.CollideWithRomeo(romeo))
                if (timer < openTime)
                    return (romeo.hitbox.Left > this.hitbox.Left && romeo.hitbox.Right < this.hitbox.Right);

            return false;
        }

#if DEBUG
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.DrawString(Debug.spriteFont, "" + place, this.position, Color.Black);
        }
#endif
    }
}
