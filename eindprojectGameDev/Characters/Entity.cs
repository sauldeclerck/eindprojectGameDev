using eindprojectGameDev.interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace eindprojectGameDev.Characters
{
    public abstract class Entity: IEntity
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Hitbox { get; set; }
    }
}
