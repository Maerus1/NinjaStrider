using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace NinjaStrider
{
    class GameOverScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Song gameOverSong;
        private SpriteFont scoreFont;
        private Vector2 scorePos;
        private string message;
        private const int SCORETEXTY = 100;
        public GameOverScene(Game game,
            SpriteBatch spriteBatch) : base(game)
        {
            game = (Game1)game;
            this.spriteBatch = spriteBatch;
            tex = game.Content.Load<Texture2D>("Images/gameover");
            gameOverSong = game.Content.Load<Song>("Audio/gameover");
            scoreFont = game.Content.Load<SpriteFont>("Fonts/scoreFont");
            scorePos = new Vector2(Shared.stage.X / 3.3f, SCORETEXTY);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            message = "Your final score: " + Shared.score.ToString();
            spriteBatch.Begin();
            spriteBatch.Draw(tex, Vector2.Zero, Color.White);
            spriteBatch.DrawString(scoreFont, message, scorePos, Color.Red);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void playSong()
        {
            MediaPlayer.Play(gameOverSong);
            MediaPlayer.IsRepeating = false;
        }
        public void stopSong()
        {
            MediaPlayer.Stop();
        }
    }
}
