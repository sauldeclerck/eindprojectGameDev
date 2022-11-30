using Microsoft.Xna.Framework;

namespace eindprojectGameDev
{
    internal interface IInputReader
    {
        Vector2 ReadMovementInput();
        bool ReadIsFighting();
    }
}