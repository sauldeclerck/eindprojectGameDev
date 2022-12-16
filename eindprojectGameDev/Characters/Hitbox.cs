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
    internal class Hitbox
    {
        private Rectangle hitboxRectangle;
        private int width, height;
        private Texture2D hitboxTexture;
        public Hitbox(Texture2D hitboxTexture, int width, int height)
        {
            this.hitboxTexture = hitboxTexture;
            this.width = width;
            this.height = height;
        }

        public void Update(ICharacter _character)
        {
            hitboxRectangle = new Rectangle((int)_character.Position.X+50, (int)_character.Position.Y+50, width, height);
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(hitboxTexture, hitboxRectangle, Color.Red);
        }
    }
}
