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
        private Vector2 positie;
        private int speed = 5;
        private Texture2D texture;
        private int spriteWidth = 96;
        private int spriteHeight = 96;
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
        public enum HeroStates { walkingRight, walkingLeft, idle, attackingRight, attackingLeft }
        public HeroStates State = HeroStates.idle;
        public Hero(Texture2D texture, IInputReader inputReader)
        {
            this.texture = texture;
            this.inputReader = inputReader;
            positie = new Vector2(100, 100);
        }

        public void Update(GameTime gameTime)
        {
            State = HeroStates.idle;
            currentAnimation = animations[0];
            var direction = inputReader.ReadMovementInput();
            if (direction.X < 0)
            {
                State = HeroStates.walkingLeft;
                currentAnimation = animations[1];
                animations[1].GetFramesFromTextureProperties(8 * 160, 1 * 96, 8, 96);
                currentAnimation.Update(gameTime);
            }
            else if (direction.X > 0)
            {
                State = HeroStates.walkingRight;
                currentAnimation = animations[1];
                animations[1].GetFramesFromTextureProperties(8 * 160, 1 * 96, 8, 96);
                currentAnimation.Update(gameTime);
            }
            else if (direction.X == 0 && direction.Y == 0)
            {
                State = HeroStates.idle;
                currentAnimation = animations[0];
                animations[0].GetFramesFromTextureProperties(2 * 160, 0 * 96, 2, 96);
                currentAnimation.Update(gameTime);
            }
            if (inputReader.ReadIsFighting() && direction.X >= 0)
            {
                State = HeroStates.attackingRight;
                currentAnimation = animations[2];
                animations[2].GetFramesFromTextureProperties(7 * 160, 2 * 96, 7, 96);
                currentAnimation.Update(gameTime);
            }
            if (inputReader.ReadIsFighting() && direction.X < 0)
            {
                State = HeroStates.attackingLeft;
                currentAnimation = animations[2];
                animations[2].GetFramesFromTextureProperties(7 * 160, 2 * 96, 7, 96);
                currentAnimation.Update(gameTime);
            }
            //create New Position
            direction *= speed;
            positie += direction;
            //checkOutOfBounds hozizontal
            if (positie.X <= 0 - spriteWidth/2)
            {
                positie.X = 0 - spriteWidth/2;
            }
            else if (positie.X >= GlobalSettings.Width-spriteWidth*2)
            {
                positie.X = GlobalSettings.Width-spriteWidth*2;
            }
            //check out of bounds vertical
            if (positie.Y > GlobalSettings.Height-spriteHeight)
            {
                positie.Y = GlobalSettings.Height - spriteHeight;
            }
            else if (positie.Y < 0)
            {
                positie.Y = 0;
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            switch (State)
            {
                case HeroStates.walkingRight:
                    _spriteBatch.Draw(texture, positie, animations[1].CurrentFrame.SourceRectangle, Color.White);
                    break;
                case HeroStates.walkingLeft:
                    _spriteBatch.Draw(texture, positie, animations[1].CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.FlipHorizontally, 1);
                    break;
                case HeroStates.idle:
                    _spriteBatch.Draw(texture, positie, animations[0].CurrentFrame.SourceRectangle, Color.White);
                    break;
                case HeroStates.attackingRight:
                    _spriteBatch.Draw(texture, positie, animations[2].CurrentFrame.SourceRectangle, Color.White);
                    break;
                case HeroStates.attackingLeft:
                    _spriteBatch.Draw(texture, positie, animations[2].CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0,0), 1f, SpriteEffects.FlipHorizontally, 1);
                    break;
                default:
                    break;
            }
        }
    }
}
