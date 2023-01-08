using eindprojectGameDev.Animations;
using eindprojectGameDev.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace eindprojectGameDev.Characters
{
    public abstract class Character : Entity
    {
        public float HeightMultiplier { get; set; }
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
            if (this.Health.CurrentHealth - amount <= 0)
            {
                if (Health.Lives != 0)
                {
                    ResetPosition();
                }
                this.Health.Lives--;
                if (this.Health.Lives > 0)
                {
                    this.Health.CurrentHealth = this.Health.MaxHealth;
                }
                else
                {
                    Health.CurrentHealth = 0;
                    Health.Lives = 0;
                }
            }
            else
            {
                this.Health.CurrentHealth -= amount;
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
            _spriteBatch.Draw(Texture, Position, CurrentAnimation.CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(0, 0), HeightMultiplier, Flip, 0);
        }

        public void ResetPosition()
        {
            Position = StartPosition;
        }
    }
}
