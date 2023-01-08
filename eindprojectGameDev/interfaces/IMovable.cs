using Microsoft.Xna.Framework;

namespace eindprojectGameDev.interfaces
{
    public interface IMovable
    {
        public Rectangle nextHitboxH { get; set; }
        public Rectangle nextHitboxV { get; set; }
        public Vector2 nextPositionH { get; set; }
        public Vector2 nextPositionV { get; set; }
    }
}
