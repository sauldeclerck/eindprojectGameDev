using eindprojectGameDev.Characters.Enemies;
using eindprojectGameDev.interfaces;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindprojectGameDev.World
{
    public static class GameManager
    {
        public static ContentManager Content;
        public static List<Block> defaultBlocks = new List<Block>();
        public static List<ICharacter> enemies = new List<ICharacter>();
        public static MouseState MouseState = new MouseState();
        public static void Reset()
        {
            enemies.Clear();
            defaultBlocks.Clear();
        }
        public static void SetContent(ContentManager Content)
        {
            GameManager.Content = Content;
        }

        public static void DoDamage(int amount, ICharacter character)
        {
            character.TakeDamage(amount);
        }
    }
}
