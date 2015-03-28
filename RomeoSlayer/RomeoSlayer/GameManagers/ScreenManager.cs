#define DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using E2DEngine.Input;

using RomeoSlayer.Entities;
using RomeoSlayer.GameScreens;

namespace RomeoSlayer.GameManagers
{
    class ScreenManager
    {
        public enum ScreenState
        {
            Start,
            Game
        }

        public E2DMouseState mouseState;
        public E2DKeyboardState keyboardState;

        public Dictionary<ScreenState, Screen> screens;

        private ScreenState currentState, previousState;

#if DEBUG
        private double slowTimer = 0;
        private double slowInterval = 50;
#endif

        public ScreenManager()
        {
            // Input States
            mouseState = new E2DMouseState();
            keyboardState = new E2DKeyboardState();

            // Set default state
            currentState = ScreenState.Start;

            // Adding screens
            screens = new Dictionary<ScreenState, Screen>();

            screens.Add(ScreenState.Start, new StartScreen());
            screens.Add(ScreenState.Game, new GameScreen());
        }

        public void Initialize()
        {
            // Initialize default screen
            screens[currentState].Initialize();
        }

        public void OpenScreen(ScreenState screenState)
        {
            previousState = currentState;
            currentState = screenState;

            screens[currentState].Initialize();
        }

        public void Update(GameTime gameTime)
        {
            mouseState.Begin();
            keyboardState.Begin();

#if DEBUG
            Resources.Debug.UpdateChangeableSpeed(keyboardState);
            //slowTimer++;

            if (true)//(slowTimer >= Resources.Debug.Speed)
            {
                
#endif
            // Not DEBUG
            screens[currentState].Update(gameTime);

#if DEBUG
            }
#endif
            keyboardState.End();
            mouseState.End();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            screens[currentState].Draw(spriteBatch);

#if DEBUG
            spriteBatch.Begin();
            spriteBatch.DrawString(Resources.Debug.spriteFont, "Speed: " + Resources.Debug.Speed, new Vector2(900, 76), Color.Black);
            spriteBatch.End();
#endif
        }
    }
}
