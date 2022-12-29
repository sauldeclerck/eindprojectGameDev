using Microsoft.Xna.Framework;

namespace eindprojectGameDev.interfaces
{
    public interface IInputReader
    {
        Vector2 ReadMovementInput();
        bool ReadIsFighting();
        bool ReadIsJumping();
    }
}