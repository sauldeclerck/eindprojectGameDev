using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace eindprojectGameDev.interfaces
{
    public interface IGameObject
    {
        public Rectangle BoundingBox { get; set; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
