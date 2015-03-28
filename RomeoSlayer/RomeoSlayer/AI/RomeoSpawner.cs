#define DEBUG

#region Using Statement
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using E2DEngine.Graphics;

using RomeoSlayer.Resources;
using RomeoSlayer.GameScreens;
using RomeoSlayer.GameManagers;
#endregion

namespace RomeoSlayer.AI
{
    class RomeoSpawner
    {
        const int level2 = 4;
        const int level3 = 12;
        const int level4 = 20;
        const int level5 = 34;
        const int level6 = 48;
        const int level7 = 100;

#if DEBUG
        private float latestBalance;
#endif
        private const int totalRomeo = 4;
        private List<float> randomBalance;

        public List<RomeoAI> romeoList;
        
        public int difficulty;

        public float spawnInterval;
        private float timer;
        public byte romeoKills;

        public Vector2 spawnPosition, airSpawnPosition;
        public float endPositionX;

        public RomeoSpawner(Vector2 spawnPosition, Vector2 airSpawnPosition, float endPositionX)
        {
            randomBalance = new List<float>();
            for (int i = 0; i < totalRomeo; i++)
                randomBalance.Add(100 / totalRomeo);

            this.timer = 0;
            this.difficulty = 1;
            this.romeoKills = 3;

            this.spawnInterval = 3600f;

            this.spawnPosition = spawnPosition;
            this.airSpawnPosition = airSpawnPosition;
            this.endPositionX = endPositionX;

            this.romeoList = new List<RomeoAI>();
        }

        public void Update(GameTime gameTime)
        {
            // Spawning
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer > spawnInterval)
            {
                timer = 0;
                Spawn();
            }

            // Update Romeo
            for (int i = 0; i < romeoList.Count; i++ )
            {
                RomeoAI romeo = romeoList[i];

                if (romeo is StandardRomeo)
                    (romeo as StandardRomeo).Update(gameTime);

                if (romeo is JumpingRomeo)
                    (romeo as JumpingRomeo).Update(gameTime);

                if (romeo is FlyingRomeo)
                    (romeo as FlyingRomeo).Update(gameTime);

                if (romeo.position.X < 150)
                    (Managers.screenManager.screens[ScreenManager.ScreenState.Game] as GameScreen).gameUI.gameOver = true;

                if (romeo.destroyAble)
                {
                    romeoKills++;

                    switch (romeoKills)
                    {
                        case level2:
                            spawnInterval = 3000f;
                            difficulty = 2;
                            break;
                        case level3:
                            spawnInterval = 2400f;
                            difficulty = 3;
                            break;
                        case level4:
                            spawnInterval = 2000f;
                            difficulty = 4;
                            break;
                        case level5:
                            spawnInterval = 1600f;
                            difficulty = 5;
                            break;
                        case level6:
                            spawnInterval = 1000f;
                            difficulty = 6;
                            break;
                        case level7:
                            spawnInterval = 500f;
                            difficulty = 7;
                            break;
                    }

                    romeoList.Remove(romeo);
                }
            }
        }

        public void Spawn()
        {
            Random random = new Random();
            int randomInt = random.Next(0, 101); // between zero and one-hundred procent

            float balanceCount = 0f;
            for (int i = 0; i < totalRomeo; i++)
            {
                if (randomInt == MathHelper.Clamp(randomInt, balanceCount, randomBalance[i] + balanceCount))
                {
                    switch (i)
                    {
                        case 0:
                            romeoList.Add(newNormalRomeo);
                            break;
                        case 1:
                            romeoList.Add(newLittleRomeo);
                            break;
                        case 2:
                            romeoList.Add(newFlyingRomeo);
                            break;
                        case 3:
                            romeoList.Add(newJumpingRomeo);
                            break;
                    }

                    if (randomBalance[i] > 12)
                    {
                        randomBalance[i] -= 12;

                        for (int j = 0; j < totalRomeo; j++)
                            if (j != i)
                                randomBalance[j] += 4;
                    }

                    break;
                }

                balanceCount += randomBalance[i];
            }

#if DEBUG
            latestBalance = randomInt;
#endif
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (RomeoAI romeo in romeoList)
                romeo.Draw(spriteBatch);

#if DEBUG
            spriteBatch.DrawString(Debug.spriteFont, "normal: " + randomBalance[0], new Vector2(24, 30), Color.Black);
            spriteBatch.DrawString(Debug.spriteFont, "little: " + randomBalance[1], new Vector2(24, 50), Color.Black);
            spriteBatch.DrawString(Debug.spriteFont, "flying: " + randomBalance[2], new Vector2(24, 70), Color.Black);
            spriteBatch.DrawString(Debug.spriteFont, "jumping: " + randomBalance[3], new Vector2(24, 90), Color.Black);
            spriteBatch.DrawString(Debug.spriteFont, "last random: " + latestBalance, new Vector2(24, 110), Color.Black);

            spriteBatch.DrawString(Debug.spriteFont, "level: " + difficulty, new Vector2(100, 150), Color.Black);
            spriteBatch.DrawString(Debug.spriteFont, "Romeo Kills: " + romeoKills, new Vector2(180, 180), Color.Black);
#endif
        }

        #region New Romeo
        private StandardRomeo newNormalRomeo
        {
            get
            {
                StandardRomeo romeo = new StandardRomeo((Sprite)Assets.normalRomeoWalkAnimation.Clone(), spawnPosition, 1.75f);

                romeo.origin = new Vector2(0, romeo.sourceRectangle.Height);
                romeo.hitbox = new Rectangle(48, 20, 64, 140);

                romeo.burnAnimation = (Sprite)Assets.normalRomeoBurn.Clone();
                romeo.squashAnimation = (Sprite)Assets.normalRomeoCrushed.Clone();

                return romeo;
            }
        }

        private StandardRomeo newLittleRomeo
        {
            get
            {
                StandardRomeo romeo = new StandardRomeo((Sprite)Assets.littleRomeoWalkAnimation.Clone(), spawnPosition, 2f);

                romeo.origin = new Vector2(0, romeo.sourceRectangle.Height);
                romeo.hitbox = new Rectangle(48, 40, 64, 120);

                romeo.squashAnimation = (Sprite)Assets.littleRomeoSquash.Clone();

                return romeo;
            }
        }

        private FlyingRomeo newFlyingRomeo
        {
            get
            {
                FlyingRomeo romeo = new FlyingRomeo(airSpawnPosition, 1.5f);

                romeo.burnAnimation = (Sprite)Assets.hangRomeoBurn.Clone();

                romeo.origin = new Vector2(0, romeo.sourceRectangle.Height);
                romeo.hitbox = new Rectangle(28, 36, 124, 130);

                return romeo;
            }
        }

        private JumpingRomeo newJumpingRomeo
        {
            get
            {
                JumpingRomeo romeo = new JumpingRomeo((Sprite)Assets.jumpingRomeoJumpAnimation.Clone(), spawnPosition - new Vector2(-36, 0), 3.34f);

                //romeo.origin = new Vector2(0, romeo.sourceRectangle.Height);
                romeo.hitbox = new Rectangle(36, 14, 94, 174);

                romeo.burnAnimation = (Sprite)Assets.jumpingRomeoBurn.Clone();
                romeo.squashAnimation = (Sprite)Assets.jumpingRomeoCrushed.Clone();

                return romeo;
            }
        }
        #endregion
    }
}
