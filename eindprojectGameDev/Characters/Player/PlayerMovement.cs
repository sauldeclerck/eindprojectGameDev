using eindprojectGameDev.interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace eindprojectGameDev.Characters.Player
{
    public class PlayerMovement : IInputReader
    {
        public Vector2 ReadMovementInput()
        {
            var state = Keyboard.GetState();
            var direction = Vector2.Zero;
            if (state.IsKeyDown(Keys.Left)) direction.X -= 1;
            if (state.IsKeyDown(Keys.Right)) direction.X += 1;
            return direction;
        }
        public bool ReadIsFighting()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Space);
        }
        public bool ReadIsJumping()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Up);
        }
    }
}
