using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static pong.objects.Paddle;

namespace pong.objects
{
    public class GameCounter
    {
        private int LeftScore = 0;
        private int RightScore = 0;
        private SpriteFont Font;

        public GameCounter(SpriteFont font)
        {
            Font = font;
        }

        public void PlayerScore(Player player)
        {
            _ = player == Player.Left ? LeftScore++ : RightScore++;

        }

        public void Draw(SpriteBatch spriteBatch, Viewport viewport )
        {
            string text = LeftScore.ToString() + " : " + RightScore.ToString();
            Vector2 size = Font.MeasureString(text);
            Vector2 position = new Vector2(viewport.Width / 2, 25);
            position.X -= size.X / 2;
            spriteBatch.DrawString(Font, text, position, Color.White);

        }
    }




}