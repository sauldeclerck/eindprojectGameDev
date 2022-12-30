using eindprojectGameDev.Animations;
using eindprojectGameDev.interfaces;
using eindprojectGameDev.Map;
using eindprojectGameDev.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace eindprojectGameDev.Characters.Player
{
    public class Hero : Player, ICharacter, IMovable
    {
        #region initialize
        public Animation currentAnimation = new Animation();
        public HealthBar HealthBar { get; set; }
        public Rectangle hitRectangle { get; set; }
        public int Damage { get; set; } = 10;
        public int speed = 5;
        public int spriteWidth { get; set; }
        public float gravityForce = 2f;
        private Hearts Hearts;
        private Color color { get; set; }
        #endregion

        public Hero(ContentManager content, int positionX, int positionY)
        {
            PlayerMovement = new PlayerMovement();
            StartPosition = new Vector2(positionX, positionY);
            Position = StartPosition;
            color = Color.White;
            Flip = SpriteEffects.None;
            nextPositionH = new Vector2(positionX, positionY);
            nextPositionV = new Vector2(positionX, positionY);
            HealthBar = new HealthBar(content.Load<Texture2D>("Red_Rectangle"));
            Hearts = new Hearts(content.Load<Texture2D>("heart"));
            spriteWidth = 160 / 3;
            Health = new Health(1, 100);
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
            if (PlayerMovement.ReadIsFighting() && direction.X >= 0 && currentAnimation.counter % 6 == 0)
            {
                hitRectangle = new Rectangle((int)(Position.X + spriteWidth * 2), (int)Position.Y + 20, 54, 80);
            }
            else if (PlayerMovement.ReadIsFighting() && direction.X < 0 && currentAnimation.counter % 6 == 0)
            {
                hitRectangle = new Rectangle((int)(Position.X), (int)Position.Y + 20, 54, 80);
            }
            else
            {
                hitRectangle = new Rectangle(0, 0, 0, 0);
            }
            Hearts.Update(this);
            Hitbox = new Rectangle((int)nextPositionH.X + 50, (int)nextPositionV.Y + 42, spriteWidth, spriteWidth);
            currentAnimation.Update(gameTime);
            CheckEnemyHit(Hitbox, true);
            CheckEnemyHit(hitRectangle, false);
            CheckPowerUp();
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            HealthBar.Draw(_spriteBatch);
            Hearts.Draw(_spriteBatch);
            if (currentAnimation.CurrentFrame != null)
            {
                _spriteBatch.Draw(Texture, Position, currentAnimation.CurrentFrame.SourceRectangle, color, 0f, new Vector2(0, 0), 1f, Flip, 0);
            }
        }

        public void CheckPowerUp()
        {
            if (GameManager.EnablePowerUps)
            {
                foreach (var item in GameManager.PowerUps)
                {
                    if (item.isActive)
                    {
                        if (Hitbox.Intersects(item.BoundingBox))
                        {
                            item.isActive = false;
                            switch (item.Type)
                            {
                                case PowerUpType.PowerUpTypes.speed:
                                    speed += 2;
                                    break;
                                case PowerUpType.PowerUpTypes.damage:
                                    Damage += 10;
                                    break;
                            }
                        }
                    }
                    continue;
                }
            }
        }

        public void CheckEnemyHit(Rectangle hitbox, bool selfDamage)
        {
            foreach (var item in GameManager.enemies)
            {
                if (item.isActive)
                {
                    if (hitbox.Intersects(item.Hitbox))
                    {
                        GameManager.DoDamage(100, item);
                        if (selfDamage)
                        {
                            TakeDamage(50);
                        }
                    }
                }
            }
        }

        public void CheckFlip(Vector2 direction)
        {
            if (direction.X != 0)
            {
                Flip = direction.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
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
            if (direction.X != 0)
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
            if (PlayerMovement.ReadIsFighting())
            {
                currentAnimation = Animations[2];
                Animations[2].GetFramesFromTextureProperties(7 * 160, 2 * 96, 7, 96);
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