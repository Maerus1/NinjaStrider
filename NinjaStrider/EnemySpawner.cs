using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaStrider
{
    class EnemySpawner : DrawableGameComponent
    {
        private Game game;
        private Hero hero;
        private SpriteBatch spriteBatch;
        private float timer = 600;
        private float reverseTimer = 1500;
        private float timerReset = 600;
        private float reverseTimerReset = 1500;
        private Random randYPos;
        private Random speed;
        private Texture2D scorebar;
        private Health health;
        public EnemySpawner(Game game, Hero hero,
            SpriteBatch spriteBatch,
            Texture2D scorebar, Health health) : base(game)
        {
            this.game = game;
            this.hero = hero;
            this.spriteBatch = spriteBatch;
            this.randYPos = new Random();
            this.speed = new Random();
            this.scorebar = scorebar;
            this.health = health;
        }

        public override void Update(GameTime gameTime)
        {
            float elapsed = gameTime.ElapsedGameTime.Milliseconds;
            timer -= elapsed;
            if (Shared.gameTimer < 500)
            {
                timerReset = 600;
                reverseTimerReset = 1500;
            }
            if (Shared.gameTimer > 500)
            {
                reverseTimer -= elapsed;
            }
            if (Shared.gameTimer > 1000)
            {
                reverseTimerReset = 1200;
                timerReset = 500;
            }
            if (Shared.gameTimer > 2000)
            {
                reverseTimerReset = 1000;
                timerReset = 400;
            }
            if (Shared.gameTimer > 3000)
            {
                reverseTimerReset = 800;
                timerReset = 300;
            }
            if (Shared.gameTimer > 4000)
            {
                reverseTimerReset = 650;
                timerReset = 200;
            }
            if (timer < 0)
            {

                //enemy going left
                Texture2D enemyTex = game.Content.Load<Texture2D>("Images/enemy");
                Vector2 enemyPos = new Vector2(Shared.stage.X, randYPos.Next(0, (int)Shared.stage.Y) + scorebar.Height);
                Vector2 enemySpeed = new Vector2(speed.Next(8, 30), 0);
                int enemyWidth = 41;
                int enemyHeight = 34;
                Enemy enemy = new Enemy(game, spriteBatch, enemyTex, enemyPos, enemySpeed, enemyWidth, enemyHeight);
                game.Components.Add(enemy);

                //collision
                SoundEffect damageSound = game.Content.Load<SoundEffect>("Audio/damage");
                CollisionManager cm = new CollisionManager(game, hero, enemy, health, damageSound);

                game.Components.Add(cm);

                timer = timerReset;
            }
            if (reverseTimer < 0)
            {
                //enemy going right
                Texture2D enemyTex = game.Content.Load<Texture2D>("Images/enemy");
                Vector2 enemyPos = new Vector2(0 - enemyTex.Width, randYPos.Next(0, (int)Shared.stage.Y) + scorebar.Height);
                Vector2 enemySpeed = new Vector2(-speed.Next(8, 30), 0);
                int enemyWidth = 41;
                int enemyHeight = 34;
                Enemy enemy = new Enemy(game, spriteBatch, enemyTex, enemyPos, enemySpeed, enemyWidth, enemyHeight);
                game.Components.Add(enemy);

                //collision
                SoundEffect damageSound = game.Content.Load<SoundEffect>("Audio/damage");
                CollisionManager cm = new CollisionManager(game, hero, enemy, health, damageSound);

                game.Components.Add(cm);

                reverseTimer = reverseTimerReset;
            }

            base.Update(gameTime);
        }
    }
}
