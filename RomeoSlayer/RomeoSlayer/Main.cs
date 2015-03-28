#define DEBUG

#region Using Statements
using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using E2DEngine;
using E2DEngine.Entities;
using E2DEngine.Graphics;

using RomeoSlayer.Sound;
using RomeoSlayer.Resources;
using RomeoSlayer.GameManagers;
#endregion

namespace RomeoSlayer
{
    public class Main : E2DGame
    {
#if DEBUG
        E2DEngine.Input.E2DMouseState mouseState = new E2DEngine.Input.E2DMouseState();
#endif

        public Main()
            : base()
        {
            Content.RootDirectory = "Content";
            Window.Title = "Romeo Slayer";

            IsMouseVisible = true;
            Window.IsBorderless = false;

            WindowWidth = 1024;
            WindowHeight = 768;

            backgroundColor = Color.Black;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            Assets.LoadAssets();
            Sounds.LoadSounds(Content);

#if DEBUG
            Console.WriteLine("Debug mode is on.");
            Debug.spriteFont = Content.Load<SpriteFont>("Fonts\\StandardSpritefont");
#endif
            Managers.soundManager = new Sound.SoundManager();

            Managers.screenManager = new ScreenManager();
            Managers.screenManager.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            Managers.screenManager.Update(gameTime);

#if DEBUG
            Debug.UpdateDebugRectangle(mouseState);
#endif
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Managers.screenManager.Draw(spriteBatch);

#if DEBUG
            if (Debug.debugRectangle != Rectangle.Empty)
            {
                spriteBatch.Begin();
                spriteBatch.DrawRectangle(Debug.debugRectangle, Color.Black * .25f);
                spriteBatch.DrawString(Debug.spriteFont, "" + Debug.debugRectangle, new Vector2(Debug.debugRectangle.X, Debug.debugRectangle.Y), Color.Black);
                spriteBatch.End();
            }
#endif
        }
    }
}
