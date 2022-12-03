using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace eindprojectGameDev.Characters
{
    internal class Hero : IGameObject
    {
        public Vector2 Position { get; set; }
        private Health health;
        public Health Health { get => health; set => this.health = value; }
        private IInputReader inputReader;
        public Vector2 position;
        private int speed = 5;
        private Texture2D heroTexture;
        private Texture2D heartTexture;
        private List<Rectangle> hearts = new List<Rectangle>();
        private int spriteWidth = 96;
        private int spriteHeight = 96;
        private Animation currentAnimation;
        public enum HeroStates { walkingRight, walkingLeft, idle, attackingRight, attackingLeft, dead }
        public HeroStates State = HeroStates.idle;
        Animation[] animations = new Animation[4]
        {
                new Animation(), //Idle (0)
                new Animation(), //Walk (1)
                new Animation(), //Fight (2)
                new Animation() //Die (3)
        };

        public Hero(Texture2D heroTexture, Texture2D heartTexture, IInputReader inputReader)
        {
            this.heroTexture = heroTexture;
            this.inputReader = inputReader;
            this.heartTexture = heartTexture;
            position = new Vector2(100, 100);
            Health = new Health(3,100);
            this.ResetLives();
        }

        public void Update(GameTime gameTime)
        {
            Position = new Vector2((int)position.X + 50, (int)position.Y + 47);
            //health--;
            State = HeroStates.idle;
            currentAnimation = animations[0];
            var direction = inputReader.ReadMovementInput();
            if (direction.X < 0)
            {
                State = HeroStates.walkingLeft;
                currentAnimation = animations[1];
                animations[1].GetFramesFromTextureProperties(8 * 160, 1 * 96, 8, 96);
            }
            else if (direction.X > 0)
            {
                State = HeroStates.walkingRight;
                currentAnimation = animations[1];
                animations[1].GetFramesFromTextureProperties(8 * 160, 1 * 96, 8, 96);
            }
            else if (direction.X == 0 && direction.Y == 0)
            {
                State = HeroStates.idle;
                currentAnimation = animations[0];
                animations[0].GetFramesFromTextureProperties(2 * 160, 0 * 96, 2, 96);
            }
            if (inputReader.ReadIsFighting() && direction.X >= 0)
            {
                State = HeroStates.attackingRight;
                currentAnimation = animations[2];
                animations[2].GetFramesFromTextureProperties(7 * 160, 2 * 96, 7, 96);
            }
            if (inputReader.ReadIsFighting() && direction.X < 0)
            {
                State = HeroStates.attackingLeft;
                currentAnimation = animations[2];
                animations[2].GetFramesFromTextureProperties(7 * 160, 2 * 96, 7, 96);
            }
            if (Health.health <= 0)
            {
                State = HeroStates.dead;
                currentAnimation = animations[3];
                animations[3].GetFramesFromTextureProperties(6 * 160, 4 * 96, 6, 96);
            }

            //create New Position
            direction *= speed;
            position += direction;
            //checkOutOfBounds hozizontal
            this.CheckPosition();

            if ( State == HeroStates.dead && animations[3].CurrentFrame == animations[3].frames[5])
            {
                if (this.Health.lives > 0)
                {
                    Health.health = Health.maxHealth;
                    Health.lives--;
                    hearts.Remove(hearts.Last());
                    State = HeroStates.idle;
                    currentAnimation = animations[0];
                    animations[0].GetFramesFromTextureProperties(2 * 160, 0 * 96, 2, 96);
                }
                else
                {
                    this.ResetLives();
                }
            }
            currentAnimation.Update(gameTime);
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            // draw lives according to the amount of lives
            for (int i = 0; i < hearts.Count; i++)
            {
                _spriteBatch.Draw(heartTexture, hearts[i], Color.White);
            }
            //draw healthbar
            //draw hero
            switch (State)
            {
                case HeroStates.walkingRight:
                    _spriteBatch.Draw(heroTexture, position, animations[1].CurrentFrame.SourceRectangle, Color.White);
                    break;
                case HeroStates.walkingLeft:
                    _spriteBatch.Draw(heroTexture, position, animations[1].CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.FlipHorizontally, 1);
                    break;
                case HeroStates.idle:
                    _spriteBatch.Draw(heroTexture, position, animations[0].CurrentFrame.SourceRectangle, Color.White);
                    break;
                case HeroStates.attackingRight:
                    _spriteBatch.Draw(heroTexture, position, animations[2].CurrentFrame.SourceRectangle, Color.White);
                    break;
                case HeroStates.attackingLeft:
                    _spriteBatch.Draw(heroTexture, position, animations[2].CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0,0), 1f, SpriteEffects.FlipHorizontally, 1);
                    break;
                case HeroStates.dead:
                        _spriteBatch.Draw(heroTexture, position, animations[3].CurrentFrame.SourceRectangle, Color.White);
                    break;
                default:
                    break;
            }
            //draw hitbox
        }

        public void CheckPosition()
        {
            if (Position.X <= 0)
            {
                this.TakeDamage(2);
                position.X = 0 - spriteWidth / 2;
            }
            else if (Position.X >= GlobalSettings.Width)
            {
                this.TakeDamage(2);
                position.X = GlobalSettings.Width;
            }
            //check out of bounds vertical
            if (Position.Y >= GlobalSettings.Height)
            {
                this.TakeDamage(2);
                position.Y = GlobalSettings.Height-spriteHeight;
            }
            else if (Position.Y <= 0)
            {
                this.TakeDamage(2);
                position.Y = 1;
            }
        }

        public void ResetLives()
        {
            this.health = new Health(3,100);
            for (int i = 0; i < 3; i++)
            {
                hearts.Add(new Rectangle(30 * i, 0, 20, 20));
            }
        }
        public void TakeDamage(int amount)
        {
            this.health.health -= amount;
        }
    }
}
