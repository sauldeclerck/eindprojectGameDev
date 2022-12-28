using eindprojectGameDev.Characters.Animations;
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

namespace eindprojectGameDev.Characters
{
    internal class Hero2 : ICharacter
    {
        #region initialize
        private PlayerMovement PlayerMovement;
        public int spriteWidth = 0;
        private int speed = 5;
        public Vector2 Position { get; set; }
        public Vector2 nextPositionH { get; set; }
        public Vector2 nextPositionV { get; set; }
        private Vector2 vector = new Vector2(1, 0);
        public Health Health { get; set; }
        public Rectangle hitbox;
        public Rectangle nextHitboxH { get; set; }
        public Rectangle nextHitboxV { get; set; }
        public Animation animation = new Animation();
        public Texture2D Texture;
        private bool canJump;
        private bool IsJumping;
        private float gravityForce = 2f;
        private float jumpSpeed = 5;
        private float startY = 5;
        bool flip = false;
        #endregion

        public Hero2(ContentManager content, int positionX, int positionY)
        {
            PlayerMovement = new PlayerMovement();
            Position = new Vector2(positionX, positionY);
            nextPositionH = new Vector2(positionX, positionY);
            nextPositionV = new Vector2(positionX, positionY);
            Texture = content.Load<Texture2D>("Cacodaemon Sprite Sheet");
            spriteWidth = 160 / 3;
            animation.GetFramesFromTextureProperties(2 * 160, 0 * 96, 2, 96);
            Health = new Health(3, 100);
            Texture = content.Load<Texture2D>("GoblinHero");
        }
        public virtual void Update(GameTime gameTime)
        {
            var direction = PlayerMovement.ReadMovementInput();
            flip = direction.X >= 0 ? false : true;
            hitbox = new Rectangle((int)nextPositionH.X+50, (int)nextPositionV.Y+42, spriteWidth, spriteWidth);
            startY = Position.Y;
            nextPositionH = Position;
            nextPositionV = Position;
            nextPositionH += direction * speed;
            nextPositionV = Gravity(nextPositionV);
            Jump();
            nextHitboxH = new Rectangle((int)nextPositionH.X+50, (int)nextPositionH.Y+42, spriteWidth, spriteWidth);
            nextHitboxV = new Rectangle((int)nextPositionV.X + 50, (int)nextPositionV.Y + 42, spriteWidth, spriteWidth);
            if (!CheckCollision(nextHitboxH)) Position = new Vector2(nextPositionH.X, Position.Y);
            if (!CheckCollision(nextHitboxV)) Position = new Vector2(Position.X, nextPositionV.Y);
            else canJump = true;
            CheckEnemyHit();
            animation.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch _spriteBatch)
        {
            if (animation.CurrentFrame != null)
            {
                switch (flip)
                {
                    case true:
                        _spriteBatch.Draw(Texture, Position, animation.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.FlipHorizontally, 0);
                        break;
                    case false:
                        _spriteBatch.Draw(Texture, Position, animation.CurrentFrame.SourceRectangle, Color.White);
                        break;
                }
            }
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
            foreach (var item in GameManager.enemies)
            {
                if (hitbox.Intersects(item.hitbox))
                {
                    LevelManager.DoDamage(50, item);
                    TakeDamage(50);
                }
            }
        }

        private Vector2 Gravity(Vector2 Position)
        {
            Position.Y += gravityForce;
            return Position;
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
                if (PlayerMovement.ReadIsJumping())
                {
                    IsJumping = true;
                    canJump = false;
                    jumpSpeed = -14;//Give it upward thrust
                }
            }
        }
    }
}