using Microsoft.Xna.Framework;

namespace eindprojectGameDev.interfaces
{
    internal interface IInputReader
    {
        Vector2 ReadMovementInput();
        bool ReadIsFighting();
        bool ReadIsJumping();
    }
}