using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaStrider
{
    class Enemy : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 speed;
        private Rectangle sourceRect;
        private int width;
        private int height;
        private float timer = 50;
        private const float TIMER = 50;

        private Vector2 origin;
        private float rotation = 0.0f;
        private const int hitboxForgiveness = 30;
        public Enemy(Game game, SpriteBatch spriteBatch,
    Texture2D tex, Vector2 position, Vector2 speed,
    int width, int height) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.width = width;
            this.speed = speed;
            this.height = height;
            this.sourceRect = new Rectangle(0, 0, width, height);
            this.origin = new Vector2(tex.Width / 2, tex.Height / 2);

        }

        public override void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            timer -= elapsed;

            if (timer < 0)
            {
                rotation += 0.2f;
                position -= speed;

                timer = TIMER;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack);
            spriteBatch.Draw(tex, position, sourceRect, Color.White, rotation, origin, 1, SpriteEffects.None, 0.25f);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X - hitboxForgiveness, (int)position.Y, tex.Width, tex.Height - hitboxForgiveness);
        }
    }
}

