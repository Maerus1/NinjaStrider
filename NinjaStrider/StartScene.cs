using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaStrider
{
    class StartScene : GameScene
    {
        private Song mainMenuSong;
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        public MenuComponent Menu { get; set; }
        string[] menuItems = {"Start Game",
                              "Help",
                              "High Score",
                              "Credits",
                              "About",
                              "Quit"};
        public StartScene(Game game,
            SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            mainMenuSong = game.Content.Load<Song>("Audio/mainmenu");
            SpriteFont regularFont = game.Content.Load<SpriteFont>("Fonts/regularFont");
            SpriteFont selectFont = game.Content.Load<SpriteFont>("Fonts/selectFont");
            Menu = new MenuComponent(game, spriteBatch,
                regularFont, selectFont, menuItems);
            tex = game.Content.Load<Texture2D>("Images/mainscreen");
            this.Components.Add(Menu);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, Vector2.Zero, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void playSong()
        {
            MediaPlayer.Play(mainMenuSong);
            MediaPlayer.IsRepeating = true;
        }
        public void stopSong()
        {

            MediaPlayer.Stop();
        }
    }
}
