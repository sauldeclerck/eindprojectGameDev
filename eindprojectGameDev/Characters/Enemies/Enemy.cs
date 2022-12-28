using eindprojectGameDev.Characters.Animations;
using eindprojectGameDev.interfaces;
using eindprojectGameDev.Map;
using eindprojectGameDev.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindprojectGameDev.Characters.Enemies
{
    internal class Enemy : ICharacter
    {
        public bool isActive = true;
        public int spriteWidth = 0;
        public enum EnemyType { daemon };
        public EnemyType Type;
        public Vector2 Position { get; set; }
        public Vector2 nextPositionH { get; set; }
        public Vector2 nextPositionV { get; set; }
        private Vector2 vector = new Vector2(1, 0);
        private Vector2 movement = new Vector2(1, 0);
        public Health Health { get; set; }
        public Rectangle hitbox;
        public Rectangle nextHitboxH { get; set; }
        public Rectangle nextHitboxV { get; set; }
        Texture2D ICharacter.Texture { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Rectangle Hitbox { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Animation animation = new Animation();
        public Texture2D Texture;
        bool flip = false;

        public virtual void Update(GameTime gameTime)
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
                if (CollidingHorizontal())
                {
                    vector *= -1;
                    flip = !flip;
                }
                if (!CollidingVertical())
                {
                    vector *= -1;
                    flip = !flip;
                }
                hitbox = new Rectangle((int)nextPositionH.X, (int)nextPositionV.Y, spriteWidth, spriteWidth);
                Position = new Vector2(nextPositionH.X, Position.Y);
                animation.Update(gameTime);
                isActive = CheckActive();
            }
        }
        public virtual void Draw(SpriteBatch _spriteBatch)
        {
            if (animation.CurrentFrame != null)
            {
                if (isActive)
                {
                    switch (movement.X)
                    {
                        case <= 0:
                            _spriteBatch.Draw(Texture, Position, animation.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.FlipHorizontally, 0);
                            break;
                        case > 0:
                            _spriteBatch.Draw(Texture, Position, animation.CurrentFrame.SourceRectangle, Color.White);
                            break;
                    }
                }
            }
            
        }

        public bool CollidingHorizontal()
        {
            foreach (var item in GameManager.defaultBlocks)
            {
                if (item != null)
                {
                    if (nextHitboxH.Intersects(item.Hitbox))
                    {
                        return true;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return false;
        }

        public bool CollidingVertical()
        {
            foreach (var item in GameManager.defaultBlocks)
            {
                if (item != null)
                {
                    if (nextHitboxV.Intersects(item.Hitbox))
                    {
                        return true;
                    }
                    else
                    {
                        continue;
                    }
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
            this.hitbox = new Rectangle(0, 0, 0, 0);
        }
    }
}
