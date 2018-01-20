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
    class GameText : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont gameFont;
        private Vector2 position;
        public string message { get; set; }
        private Color color;


        public GameText(Game game, SpriteBatch spriteBatch,
            SpriteFont gameFont,
            Vector2 position, string message, Color color) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.gameFont = gameFont;
            this.position = position;
            this.message = message;
            this.color = color;

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(gameFont, message, position, color);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
