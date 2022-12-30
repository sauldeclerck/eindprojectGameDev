using eindprojectGameDev.interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace eindprojectGameDev.Characters.Enemies
{
    public abstract class NPC : Character, ICharacter
    {
        public bool isActive { get; set; }
        public Vector2 vector { get; set; }
        public EnemyTypes.EnemyType EnemyType { get; set; }
        public Vector2 movement = new Vector2(1, 0);
        public int spriteWidth { get; set; }

        public void SetNextPositions()
        {
            nextPositionH = Position;
            nextPositionV = Position;
            movement *= vector;
            nextPositionH += movement;
            nextHitboxH = new Rectangle((int)(nextPositionH.X), (int)nextPositionH.Y, spriteWidth, spriteWidth);
            nextHitboxV = new Rectangle((int)(nextPositionH.X+20), (int)nextPositionV.Y+50, spriteWidth/4, 20);
            CheckNextPositions();
            Flip = movement.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        }

        public void CheckNextPositions()
        {
            if (CheckCollision(nextHitboxH)) { vector *= -1; }
            if (!CheckCollision(nextHitboxV)) { vector *= -1; }
        }

        public bool CheckActive()
        {
            if (this.Health.lives <= 0)
            {
                Deactivate();
                return false;
            }
            return true;
        }
        public void Deactivate()
        {
            this.Position = new Vector2(0, 0);
            this.Hitbox = new Rectangle(0, 0, 0, 0);
        }

        public void SetHitbox()
        {
            Hitbox = new Rectangle((int)nextPositionH.X, (int)nextPositionV.Y, spriteWidth, spriteWidth);
        }
        public void SetPosition()
        {
            Position = new Vector2(nextPositionH.X, Position.Y);
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
