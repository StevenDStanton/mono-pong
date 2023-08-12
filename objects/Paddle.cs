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
        public static int PaddleWidth = 10;
        private int PaddleHeight = 161;
        private int _screenHeight;
        private int _screenWidth;
        public Direction PaddleDirection { get; private set;}

        public Paddle(Player player, Color color, Viewport viewport)
        {
            _screenHeight = viewport.Height;
            _screenWidth = viewport.Width;
            var screenCenter = _screenHeight / 2 - PaddleHeight / 2;
            var x = player == Player.Left ? 10 : _screenWidth - PaddleWidth -10;
            Bounds = new Rectangle(x, screenCenter, PaddleWidth, PaddleHeight);
            PlayerColor = color;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D _texture2D)
        {
            spriteBatch.Draw(_texture2D, Bounds, PlayerColor);
        }

        public void Move(Direction y)
        {
            PaddleDirection = y;
            if(Bounds.Y + (int)y   < 0 || Bounds.Y + (int)y > _screenHeight - PaddleHeight )
            {
                return;
            }
            var yAxis = Bounds.Y + (int)y;
            Bounds = new Rectangle(Bounds.X, yAxis, PaddleWidth, PaddleHeight);

        }
    }


}