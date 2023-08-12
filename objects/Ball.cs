using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static pong.objects.Paddle;
using static pong.Pong;

namespace pong.objects
{
    public class Ball
    {
        private Player CurrentPlayer { get; set; }
        private Rectangle Bounds { get; set; }
        private Color BallColor { get; set; }
        private int BallWidth { get; set; }
        private int BallHeight { get; set; }
        private int _screenHeight;
        private int _screenWidth;
        private bool locked = true;

        public Ball(Viewport viewport , Player startingPlayer)
        {
            _screenHeight = viewport.Height;
            _screenWidth = viewport.Width;
            CurrentPlayer = startingPlayer;
            var paddleWidth = Paddle.PaddleWidth;
            var screenCenter = _screenHeight / 2 - BallHeight / 2;
            BallWidth = 10;
            BallHeight = 10;
            BallColor = Color.Red;
            var x = CurrentPlayer == Player.Left ? BallWidth + paddleWidth : _screenWidth - BallWidth - paddleWidth - 10;
            Bounds = new Rectangle(x, screenCenter, BallWidth, BallHeight);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D _texture2D)
        {
            spriteBatch.Draw(_texture2D, Bounds, BallColor);
        }

        public void Move(Direction y, Player player)
        {
            if (locked && player == CurrentPlayer)
            {
                var yAxis = Bounds.Y + (int)y;
                Bounds = new Rectangle(Bounds.X, yAxis, BallWidth, BallHeight);
            }
        }

        public void DetectPosition(Paddle rightPaddle, Paddle leftPaddle)
        {
            // if (!locked && Bounds.Intersects(paddle.Bounds))
            // {
            //     //CurrentPlayer = paddle.PlayerColor == Color.Blue ? Player.Left : Player.Right;
            // }

            if(Bounds.X < 0 || Bounds.X > _screenWidth - BallWidth)
            {
                //Point for current player;
            }

            if(Bounds.Y < 0 || Bounds.Y > _screenHeight - BallHeight)
            {
                //Make bounce
            }
        }
    }
}