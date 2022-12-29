using eindprojectGameDev.Animations;
using eindprojectGameDev.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace eindprojectGameDev.Characters
{
    public abstract class Character : Entity
    {
        public Vector2 StartPosition { get; set; }
        public Animation[] Animations { get; set; }
        public Animation CurrentAnimation { get; set; }
        public Rectangle nextHitboxH { get; set; }
        public Rectangle nextHitboxV { get; set; }
        public Vector2 nextPositionH { get; set; }
        public Vector2 nextPositionV { get; set; }
        public Vector2 direction { get; set; }
        public Health Health { get; set; }
        public SpriteEffects Flip { get; set; }

        public void TakeDamage(int amount)
        {
            if (this.Health.health - amount <= 0)
            {
                if (Health.lives != 0)
                {
                    ResetPosition();
                }
                this.Health.lives--;
                if (this.Health.lives > 0)
                {
                    this.Health.health = this.Health.maxHealth;
                }
                else
                {
                    Health.health = 0;
                    Health.lives = 0;
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

        public void DrawAnimation(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(Texture, Position, CurrentAnimation.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0, 0), 1f, Flip, 0);
        }

        public void ResetPosition()
        {
            Position = StartPosition;
        }
    }
}
