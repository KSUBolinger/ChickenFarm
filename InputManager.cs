using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ChickenFarm
{
    public class InputManager
    {
        KeyboardState currentKeyboard;
        KeyboardState priorKeyboard;

        public Vector2 Direction { get; private set; }

        public bool ExitState { get; private set; }

        public void Update(GameTime gameTime)
        {
            priorKeyboard = currentKeyboard;
            currentKeyboard = Keyboard.GetState();
        }

        /// <summary>
        /// checks to see if the sprite should be in motion
        /// </summary>
        /// <returns>boolean representing if in motion</returns>
        public bool MotionCheck()
        {
            if (currentKeyboard.IsKeyDown(Keys.W) && (priorKeyboard.IsKeyUp(Keys.W) || currentKeyboard.IsKeyDown(Keys.W)))
            {
                return true;
            }
            else if (currentKeyboard.IsKeyDown(Keys.S) && priorKeyboard.IsKeyUp(Keys.S))
            {
                return true;
            }
            else if (currentKeyboard.IsKeyDown(Keys.A) && priorKeyboard.IsKeyUp(Keys.A))
            {
                return true;
            }
            else if (currentKeyboard.IsKeyDown(Keys.D) && priorKeyboard.IsKeyUp(Keys.D))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
