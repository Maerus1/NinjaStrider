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
    class Hero : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 speed;
        private Rectangle sourceRect;
        private int width;
        private int height;
        private int frameUpdater;
        private float timer = 50;
        private const float TIMER = 50;
        private float startingY;
        private float jumpSpeed;
        private float constantJumpSpeed;
        private bool jumping;
        private SoundEffect jump;
        private const int hitboxForgiveness = 5;
        private Texture2D scorebar;
        private const int JUMPINGFRAME = 5;


        public Hero(Game game, SpriteBatch spriteBatch,
    Texture2D tex, Vector2 position, Vector2 speed,
    int width, int height, float jumpHeight, Texture2D scorebar) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;

            this.width = width;
            this.speed = speed;
            this.height = height;
            this.sourceRect = new Rectangle(0, 0, width, height);

            this.jumping = false;
            this.startingY = position.Y;
            this.constantJumpSpeed = jumpHeight;
            this.jumpSpeed = 0;
            jump = game.Content.Load<SoundEffect>("Audio/jump");
            this.scorebar = scorebar;
            this.frameUpdater = 0;
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.D))
            {
                position.X += speed.X;
            }
            if (ks.IsKeyDown(Keys.A))
            {
                position.X -= speed.X;
            }
            if (position.X + sourceRect.Width > Shared.stage.X)
            {
                position.X = Shared.stage.X - sourceRect.Width;
            }
            if (position.X < 0)
            {
                position.X = 0;
            }
            if (position.Y + sourceRect.Height > Shared.stage.Y)
            {
                position.Y = Shared.stage.Y - sourceRect.Height;
            }
            if (position.Y < scorebar.Height)
            {
                position.Y = scorebar.Height;
            }

            if (jumping)
            {
                sourceRect.X = JUMPINGFRAME * width;
                if (ks.IsKeyDown(Keys.Space))
                {
                    position.Y += jumpSpeed;
                }
                position.Y -= jumpSpeed / 4;
                //position.Y += jumpSpeed;
                //jumpSpeed += 1;
                //if (ks.IsKeyDown(Keys.Space))
                //{
                //    position.Y -= 1;
                //}
                if (position.Y >= startingY)
                {
                    position.Y = startingY;
                    jumping = false;
                }
            }
            else
            {
                if (ks.IsKeyDown(Keys.Space))
                {
                    jumping = true;
                    jumpSpeed = -(Math.Abs(constantJumpSpeed));
                    jump.Play();
                }
            }
            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            timer -= elapsed;
            if (!jumping)
            {

                if (timer < 0)
                {
                    if (frameUpdater < 9)
                    {
                        sourceRect.Y = 0;
                        sourceRect.X = ++frameUpdater * width;
                    }
                    if (frameUpdater == 9)
                    {
                        frameUpdater = 0;
                    }

                    timer = TIMER;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, sourceRect, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, sourceRect.Width, sourceRect.Height - hitboxForgiveness);
        }

        public void setPosition(Vector2 pos)
        {
            this.position.X = pos.X;
            this.position.Y = pos.Y;
        }
    }
}

