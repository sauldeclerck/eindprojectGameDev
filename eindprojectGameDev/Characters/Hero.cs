using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace eindprojectGameDev.Characters
{
    internal class Hero : IGameObject
    {
        private IInputReader inputReader;
        public Vector2 position;
        private int speed = 5;
        private Texture2D healthTexture;
        private Texture2D heroTexture;
        private int spriteWidth = 96;
        private int spriteHeight = 96;
        private Rectangle healthBarRectangle;
        private Animation currentAnimation;
        Animation[] animations = new Animation[4]
        {
                new Animation(), //Idle
                new Animation(), //Walk
                new Animation(), //Fight
                new Animation() //Die
        };
        /*
        private Animation animationIdle;
        private Animation animationWalk;
        private Animation animationDie;
        private Animation animationFight;
        */
        public enum HeroStates { walkingRight, walkingLeft, idle, attackingRight, attackingLeft, dead }
        public HeroStates State = HeroStates.idle;

        private int health = 100;
        bool isAlive = true;
        public int Health
        {
            get { return health; }
            set {
                if (value + health > 100)
                {
                    health = 100;
                }
                else if (health + value < 0)
                {
                    isAlive = false;
                }
                health = value; }
        }

        public Hero(Texture2D heroTexture, Texture2D healthTexture, IInputReader inputReader)
        {
            this.healthTexture = healthTexture;
            this.heroTexture = heroTexture;
            this.inputReader = inputReader;
            position = new Vector2(100, 100);
        }

        public void Update(GameTime gameTime)
        {
            //health--;
            State = HeroStates.idle;
            currentAnimation = animations[0];
            var direction = inputReader.ReadMovementInput();
            healthBarRectangle = new Rectangle((int)position.X+spriteWidth/2, (int)position.Y+spriteWidth/3, health/2, 10);
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
            if (health <= 0)
            {
                State = HeroStates.dead;
                currentAnimation = animations[3];
                animations[3].GetFramesFromTextureProperties(6 * 160, 4 * 96, 6, 96);
            }
            currentAnimation.Update(gameTime);
            //create New Position
            direction *= speed;
            position += direction;
            //checkOutOfBounds hozizontal
            if (position.X <= 0 - spriteWidth/2)
            {
                position.X = 0 - spriteWidth/2;
            }
            else if (position.X >= GlobalSettings.Width-spriteWidth*2)
            {
                position.X = GlobalSettings.Width-spriteWidth*2;
            }
            //check out of bounds vertical
            if (position.Y > GlobalSettings.Height-spriteHeight)
            {
                position.Y = GlobalSettings.Height - spriteHeight;
            }
            else if (position.Y < 0)
            {
                position.Y = 0;
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(healthTexture, healthBarRectangle, Color.Red);
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
        }
    }
}
