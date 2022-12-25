using eindprojectGameDev.interfaces;
using eindprojectGameDev.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace eindprojectGameDev.Characters
{
    internal class Hero : ICharacter
    {
        #region initialize
        public enum HeroStates { walkingRight, walkingLeft, idle, attackingRight, attackingLeft, dead }
        public Health Health { get => health; set => this.health = value; }
        public Vector2 Position { get; set; }
        private Texture2D RedTexture;
        private Health health;
        public Rectangle hitbox;
        public Rectangle nextHitboxH { get; set; }
        public Rectangle nextHitboxV { get; set; }
        public Vector2 nextPositionH { get; set; }
        public Vector2 nextPositionV { get; set; }
        private IInputReader inputReader;
        public Vector2 position;
        private int speed = 5;
        private Texture2D heroTexture;
        private int spriteWidth = 96, spriteHeight = 96;
        public HeroStates State = HeroStates.idle;
        private Animation currentAnimation;
        private bool IsJumping = false;
        private int jumpHeight = 10;
        private int jumpCounter = 5;
        private int jumpLength = 5;
        private bool onGround = false;
        Animation[] animations = new Animation[4]
        {
            new Animation(), //Idle (0)
            new Animation(), //Walk (1)
            new Animation(), //Fight (2)
            new Animation() //Die (3)
        };
        #endregion
        public Hero()
        {
            this.heroTexture = GameManager.Content.Load<Texture2D>("GoblinHero");
            this.RedTexture = GameManager.Content.Load<Texture2D>("Red_Rectangle");
            this.inputReader = new PlayerMovement();
            position = new Vector2(170, 170);
            Health = new Health(3, 100);
            this.ResetLives();
        }

        public void Update(GameTime gameTime)
        {
            var direction = inputReader.ReadMovementInput();

            Position = new Vector2((int)position.X, (int)position.Y);
            nextPositionH = position;
            nextPositionV = position;
            hitbox = new Rectangle((int)position.X + 50, (int)position.Y + 50, spriteWidth - 50, spriteHeight - 50);
            nextPositionV = Gravity(nextPositionV);
            direction *= speed;
            nextPositionH += direction;
            nextHitboxH = new Rectangle((int)nextPositionH.X+50, (int)nextPositionH.Y+50, spriteWidth - 50, spriteHeight - 50);
            nextHitboxV = new Rectangle((int)nextPositionV.X+50, (int)nextPositionV.Y+50, spriteWidth - 50, spriteHeight - 50);
            //MessageBox.Show(nextPosition.X + " " + nextPosition.Y);

            //if (!CheckCollisionHorizontal(nextHitbox))
            //{
            //    position.X = nextPosition.X;
            //}
            //if (!CheckCollisionVertical(nextHitbox))
            //{
            //    position.Y = nextPosition.Y;
            //    onGround = false;
            //}
            //else
            //{
            //    onGround = true;
            //}

            if (!CheckCollision(nextHitboxH))
            {
                position.X = nextPositionH.X;
            }
            if (!CheckCollision(nextHitboxV))
            {
                position.Y = nextPositionV.Y;
                onGround = false;
            }
            else
            {
                onGround = true;
            }
            SetAnimation(direction);
            currentAnimation.Update(gameTime);
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
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
                    _spriteBatch.Draw(heroTexture, position, animations[2].CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.FlipHorizontally, 1);
                    break;
                case HeroStates.dead:
                    _spriteBatch.Draw(heroTexture, position, animations[3].CurrentFrame.SourceRectangle, Color.White);
                    break;
                default:
                    break;
            }
        }

        public bool CheckCollision(Rectangle nextPosition)
        {
            foreach (var item in GameManager.defaultBlocks)
            {
                if (item != null)
                {
                    if (nextPosition.Intersects(item.Hitbox))
                        return true;
                    else
                        continue;
                }
            }
            return false;
        }
        public bool CheckCollisionLeft(Rectangle hitbox)
        {
            foreach (var item in GameManager.defaultBlocks)
            {
                if (item != null)
                {
                    //left and right
                    if (hitbox.Right > item.Hitbox.Left &&
                        hitbox.Left < item.Hitbox.Left &&
                        hitbox.Bottom > item.Hitbox.Top &&
                        hitbox.Top < item.Hitbox.Bottom)
                        return true;
                }
            }
            return false;
        }
        public bool CheckCollisionRight(Rectangle hitbox)
        {
            foreach (var item in GameManager.defaultBlocks)
            {
                if (item != null)
                {
                    //left and right
                    if (hitbox.Left < item.BoundingBox.Right &&
                        hitbox.Right > item.BoundingBox.Right &&
                        hitbox.Bottom > item.BoundingBox.Top &&
                        hitbox.Top < item.BoundingBox.Bottom)
                        return true;
                }
            }
            return false;
        }
        public bool CheckCollisionVertical(Rectangle hitbox)
        {
            foreach (var item in GameManager.defaultBlocks)
            {
                if (item != null)
                {
                    //top and bottom
                    if ((hitbox.Bottom > item.Hitbox.Top &&
                        hitbox.Top < item.Hitbox.Top &&
                        hitbox.Right > item.Hitbox.Left &&
                        hitbox.Left < item.Hitbox.Right) &&
                        (hitbox.Top < item.Hitbox.Bottom &&
                        hitbox.Bottom > item.Hitbox.Bottom &&
                        hitbox.Right > item.Hitbox.Left &&
                        hitbox.Left < item.Hitbox.Right))
                        return true;
                }
            }
            return false;
        }
        public void ResetLives()
        {
            this.health = new Health(3, 100);
        }
        public void TakeDamage(int amount)
        {
            this.health.health -= amount;
        }
        private void Jump()
        {
            if (inputReader.ReadIsJumping() && !IsJumping && onGround)
            {
                IsJumping = true;
            }
            if (!IsJumping) return;
            if (!onGround)
            {
                position.Y -= jumpHeight;
                jumpCounter++;
            }
            if (jumpCounter != jumpLength) return;
            IsJumping = false;
            jumpCounter = 0;
        }
        private Vector2 Gravity(Vector2 Position)
        {
            Position.Y += jumpCounter;

            //if (!onGround)
            //{
            //    Position.Y += jumpCounter;
            //}
            //else
            //{
            //    onGround = true;
            //}
            return Position;
        }
        private void SetAnimation(Vector2 direction)
        {
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
        }
    }
}