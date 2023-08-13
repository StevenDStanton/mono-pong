using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace pong.objects
{
    public class InputHandler
    {
        public enum Direction
        {
            Up = -1,
            Down = 1,
            None = 0
        }

        public Direction GetLeftPaddleDirection()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.W))
            {
                return Direction.Up;
            }
            else if (keyboardState.IsKeyDown(Keys.S))
            {
                return Direction.Down;
            }
            else if (keyboardState.IsKeyUp(Keys.W) || keyboardState.IsKeyUp(Keys.S))
            {
                return Direction.None;
            }

            return Direction.None; // Default case
        }

        public Direction GetRightPaddleDirection()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                return Direction.Up;
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                return Direction.Down;
            }
            else if (keyboardState.IsKeyUp(Keys.Up) || keyboardState.IsKeyUp(Keys.Down))
            {
                return Direction.None;
            }
            return Direction.None; // Default case
        }
        public bool IsLeftLaunchPressed()
        {
            return Keyboard.GetState().IsKeyDown(Keys.D);
        }

        public bool IsRightLaunchPressed()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Left);
        }
    }
}