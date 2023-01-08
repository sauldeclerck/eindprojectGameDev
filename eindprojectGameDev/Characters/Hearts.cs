using eindprojectGameDev.interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace eindprojectGameDev.Characters
{
    public class Hearts
    {
        private List<Rectangle> hearts = new List<Rectangle>();
        private Texture2D heartTexture;

        public Hearts(Texture2D heartTexture)
        {
            this.heartTexture = heartTexture;
        }
        public void Update(ICharacter _character)
        {
            hearts.Clear();
            for (int i = 0; i < _character.Health.Lives; i++)
            {
                hearts.Add(new Rectangle(25 * i, 0, 20, 20));
            }
        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            // draw lives according to the amount of lives
            for (int i = 0; i < hearts.Count; i++)
            {
                _spriteBatch.Draw(heartTexture, hearts[i], Color.White);
            }
        }
    }
}
