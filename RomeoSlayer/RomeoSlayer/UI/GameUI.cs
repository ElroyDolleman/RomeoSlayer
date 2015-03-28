using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using E2DEngine.Graphics;

using RomeoSlayer.Entities;
using RomeoSlayer.Resources;
using RomeoSlayer.GameManagers;

namespace RomeoSlayer.UI
{
    class GameUI
    {
        public bool pause, gameOver;

        public Button backButton;
        public Button restartButton;
        public Button pauseButton;

        public GameUI()
        {
            pause = false;
            gameOver = false;

            pauseButton = new Button((Sprite)Assets.pauseButton.Clone(), new Vector2(962, 64), Managers.screenManager.mouseState);
            backButton = new Button((Sprite)Assets.homeButton.Clone(), new Vector2(Main.WindowCenter.X / 2, 650), Managers.screenManager.mouseState);
            restartButton = new Button((Sprite)Assets.restartButton.Clone(), new Vector2(Main.WindowCenter.X / 2 + Main.WindowCenter.X, 650), Managers.screenManager.mouseState);

            pauseButton.OnClick += new Button.OnClickEvent(PauseGame);
            backButton.OnClick += new Button.OnClickEvent(BackToStartScreen);
            restartButton.OnClick += new Button.OnClickEvent(RestartGame);

            // Pause Source Rectangle
            Rectangle pauseSource = pauseButton.sourceRectangle;
            pauseButton.normalSource = pauseSource;
            pauseButton.hoverSource = pauseSource;

            pauseSource.Y += pauseSource.Height;
            pauseButton.clickSource = pauseSource;

            // Back Source Rectangle
            Rectangle backSource = backButton.sourceRectangle;
            backButton.normalSource = backSource;
            backButton.hoverSource = backSource;

            backSource.Y += backSource.Height;
            backButton.clickSource = backSource;

            // Restart Source Rectangle
            Rectangle restartSource = restartButton.sourceRectangle;
            restartButton.normalSource = restartSource;
            restartButton.hoverSource = restartSource;

            restartSource.Y += restartSource.Height;
            restartButton.clickSource = restartSource;
            
        }

        public void Update(GameTime gameTime)
        {
            if (gameOver)
            {
                backButton.Update(gameTime);
                restartButton.Update(gameTime);
            }
            else
                pauseButton.Update(gameTime);
        }

        private void PauseGame(Button button)
        {
            pause = !pause;
        }

        private void RestartGame(Button button)
        {
            Managers.screenManager.OpenScreen(ScreenManager.ScreenState.Game);
        }

        private void BackToStartScreen(Button button)
        {
            Managers.screenManager.OpenScreen(ScreenManager.ScreenState.Start);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (gameOver)
            {
                backButton.Draw(spriteBatch);
                restartButton.Draw(spriteBatch);
            }
            else
                pauseButton.Draw(spriteBatch);
        }
    }
}
