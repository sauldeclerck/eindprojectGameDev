using eindprojectGameDev.Animations;
using eindprojectGameDev.interfaces;
using eindprojectGameDev.Map;
using eindprojectGameDev.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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

namespace eindprojectGameDev.Characters.Player
{
    public class Hero : Player, ICharacter, IMovable
    {
        #region initialize
        public Animation currentAnimation = new Animation();
        public HealthBar HealthBar { get; set; }
        public int speed = 5;
        public int spriteWidth { get; set; }
        public float gravityForce = 2f;
        private Hearts Hearts;
        #endregion

        public Hero(ContentManager content, int positionX, int positionY)
        {
            PlayerMovement = new PlayerMovement();
            Position = new Vector2(positionX, positionY);
            nextPositionH = new Vector2(positionX, positionY);
            nextPositionV = new Vector2(positionX, positionY);
            Texture = content.Load<Texture2D>("Cacodaemon Sprite Sheet");
            HealthBar = new HealthBar(content.Load<Texture2D>("Red_Rectangle"));
            Hearts = new Hearts(content.Load<Texture2D>("heart"));
            spriteWidth = 160 / 3;
            //animation.GetFramesFromTextureProperties(2 * 160, 0 * 96, 2, 96);
            Health = new Health(3, 100);
            Texture = content.Load<Texture2D>("GoblinHero");
            Animations = new Animation[4]
        {
            new Animation(), //Idle (0)
            new Animation(), //Walk (1)
            new Animation(), //Fight (2)
            new Animation() //Die (3)
        };
        }
        public void Update(GameTime gameTime)
        {
            startY = Position.Y;
            direction = PlayerMovement.ReadMovementInput();
            JumpInput = PlayerMovement.ReadIsJumping();
            HealthBar.Update(Position, Health);
            nextPositionH = Position;
            nextPositionV = Position;
            nextPositionH += direction * speed;
            nextPositionV = Gravity(nextPositionV);
            Jump();
            nextHitboxH = new Rectangle((int)nextPositionH.X + 50, (int)nextPositionH.Y + 42, spriteWidth, spriteWidth);
            nextHitboxV = new Rectangle((int)nextPositionV.X + 50, (int)nextPositionV.Y + 42, spriteWidth, spriteWidth);
            CheckFlip(direction);
            if (!CheckCollision(nextHitboxH)) { SetPosition(new Vector2(nextPositionH.X, Position.Y)); }
            if (!CheckCollision(nextHitboxV)) { SetPosition(new Vector2(Position.X, nextPositionV.Y)); }
            else canJump = true;
            SetAnimation(direction);
            currentAnimation.Update(gameTime);
            Hearts.Update(this);
            Hitbox = new Rectangle((int)nextPositionH.X + 50, (int)nextPositionV.Y + 42, spriteWidth, spriteWidth);
            if (PlayerMovement.ReadIsFighting())
            {
                CheckFlip(direction);
                currentAnimation = Animations[2];
                Animations[2].GetFramesFromTextureProperties(7 * 160, 2 * 96, 7, 96);
            }
            currentAnimation.Update(gameTime);
            CheckEnemyHit();
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            HealthBar.Draw(_spriteBatch);
            Hearts.Draw(_spriteBatch);
            if (currentAnimation.CurrentFrame != null)
            {
                _spriteBatch.Draw(Texture, Position, currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0, 0), 1f, Flip, 0);
            }
        }

        public void CheckEnemyHit()
        {
            foreach (var item in GameManager.enemies)
            {
                if (Hitbox.Intersects(item.Hitbox))
                {
                    GameManager.DoDamage(50, item);
                    //TakeDamage(50);
                }
            }
        }

        public void CheckFlip(Vector2 direction)
        {
            Flip = direction.X >= 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
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
            if (Health.health - amount <= 0)
            {
                Health.lives--;
                if (Health.lives > 0)
                {
                    Health.health = Health.maxHealth;
                }
            }
            else
            {
                Health.health -= amount;
            }
        }


        public Vector2 Gravity(Vector2 Position)
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
                currentAnimation = Animations[1];
                Animations[1].GetFramesFromTextureProperties(8 * 160, 1 * 96, 8, 96);
            }
            else if (direction.X > 0)
            {
                currentAnimation = Animations[1];
                Animations[1].GetFramesFromTextureProperties(8 * 160, 1 * 96, 8, 96);
            }
            else if (direction.X == 0 && direction.Y == 0)
            {
                currentAnimation = Animations[0];
                Animations[0].GetFramesFromTextureProperties(2 * 160, 0 * 96, 2, 96);
            }

            if (Health.health <= 0)
            {
                currentAnimation = Animations[3];
                Animations[3].GetFramesFromTextureProperties(6 * 160, 4 * 96, 6, 96);
            }
        }

        public void Jump()
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