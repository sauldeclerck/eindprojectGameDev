using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace eindprojectGameDev.Characters
{
    public class HealthBar
    {
        private Texture2D healthTexture;
        private Rectangle healthBarRectangle;

        public HealthBar(Texture2D healthTexture)
        {
            this.healthTexture = healthTexture;
        }
        public void Update(Vector2 Position, Health health)
        {
            healthBarRectangle = new Rectangle((int)Position.X + 50, (int)Position.Y + 20, health.health / 2, 10);
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(healthTexture, healthBarRectangle, Color.Red);
        }
    }
}
