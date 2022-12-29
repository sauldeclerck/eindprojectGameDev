using eindprojectGameDev.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace eindprojectGameDev.interfaces
{
    public interface ICharacter
    {
        public Texture2D Texture { get; set; }
        public Rectangle Hitbox { get; set; }
        public Vector2 Position { get; set; }
        public Health Health { get; set; }
        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch spriteBatch);
        public void TakeDamage(int amount);
    }
}
