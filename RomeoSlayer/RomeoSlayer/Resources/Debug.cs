#define DEBUG

#if DEBUG
#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using E2DEngine.Input;
using E2DEngine.Helpers;
#endregion

namespace RomeoSlayer.Resources
{
    class Debug
    {
        public static SpriteFont spriteFont;

        private static Vector2 clickPosition = Vector2.Zero;
        public static Rectangle debugRectangle = Rectangle.Empty;

        private static float speed = 1;
        public static float Speed { get { return speed; } }

        public static void UpdateChangeableSpeed(E2DKeyboardState keyboardState)
        {
            if (keyboardState.KeyPressed(Keys.Up))
                speed = MathHelper.Min(speed * 2, 4);

            else if (keyboardState.KeyPressed(Keys.Down))
                speed = MathHelper.Max(speed / 2, 0.25f);
        }

        #region Update The Debug Rectangle
        /// <summary>
        /// Creating a rectangle with your mouse in game and get the properties.
        /// </summary>
        #endregion
        public static void UpdateDebugRectangle(E2DMouseState mouseState)
        {
            mouseState.Begin();

            if (mouseState.RightButtonPressed)
                clickPosition = mouseState.Position;

            if (mouseState.RightButtonReleased)
            {
                Console.Write("Debug Rectangle: {0}", debugRectangle);
                Console.WriteLine(" ({0}, {1}, {2}, {3})", debugRectangle.X, debugRectangle.Y, debugRectangle.Width, debugRectangle.Height);
            }

            if (mouseState.RightButtonHold)
                debugRectangle = MathHelp.RectangleFromTwoVectors(clickPosition, mouseState.Position);
            else
                debugRectangle = Rectangle.Empty;

            mouseState.End();
        }
    }
}
#endif
