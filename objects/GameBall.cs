using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static pong.objects.InputHandler;
using static pong.objects.Paddle;
namespace pong.objects
{
    public class GameBall
    {
        List<SoundEffect> soundEffects = new List<SoundEffect>(5);
        SoundEffect activatedEffect;
        SoundEffect missionFailedEffect;
        private Player CurrentPlayer { get; set; }
        private Paddle CurrentPaddle { get; set; }
        private float BallSpeed;
        private float Deflection = 15.0f;
        private Vector2 BallVelocity { get; set; }
        private Rectangle Bounds { get; set; }
        private Color BallColor { get; set; }
        private int BallWidth { get; set; }
        private int BallHeight { get; set; }
        private int ScreenHeight;
        private int ScreenWidth;
        private Random _random = new Random();
        public bool Locked { get; private set; } = true;



        public GameBall(Viewport viewport, Player startingPlayer, Paddle leftPaddle, Paddle rightPaddle, ContentManager Content)
        {

            CurrentPlayer = startingPlayer;
            CurrentPaddle = CurrentPlayer == Player.Left ? leftPaddle : rightPaddle;
            ScreenHeight = viewport.Height;
            ScreenWidth = viewport.Width;
            for (int i = 0; i <= 4; i++)
            {
                SoundEffect se = Content.Load<SoundEffect>($"sounds\\forceField_{i:000}");
                soundEffects.Add(se);
            }

            //Need to find new sounds these don't fit
            activatedEffect = Content.Load<SoundEffect>("sounds/laserLarge_000");
            missionFailedEffect = Content.Load<SoundEffect>("sounds/explosionCrunch_000");
            Reset();
        }

        public void Reset()
        {
            Locked = true;
            BallSpeed = 20.0f;
            var paddleWidth = Paddle.PaddleWidth;
            BallWidth = 10;
            BallHeight = 10;
            BallColor = Color.Red;
            BallVelocity = new Vector2(0, 0);

            var x = CurrentPlayer == Player.Left ? BallWidth + paddleWidth : ScreenWidth - BallWidth - paddleWidth - 10;

            // Adjust the Y position based on the passed paddle's vertical center
            var ballMiddleY = CurrentPaddle.Bounds.Y + CurrentPaddle.Bounds.Height / 2 - BallHeight / 2;

            Bounds = new Rectangle(x, ballMiddleY, BallWidth, BallHeight);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D _texture2D)
        {
            spriteBatch.Draw(_texture2D, Bounds, BallColor);
        }

        public void Move(Paddle rightPaddle, Paddle leftPaddle, GameCounter counter, float deltaTime)
        {
            if (Locked)
            {
                Reset();
                return;
            }
            Bounds = new Rectangle(
                Bounds.X + (int)(BallVelocity.X * BallSpeed * deltaTime),
                Bounds.Y + (int)(BallVelocity.Y * BallSpeed * deltaTime),
                BallWidth,
                BallHeight
            );
            DetectPosition(rightPaddle, leftPaddle, counter);
        }

        public void Launch(Direction paddleDirection)
        {
            if (Locked)
            {
                activatedEffect.Play();
                Locked = false;
                var speed = CurrentPlayer == Player.Left ? BallSpeed : -BallSpeed;
                float deflection = (float)paddleDirection * Deflection;
                BallVelocity = new Vector2(speed, deflection);
            }

        }

        public void PlayRandomSound()
        {
            int index = _random.Next(0, 4);
            soundEffects[index].Play();
        }

        public void DetectPosition(Paddle rightPaddle, Paddle leftPaddle, GameCounter counter)
        {
            bool collision = false;

            if (Bounds.Intersects(leftPaddle.Bounds) && BallVelocity.X < 0)
            {
                collision = true;
                CurrentPaddle = leftPaddle;
                CurrentPlayer = Player.Left;
            }
            else if (Bounds.Intersects(rightPaddle.Bounds) && BallVelocity.X > 0)
            {
                collision = true;
                CurrentPaddle = rightPaddle;
                CurrentPlayer = Player.Right;
            }

            if (collision)
            {
                PlayRandomSound();
                BallVelocity = new Vector2(
                    -BallVelocity.X,
                    BallVelocity.Y + (float)CurrentPaddle.PaddleDirection * Deflection
                );
                BallSpeed = Math.Min(BallSpeed + 1.0f, 55.0f);
            }

            if (Bounds.X < 0 || Bounds.X > ScreenWidth - BallWidth)
            {
                missionFailedEffect.Play();
                Reset();
                counter.PlayerScore(CurrentPlayer);
                return;

            }

            if (Bounds.Y < 0 || Bounds.Y > ScreenHeight - BallHeight)
            {
                PlayRandomSound();
                BallVelocity = new Vector2(
                    BallVelocity.X,  // retain horizontal direction
                    -BallVelocity.Y  // reverse vertical direction
                );
            }
        }
    }
}