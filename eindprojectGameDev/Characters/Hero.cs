using eindprojectGameDev.interfaces;
using eindprojectGameDev.Map;
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
        private int jumpSpeed = 0;
        private int startY = 0;
        private float gravityForce = 4.5f;
        private bool onGround = false;
        private bool canJump = false;
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
            this.inputReader = new PlayerMovement();
            position = new Vector2(170, 170);
            Health = new Health(3, 100);
            this.ResetHero();
        }

        public void Update(GameTime gameTime)
        {
            if (this.health.health < 0)
            {
                this.health.lives -= 1;
                this.health.health = this.health.maxHealth;
                ResetHero();
            }
            var direction = inputReader.ReadMovementInput();

            Position = new Vector2((int)position.X, (int)position.Y);
            nextPositionH = position;
            nextPositionV = position;
            hitbox = new Rectangle((int)position.X + 50, (int)position.Y + 50, spriteWidth - 50, spriteHeight - 50);
            nextPositionV = Gravity(nextPositionV);
            direction *= speed;
            nextPositionH += direction;
            startY = (int)nextPositionV.Y;
            Jump();
            nextHitboxH = new Rectangle((int)nextPositionH.X + 50, (int)nextPositionH.Y + 50, spriteWidth - 50, spriteHeight - 50);
            nextHitboxV = new Rectangle((int)nextPositionV.X + 50, (int)nextPositionV.Y + 50, spriteWidth - 50, spriteHeight - 50);
            
            if (!CheckCollision(nextHitboxH)) position.X = nextPositionH.X;
            
            if (!CheckCollision(nextHitboxV)){
                position.Y = nextPositionV.Y;
                onGround = false;
            }
            else
            {
                onGround = true;
                canJump = true;
            }
            CheckEnemyHit();
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
                    {
                        return true;
                    }
                    else
                        continue;
                }
            }
            return false;
        }
        public void CheckEnemyHit()
        {
            foreach (var item in LevelManager.enemies)
            {
                if (hitbox.Intersects(item.hitbox))
                {
                    LevelManager.DoDamage(50, item);
                    TakeDamage(50);
                }
            }
        }
        public void ResetHero()
        {
            this.position = new Vector2(170, 170);
        }
        public void TakeDamage(int amount)
        {
            this.health.health -= amount;
        }
        private void Jump()
        {
            // credits => https://flatformer.blogspot.com/
            if (IsJumping)
            {
                if (canJump)
                {
                    nextPositionV += new Vector2(0, jumpSpeed);//Making it go up
                    jumpSpeed += 1;//Some math (explained later)
                }
                
                if (nextPositionV.Y >= startY)
                //If it's farther than ground
                {
                    nextPositionV = new Vector2(0, startY);//Then set it on
                    canJump = false;
                    IsJumping = false;
                }
            }

            else
            {
                if (inputReader.ReadIsJumping())
                {
                    IsJumping = true;
                    canJump = false;
                    jumpSpeed = -14;//Give it upward thrust
                }
            }
        }
        private Vector2 Gravity(Vector2 Position)
        {
            Position.Y += gravityForce;
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