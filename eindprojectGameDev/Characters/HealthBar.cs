using eindprojectGameDev.interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindprojectGameDev.Characters
{
    internal class HealthBar
    {
        private Texture2D healthTexture;
        private Rectangle healthBarRectangle;

        public HealthBar(Texture2D healthTexture)
        {
            this.healthTexture = healthTexture;
        }
        public void Update(Vector2 Position, Health health)
        {
            healthBarRectangle = new Rectangle((int)Position.X+50, (int)Position.Y+20, health.health/2, 10);
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(healthTexture, healthBarRectangle, Color.Red);
        }
    }
}
