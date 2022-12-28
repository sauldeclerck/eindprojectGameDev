using eindprojectGameDev.Characters.Animations;
using eindprojectGameDev.interfaces;
using eindprojectGameDev.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eindprojectGameDev.Characters
{
    internal class Entity : ICharacter
    {
        public bool JumpInput { get; set; }
        public bool IsJumping;
        public float jumpSpeed = 5;
        public float startY { get; set; }
        public bool canJump { get; set; }
        public bool Jumpable { get; set; }
        public Vector2 Position { get; set; }
        public Health Health { get; set; }
        public Texture2D Texture { get; set; }
        public Animation currentAnimation = new Animation();
        public Rectangle Hitbox { get; set; }
        public HealthBar HealthBar { get; set; }
        public Rectangle nextHitboxH { get; set; }
        public Rectangle nextHitboxV { get; set; }
        public int speed = 5;
        SpriteEffects flip = SpriteEffects.None;
        public Vector2 nextPositionH { get; set; }
        public int spriteWidth { get; set; }
        public Vector2 nextPositionV { get; set; }
        public float gravityForce = 2f;
        public Vector2 direction;
        Animation[] animations = new Animation[4]
        {
            new Animation(), //Idle (0)
            new Animation(), //Walk (1)
            new Animation(), //Fight (2)
            new Animation() //Die (3)
        };

        public void Update(GameTime gameTime)
        {
            startY = Position.Y;
            HealthBar.Update(Position, Health);
            nextPositionH = Position;
            nextPositionV = Position;
            nextPositionH += direction * speed;
            nextPositionV = Gravity(nextPositionV);
            Jump();
            nextHitboxH = new Rectangle((int)nextPositionH.X + 50, (int)nextPositionH.Y + 42, spriteWidth, spriteWidth);
            nextHitboxV = new Rectangle((int)nextPositionV.X + 50, (int)nextPositionV.Y + 42, spriteWidth, spriteWidth);
            flip = direction.X >= 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            if (!CheckCollision(nextHitboxH)) { SetPosition(new Vector2(nextPositionH.X, Position.Y)); }
            if (!CheckCollision(nextHitboxV)) { SetPosition(new Vector2(Position.X, nextPositionV.Y));}
            else canJump = true;
            SetAnimation(direction);
            currentAnimation.Update(gameTime);
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch)
        {
            HealthBar.Draw(_spriteBatch);
            if (currentAnimation.CurrentFrame != null)
            {
                _spriteBatch.Draw(Texture, Position, currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0, 0), 1f, flip, 0);
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

        public void TakeDamage(int amount)
        {
            if (this.Health.health - amount <= 0)
            {
                this.Health.lives--;
                if (this.Health.lives > 0)
                {
                    this.Health.health = this.Health.maxHealth;
                }
            }
            else
            {
                this.Health.health -= amount;
            }
        }


        private Vector2 Gravity(Vector2 Position)
        {
            Position.Y += gravityForce;
            return Position;
        }

        public void SetPosition(Vector2 newPosition)
        {
            Position = newPosition;
        }

        public void SetAnimation(Vector2 direction)
        {
            if (direction.X < 0)
            {
                currentAnimation = animations[1];
                animations[1].GetFramesFromTextureProperties(8 * 160, 1 * 96, 8, 96);
            }
            else if (direction.X > 0)
            {
                currentAnimation = animations[1];
                animations[1].GetFramesFromTextureProperties(8 * 160, 1 * 96, 8, 96);
            }
            else if (direction.X == 0 && direction.Y == 0)
            {
                currentAnimation = animations[0];
                animations[0].GetFramesFromTextureProperties(2 * 160, 0 * 96, 2, 96);
            }
            //if (PlayerMovement.ReadIsFighting() && direction.X >= 0)
            //{
            //    currentAnimation = animations[2];
            //    animations[2].GetFramesFromTextureProperties(7 * 160, 2 * 96, 7, 96);
            //}
            //if (PlayerMovement.ReadIsFighting() && direction.X < 0)
            //{
            //    currentAnimation = animations[2];
            //    animations[2].GetFramesFromTextureProperties(7 * 160, 2 * 96, 7, 96);
            //}
            if (Health.health <= 0)
            {
                currentAnimation = animations[3];
                animations[3].GetFramesFromTextureProperties(6 * 160, 4 * 96, 6, 96);
            }
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
                if (JumpInput)
                {
                    IsJumping = true;
                    canJump = false;
                    jumpSpeed = -14;//Give it upward thrust
                }
            }
        }
    }
}
