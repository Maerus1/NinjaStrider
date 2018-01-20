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
    class HighScoreScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private GameText highScores;
        private const int SCOREHEIGHT = 200;
        private List<int> highScoreList;
        private string scoreMessage = "";
        private SpriteFont systemFont;
        private Vector2 systemPos;
        private GameText systemMessage;
        private Game1 game;
        public HighScoreScene(Game game,
            SpriteBatch spriteBatch) : base(game)
        {
            this.game = (Game1)game;
            this.spriteBatch = spriteBatch;
            tex = game.Content.Load<Texture2D>("Images/highscores");
            SpriteFont scoreFont = game.Content.Load<SpriteFont>("Fonts/scoreFont");
            Vector2 scorePos = new Vector2(Shared.stage.X / 2.15f, SCOREHEIGHT);

            updateHighScore();


            highScores = new GameText(game, spriteBatch, scoreFont, scorePos, scoreMessage, Color.Red);
            this.Components.Add(highScores);


            Vector2 noticePos = new Vector2(0, Shared.stage.Y - 25);
            string noticeMessage = "If you used God mode, your score is not recorded. Cheaters don't get high scores ;P";


            GameText godModeNotice = new GameText(game, spriteBatch, scoreFont, noticePos, noticeMessage, Color.Red);
            this.Components.Add(godModeNotice);

            systemFont = game.Content.Load<SpriteFont>("Fonts/regularFont");
            systemPos = new Vector2(Shared.stage.X / 3, scorePos.Y);
            systemMessage = new GameText(game, spriteBatch, systemFont, systemPos, Shared.systemMessage, Color.White);
            this.Components.Add(systemMessage);
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

        public void updateHighScore()
        {
            highScoreList = HighScoreManager.loadScores();
            if (systemMessage != null)
            {
                systemMessage.message = Shared.systemMessage;
                if (Shared.systemMessage != "")
                {
                    scoreMessage = "";
                    highScores.message = scoreMessage;
                }
                Shared.systemMessage = "";

            }
            if (highScoreList != null)
            {
                highScoreList.Sort();
                highScoreList.Reverse();
                scoreMessage = "";

                if (highScoreList.Count > 5)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        scoreMessage += (i + 1).ToString() + ". " + highScoreList[i].ToString() + "\n";
                    }
                }
                else
                {
                    for (int i = 0; i < highScoreList.Count; i++)
                    {
                        scoreMessage += (i + 1).ToString() + ". " + highScoreList[i].ToString() + "\n";
                    }
                }
                if (highScores != null)
                {
                    highScores.message = scoreMessage;
                }
            }

        }
    }
}
