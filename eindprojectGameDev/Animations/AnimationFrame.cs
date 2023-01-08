using eindprojectGameDev.interfaces;
using Microsoft.Xna.Framework;

namespace eindprojectGameDev.Animations
{
    public class AnimationFrame : IAnimation
    {
        public Rectangle SourceRectangle { get; set; }

        public AnimationFrame(Rectangle sourceRectangle)
        {
            SourceRectangle = sourceRectangle;
        }
    }
}
