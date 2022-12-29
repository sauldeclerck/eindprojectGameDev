using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace eindprojectGameDev.interfaces
{
    public interface ICollidable
    {
        bool CheckCollision(Rectangle rec);
        void Draw(SpriteBatch spriteBatch);
    }
}
