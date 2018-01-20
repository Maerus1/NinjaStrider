using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NinjaStrider
{
    class BackgroundScroller : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Rectangle srcRect;
        private Vector2 position1, position2;
        private Vector2 speed;

        public BackgroundScroller(Game game, SpriteBatch spriteBatch,
            Texture2D tex, Rectangle srcRect, Vector2 position,
            Vector2 speed) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.srcRect = srcRect;
            this.position1 = position;
            this.position2 = new Vector2(position1.X + tex.Width,
                position1.Y);
            this.speed = speed;
        }

        public override void Update(GameTime gameTime)
        {
            position1 -= speed;
            position2 -= speed;
            if (position1.X < -tex.Width)
            {
                position1.X = position2.X + tex.Width;
            }
            if (position2.X < -tex.Width)
            {
                position2.X = position1.X + tex.Width;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position1, srcRect, Color.White);
            spriteBatch.Draw(tex, position2, srcRect, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
