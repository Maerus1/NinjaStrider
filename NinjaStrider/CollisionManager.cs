using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaStrider
{
    class CollisionManager : GameComponent
    {
        private Hero hero;
        private Enemy enemy;
        private Health health;
        private SoundEffect damageSound;
        private const int healthSize = 51;
        private Game1 game;
        private float killTimer = 6500;

        public CollisionManager(Game game,
            Hero hero, Enemy enemy,
            Health health,
            SoundEffect damageSound) : base(game)
        {
            this.hero = hero;
            this.enemy = enemy;
            this.damageSound = damageSound;
            this.health = health;
            this.game = (Game1)game;
        }

        public override void Update(GameTime gameTime)
        {
            if (enemy != null && !Shared.godMode)
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                killTimer -= elapsed;
                if (enemy.getBounds().Intersects(hero.getBounds()) && enemy.Visible)
                {
                    damageSound.Play();
                    enemy.Visible = false;
                    enemy.Enabled = false;
                    health.position.X += healthSize;
                    if (health.position.X > Shared.stage.X - healthSize)
                    {
                        Shared.gameOver = true;
                    }
                }

                if (!Shared.actionScene)
                {
                    enemy.Visible = false;
                    enemy.Enabled = false;
                }
                if (killTimer < 0)
                {
                    enemy.Visible = false;
                    enemy.Dispose();
                    enemy = null;

                }
            }

            base.Update(gameTime);
        }
    }
}
