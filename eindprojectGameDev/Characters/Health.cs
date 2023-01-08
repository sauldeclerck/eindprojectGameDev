namespace eindprojectGameDev.Characters
{
    public class Health
    {
        public int Lives { get; set; }
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; private set; }
        public Health(int lives, int health)
        {
            this.Lives = lives;
            this.CurrentHealth = health;
            MaxHealth = health;
        }
    }
}
