namespace eindprojectGameDev.Characters.Player
{
    public abstract class Player : Character
    {
        public PlayerMovement PlayerMovement { get; set; }
        public bool JumpInput { get; set; }
        public bool IsJumping;
        public float jumpSpeed = 5;
        public float startY { get; set; }
        public bool canJump { get; set; }
    }
}
