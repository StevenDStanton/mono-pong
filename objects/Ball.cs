using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static pong.objects.InputHandler;
using static pong.objects.Paddle;
namespace pong.objects
{
    public class Ball
    {
        private Player CurrentPlayer { get; set; }
        private Rectangle Bounds { get; set; }
        private Color BallColor { get; set; }
        private int BallWidth { get; set; }
        private int BallHeight { get; set; }
        private Vector2 Velocity { get; set; }
        private int ScreenHeight;
        private int ScreenWidth;
        public bool Locked { get; private set; } = true;
        private float Speef = 1.0f;
        private float Deflection = 1.0f;


        public Ball(Viewport viewport, Player startingPlayer)
        {
            ScreenHeight = viewport.Height;
            ScreenWidth = viewport.Width;
            CurrentPlayer = startingPlayer;
            var paddleWidth = Paddle.PaddleWidth;
            var screenCenter = ScreenHeight / 2 - BallHeight / 2;
            BallWidth = 10;
            BallHeight = 10;
            BallColor = Color.Red;
            var x = CurrentPlayer == Player.Left ? BallWidth + paddleWidth : ScreenWidth - BallWidth - paddleWidth - 10;
            Bounds = new Rectangle(x, screenCenter, BallWidth, BallHeight);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D _texture2D)
        {
            spriteBatch.Draw(_texture2D, Bounds, BallColor);
        }

        public void FollowPaddle(Direction y, Player player)
        {
            if (player == CurrentPlayer)
            {
                var yAxis = Bounds.Y + (int)y;
                Bounds = new Rectangle(Bounds.X, yAxis, BallWidth, BallHeight);
                return;
            }
        }

        public void Move()
        {
            Console.WriteLine($"Velocity: {Velocity}");
            Bounds = new Rectangle(Bounds.X + (int)Velocity.X, Bounds.Y + (int)Velocity.Y, BallWidth, BallHeight);
        }

        public void Launch(Direction paddleDirection)
        {
            if (Locked)
            {
                Locked = false;
                var speed = CurrentPlayer == Player.Left ? Speef : -Speef;
                float deflection = (float)paddleDirection * Deflection;
                Console.WriteLine($"Paddle Direction: {paddleDirection}, Deflection: {deflection}");
                Velocity = new Vector2(speed, deflection);
            }

        }

        public void DetectPosition(Paddle rightPaddle, Paddle leftPaddle)
        {
            if (!Locked)
            {
                //CurrentPlayer = paddle.PlayerColor == Color.Blue ? Player.Left : Player.Right;
            }

            if (Bounds.X < 0 || Bounds.X > ScreenWidth - BallWidth)
            {
                //Point for current player;
            }

            if (Bounds.Y < 0 || Bounds.Y > ScreenHeight - BallHeight)
            {
                //Make bounce
            }
        }
    }
}