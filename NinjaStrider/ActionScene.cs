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
    class ActionScene : GameScene
    {
        private Hero hero;
        private SpriteBatch spriteBatch;
        private GameText scoreText;
        private Game1 game;
        private Song mainSong;
        private Vector2 heroPos;
        private Vector2 heroSpeed;
        //hero width and height
        private const int WIDTH = 64;
        private const int HEIGHT = 79;
        private const int HEROHEIGHTBUFFER = 10;
        private const float HEROJUMPSPEED = 15;
        private Texture2D infobarTex;
        private Texture2D heroTex;
        private Texture2D healthTex;
        private Vector2 healthPos;
        private Health health;
        private KeyboardState oldState;
        private GameText godModeMessage;

        public ActionScene(Game game,
            SpriteBatch spriteBatch) : base(game)
        {
            this.game = (Game1)game;
            this.spriteBatch = spriteBatch;
            //put these into the action scene
            mainSong = game.Content.Load<Song>("Audio/mainLevel");

            heroTex = game.Content.Load<Texture2D>("Spritesheets/hero");

            Texture2D backgroundTex = game.Content.Load<Texture2D>("Images/background");
            Rectangle backgroundSrcRect = new Rectangle(0, 0, backgroundTex.Width,
                backgroundTex.Height);

            Vector2 backgroundSpeed = new Vector2(1, 0);
            Vector2 backgroundPosition = new Vector2(0, Shared.stage.Y - backgroundSrcRect.Height);

            BackgroundScroller background = new BackgroundScroller(game, spriteBatch,
                backgroundTex, backgroundSrcRect, backgroundPosition, backgroundSpeed);

            this.Components.Add(background);

            Texture2D foregroundTex = game.Content.Load<Texture2D>("Images/foreground");
            Rectangle foregroundSrcRect = new Rectangle(0, 0, foregroundTex.Width,
                foregroundTex.Height);

            Vector2 foregroundSpeed = new Vector2(5, 0);
            Vector2 foregroundPosition = new Vector2(0, Shared.stage.Y - foregroundSrcRect.Height);

            BackgroundScroller foreground = new BackgroundScroller(game, spriteBatch,
                foregroundTex, foregroundSrcRect, foregroundPosition, foregroundSpeed);

            this.Components.Add(foreground);


            infobarTex = game.Content.Load<Texture2D>("Images/infobar");
            Vector2 infobarPos = Vector2.Zero;

            Infobar infobar = new Infobar(game, spriteBatch, infobarTex, infobarPos);
            this.Components.Add(infobar);

            //so the main character isn't hugging the bottom of the screen


            heroPos = new Vector2(0, Shared.stage.Y - (heroTex.Height + HEROHEIGHTBUFFER));

            heroSpeed = new Vector2(8, 5);


            hero = new Hero(game, spriteBatch, heroTex, heroPos, heroSpeed, WIDTH, HEIGHT, HEROHEIGHTBUFFER, infobarTex);
            this.Components.Add(hero);


            SpriteFont scoreFont = game.Content.Load<SpriteFont>("Fonts/scoreFont");
            SpriteFont messageFont = game.Content.Load<SpriteFont>("Fonts/selectFont");
            Vector2 scoreTextPos = Vector2.Zero;
            string scoreTextMessage = "Score: " + Shared.score.ToString();

            scoreText = new GameText(game, spriteBatch, scoreFont, scoreTextPos, scoreTextMessage, Color.White);

            this.Components.Add(scoreText);

            healthTex = game.Content.Load<Texture2D>("Images/health");
            healthPos = new Vector2(Shared.stage.X - healthTex.Width, 0);

            health = new Health(game, spriteBatch, healthTex, healthPos);

            this.Components.Add(health);


            EnemySpawner spawner = new EnemySpawner(game, hero, spriteBatch, infobarTex, health);

            this.Components.Add(spawner);

            Vector2 godModeMessagePos = new Vector2(Shared.stage.X / 3, 0);
            string godModeMessageString = "Moo";

            godModeMessage = new GameText(game, spriteBatch, scoreFont, godModeMessagePos, godModeMessageString, Color.Red);
            this.Components.Add(godModeMessage);
        }

        public override void Update(GameTime gameTime)
        {
            Shared.gameTimer++;

            Shared.score++;
            scoreText.message = "Score: " + Shared.score.ToString();
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.OemTilde) && oldState.IsKeyUp(Keys.OemTilde))
            {
                Shared.godMode = !(Shared.godMode);
                Shared.activatedGodMode = true;
            }
            if (Shared.godMode)
            {
                godModeMessage.message = "God mode Enabled!";
            }
            else
            {
                godModeMessage.message = "";
            }
            oldState = ks;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
        public void reset()
        {
            MediaPlayer.Play(mainSong);
            MediaPlayer.IsRepeating = true;
            Shared.score = 1;
            Shared.gameTimer = 0;
            hero.setPosition(new Vector2(0, Shared.stage.Y - heroTex.Height));
            health.setPosition(new Vector2(Shared.stage.X - healthTex.Width, 0));
        }
        public void stopSong()
        {
            MediaPlayer.Stop();
        }
    }
}
