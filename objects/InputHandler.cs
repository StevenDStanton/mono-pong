using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace pong.objects
{
    public class InputHandler
    {
        private bool _LeftUpPressed = false;
        private bool _LeftDownPressed = false;
        private bool _RightUpPressed = false;
        private bool _RightDownPressed = false;

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
                _LeftUpPressed = true;
                return Direction.Up;
            }
            else if (_LeftUpPressed && keyboardState.IsKeyUp(Keys.W))
            {
                _LeftUpPressed = false;
                return Direction.None;
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                _LeftDownPressed = true;
                return Direction.Down;
            }
            else if (_LeftDownPressed && keyboardState.IsKeyUp(Keys.S))
            {
                _LeftDownPressed = false;
                return Direction.None;
            }

            return Direction.None; // Default case
        }

        public Direction GetRightPaddleDirection()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                _RightUpPressed = true;
                return Direction.Up;
            }
            else if (_RightUpPressed && keyboardState.IsKeyUp(Keys.Up))
            {
                _RightUpPressed = false;
                return Direction.None;
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                _RightDownPressed = true;
                return Direction.Down;
            }
            else if (_RightDownPressed && keyboardState.IsKeyUp(Keys.Down))
            {
                _RightDownPressed = false;
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