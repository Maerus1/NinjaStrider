using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace NinjaStrider
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ActionScene actionScene;
        GameOverScene gameOverScene;
        StartScene startScene;
        HelpScene helpScene;
        CreditsScene creditsScene;
        AboutScene aboutScene;
        HighScoreScene highScoreScene;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Shared.stage = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            Shared.gameOver = false;
            Shared.actionScene = false;
            base.Initialize();
        }

        private void hideAllScenes()
        {
            GameScene gs = null;
            foreach (GameComponent item in Components)
            {
                if (item is GameScene)
                {
                    gs = (GameScene)item;
                    gs.hide();
                }
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            startScene = new StartScene(this, spriteBatch);
            Components.Add(startScene);

            gameOverScene = new GameOverScene(this, spriteBatch);
            Components.Add(gameOverScene);

            actionScene = new ActionScene(this, spriteBatch);
            Components.Add(actionScene);

            helpScene = new HelpScene(this, spriteBatch);
            Components.Add(helpScene);

            creditsScene = new CreditsScene(this, spriteBatch);
            Components.Add(creditsScene);

            aboutScene = new AboutScene(this, spriteBatch);
            Components.Add(aboutScene);

            highScoreScene = new HighScoreScene(this, spriteBatch);
            Components.Add(highScoreScene);

            startScene.playSong();
            startScene.show();
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            int selectedIndex = 0;
            KeyboardState ks = Keyboard.GetState();

            if (startScene.Enabled)
            {
                selectedIndex = startScene.Menu.selectedIndex;

                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    Shared.actionScene = true;
                    actionScene.show();
                    actionScene.reset();
                    startScene.stopSong();
                }
                else if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    helpScene.show();
                }
                else if (selectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    highScoreScene.updateHighScore();
                    highScoreScene.show();
                }
                else if (selectedIndex == 3 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    creditsScene.show();
                }
                else if (selectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    aboutScene.show();
                }
                else if (selectedIndex == 5 && ks.IsKeyDown(Keys.Enter))
                {
                    this.Exit();
                }
            }
            else if (gameOverScene.Enabled || actionScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    Shared.actionScene = false;
                    hideAllScenes();
                    startScene.show();
                    actionScene.stopSong();
                    gameOverScene.stopSong();
                    startScene.playSong();
                    Shared.godMode = false;
                    Shared.activatedGodMode = false;
                }
            }
            else if (helpScene.Enabled || creditsScene.Enabled || aboutScene.Enabled || highScoreScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    hideAllScenes();
                    startScene.show();

                }
            }

            //game over
            if (Shared.gameOver)
            {
                HighScoreManager.saveScore();
                Shared.actionScene = false;
                actionScene.stopSong();
                hideAllScenes();
                gameOverScene.show();
                gameOverScene.playSong();
                Shared.gameOver = false;
                Shared.gameTimer = 0;
                highScoreScene.updateHighScore();
                Shared.godMode = false;
                Shared.activatedGodMode = false;
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
