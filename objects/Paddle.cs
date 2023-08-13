using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static pong.objects.InputHandler;

namespace pong.objects
{


    public class Paddle
    {
        public enum Player
        {
            Left,
            Right
        }
        public Rectangle Bounds { get; private set; }
        private Color PlayerColor { get; set; }
        private const float PLAYER_SPEED = 500.0f;
        public static int PaddleWidth = 10;
        private int PaddleHeight = 161;
        private int _screenHeight;
        private int _screenWidth;
        private Vector2 PlayerPosition;
        public Direction PaddleDirection { get; private set; }

        public Paddle(Player playerSide, Color color, Viewport viewport)
        {
            _screenHeight = viewport.Height;
            _screenWidth = viewport.Width;
            var screenCenter = _screenHeight / 2 - PaddleHeight / 2;
            var playerX = playerSide == Player.Left ? 10 : _screenWidth - PaddleWidth - 10;
            PlayerPosition = new Vector2(playerX, screenCenter);
            Bounds = new Rectangle((int)PlayerPosition.X, (int)PlayerPosition.Y, PaddleWidth, PaddleHeight);
            PlayerColor = color;
            
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D _texture2D)
        {
            spriteBatch.Draw(_texture2D, Bounds, PlayerColor);
        }

        public void Move(Direction y, float deltaTime)
        {
            PaddleDirection = y;

            // Modify the PlayerPosition based on the direction and speed
            PlayerPosition.Y += (float)y * PLAYER_SPEED * deltaTime;

            // Check boundaries and correct if necessary
            if (PlayerPosition.Y < 0)
            {
                PlayerPosition.Y = 0;
            }
            else if (PlayerPosition.Y > _screenHeight - PaddleHeight)
            {
                PlayerPosition.Y = _screenHeight - PaddleHeight;
            }

            // Update the Bounds using the PlayerPosition
            Bounds = new Rectangle((int)PlayerPosition.X, (int)PlayerPosition.Y, PaddleWidth, PaddleHeight);
        }

    }


}