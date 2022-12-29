﻿using eindprojectGameDev.Characters.Enemies;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace eindprojectGameDev.World
{
    public static class GameManager
    {
        public static ContentManager Content;
        public static List<Block> defaultBlocks = new List<Block>();
        public static List<NPC> enemies = new List<NPC>();
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

        public static void DoDamage(int amount, NPC character)
        {
            character.TakeDamage(amount);
        }
    }
}
