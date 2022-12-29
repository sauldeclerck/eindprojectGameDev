using eindprojectGameDev.Animations;
using eindprojectGameDev.interfaces;
using eindprojectGameDev.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace eindprojectGameDev.Characters.Enemies
{
    public class porcupine : Enemy, ICharacter
    {
        public int spriteWidth = 0;
        public enum EnemyType { daemon };
        
        private Vector2 movement = new Vector2(1, 0);
        public porcupine(int positionX, int positionY, ContentManager content)
        {
            isActive = true;
            vector = new Vector2(1, 0);
            Position = new Vector2(positionX, positionY);
            nextPositionH = new Vector2(positionX, positionY);
            nextPositionV = new Vector2(positionX, positionY);
            Texture = content.Load<Texture2D>("Porcupine");
            spriteWidth = 160 / 5;
            CurrentAnimation = new Animation();
            CurrentAnimation.GetFramesFromTextureProperties(base.Texture.Width / 5 * 5, 0, 5, spriteWidth);
            Health = new Health(1, 20);
            //Type = EnemyType.daemon;
        }
        public void Update(GameTime gameTime)
        {
            if (isActive)
            {
                nextPositionH = Position;
                nextPositionV = Position;
                movement *= vector;
                nextPositionH += movement;
                // nextPositionV += new Vector2(nextPositionH.X, nextPositionH.Y-50);
                nextHitboxH = new Rectangle((int)nextPositionH.X, (int)nextPositionH.Y, spriteWidth, spriteWidth);
                nextHitboxV = new Rectangle((int)nextPositionH.X, (int)nextPositionV.Y + 50, spriteWidth, 20);
                if (CheckCollision(nextHitboxH)) { vector *= -1; }
                if (!CheckCollision(nextHitboxV)) { vector *= -1; }
                
                Hitbox = new Rectangle((int)nextPositionH.X, (int)nextPositionV.Y, spriteWidth, spriteWidth);
                Position = new Vector2(nextPositionH.X, Position.Y);
                CurrentAnimation.Update(gameTime);
                isActive = CheckActive();
            }
        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            if (CurrentAnimation.CurrentFrame != null)
            {
                if (isActive)
                {
                    switch (movement.X)
                    {
                        case <= 0:
                            _spriteBatch.Draw(Texture, Position, CurrentAnimation.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.FlipHorizontally, 0);
                            break;
                        case > 0:
                            _spriteBatch.Draw(Texture, Position, CurrentAnimation.CurrentFrame.SourceRectangle, Color.White);
                            break;
                    }
                }
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

        private bool CheckActive()
        {
            if (this.Health.lives <= 0)
            {
                Deactivate();
                return false;
            }
            return true;
        }
        private void Deactivate()
        {
            this.Position = new Vector2(0, 0);
            this.Hitbox = new Rectangle(0, 0, 0, 0);
        }
    }
}
