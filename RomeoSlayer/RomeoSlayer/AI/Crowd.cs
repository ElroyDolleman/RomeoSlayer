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
    class Crowd
    {
        CroudState state;

        List<Sprite> backCroud;
        List<Spectator> spectators;

        private const int interval = 1600;
        private float stateTimer;

        public Crowd()
        {
            spectators = new List<Spectator>();
            backCroud = new List<Sprite>();

            Random randomPerson = new Random();

            for (int y = 0; y < 3; y++)
                for (int x = 0; x < 25; x++)
                {
                    Sprite person = (Sprite)Assets.crowd.Clone();
                    person.CurrentFrame = randomPerson.Next(0, 7);

                    spectators.Add(new Spectator(person, new Vector2(x * person.sourceRectangle.Width, 384 + 48 * y)));
                }

            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 25; x++)
                {
                    Sprite person = (Sprite)Assets.crowd.Clone();
                    person.CurrentFrame = 7;
                    person.position = new Vector2(x * person.sourceRectangle.Width, 288 + (48 * y));

                    backCroud.Add(person);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach(Spectator spectator in spectators)
                spectator.Update(gameTime);

            if (state != CroudState.Neutral)
            {
                stateTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (stateTimer >= interval)
                {
                    stateTimer = 0;
                    ChangeState(CroudState.Neutral);
                }
            }
        }

        public void ChangeState(CroudState newState)
        {
            if (state != newState)
            {
                switch (newState)
                {
                    case CroudState.Happy:
                        GameManagers.Managers.soundManager.PlayMusic(Sound.Sounds.crowd);
                        break;
                }

                state = newState;

                foreach (Spectator spectator in spectators)
                    spectator.ChangeState(newState);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Sprite person in backCroud)
                person.Draw(spriteBatch);

            foreach (Spectator spectator in spectators)
                spectator.Draw(spriteBatch);
        }
    }
}
