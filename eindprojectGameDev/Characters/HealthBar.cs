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
        public void Update(IGameObject _gameObject)
        {
            healthBarRectangle = new Rectangle((int)_gameObject.Position.X, (int)_gameObject.Position.Y-20, _gameObject.Health.health/2, 10);
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(healthTexture, healthBarRectangle, Color.Red);
        }
    }
}
