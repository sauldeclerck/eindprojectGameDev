using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindprojectGameDev.Characters
{
    internal class Health
    {
        public int lives, health, maxHealth;
        public Health(int lives, int health)
        {
            this.lives = lives;
            this.health = health;
            maxHealth = health;
        }
    }
}
