using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindprojectGameDev.Characters
{
    internal class Hitbox
    {
        private Rectangle hitboxRectangle;
        private int width, height;
        private Texture2D hitboxTexture;
        public Hitbox(Texture2D _hitboxTexture, int width, int height)
        {
            this.hitboxTexture = _hitboxTexture;
            this.width = width;
            this.height = height;
        }

        public void Update(IGameObject _gameObject)
        {
            hitboxRectangle = new Rectangle((int)_gameObject.Position.X, (int)_gameObject.Position.Y, width, height);
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(hitboxTexture, hitboxRectangle, Color.Transparent);
        }
    }
}
