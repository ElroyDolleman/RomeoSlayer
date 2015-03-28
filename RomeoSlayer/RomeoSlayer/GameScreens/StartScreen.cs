#define DEBUG

#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using E2DEngine.Input;
using E2DEngine.Helpers;
using E2DEngine.Graphics;

using RomeoSlayer.Sound;
using RomeoSlayer.Entities;
using RomeoSlayer.Resources;
using RomeoSlayer.GameManagers;
#endregion

namespace RomeoSlayer.GameScreens
{
    class StartScreen : Screen
    {
        Sprite logo;

        Button playButton;
        Button infoButton;
        Button optionsButton;

        E2DMouseState mouseState;

        public StartScreen()
        {
            background = Assets.startScreenBackground;

            logo = (Sprite)Assets.logo.Clone();
            logo.position = new Vector2(550, 246);
        }

        public override void Initialize()
        {
            mouseState = Managers.screenManager.mouseState;

            playButton = new Button(Assets.playButton, new Vector2(721, 476), mouseState);

            playButton.clickSource = MathHelp.RectangleFromCenter(902, 530, 192, 192);
            playButton.OnClick = new Button.OnClickEvent(PlayGame);

            infoButton = new Button(Assets.infoButton, new Vector2(519, 476), mouseState);

            optionsButton = new Button(Assets.optionsButton, new Vector2(319, 476), mouseState);

            Managers.soundManager.PlayMusic(Sounds.menuMusic);
        }

        public override void Update(GameTime gameTime)
        {
            playButton.Update(gameTime);
        }

        public void PlayGame(Button button)
        {
            Managers.soundManager.StopMusic();
            Managers.screenManager.OpenScreen(ScreenManager.ScreenState.Game);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            // Draw Background
            base.Draw(spriteBatch);
            logo.Draw(spriteBatch);

            // Draw Buttons
            playButton.Draw(spriteBatch);
            infoButton.Draw(spriteBatch);
            optionsButton.Draw(spriteBatch);

#if DEBUG
            spriteBatch.DrawRectangle(playButton.position - Vector2.One * 2, 4, 4, Color.Purple);
            spriteBatch.DrawRectangle(infoButton.position - Vector2.One * 2, 4, 4, Color.Purple);
            spriteBatch.DrawRectangle(optionsButton.position - Vector2.One * 2, 4, 4, Color.Purple);
#endif

            spriteBatch.End();
        }
    }
}
