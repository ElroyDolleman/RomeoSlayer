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
    public enum CroudState
    {
        Neutral,
        Happy,
        Surprise
    }

    class Spectator
    {
        private CroudState state;

        public Sprite person;
        public Sprite clapHands;

        public Spectator(Sprite person, Vector2 position)
        {
            this.person = person;
            this.clapHands = (Sprite)Assets.clapAnimation.Clone();

            person.position = position;
            clapHands.position = new Vector2(position.X, position.Y + 16);
        }

        public void Update(GameTime gameTime)
        {
            if (state == CroudState.Happy)
                clapHands.UpdateAnimation(gameTime);
        }

        public void ChangeState(CroudState newState)
        {
            this.state = newState;

            switch(this.state)
            {
                case CroudState.Neutral:
                    person.sourceRectangle.Y = 0;
                    break;
                case CroudState.Happy:
                    person.sourceRectangle.Y = person.sourceRectangle.Height;
                    clapHands.CurrentFrame = 1;
                    break;
                case CroudState.Surprise:
                    person.sourceRectangle.Y = person.sourceRectangle.Height * 2;
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            person.Draw(spriteBatch);

            if (state == CroudState.Happy)
                clapHands.Draw(spriteBatch);
        }
    }
}
