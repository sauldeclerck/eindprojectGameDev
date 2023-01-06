﻿using eindprojectGameDev.Characters.Enemies;
using eindprojectGameDev.Characters.Player;
using eindprojectGameDev.Map;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace eindprojectGameDev.World
{
    public static class GameManager
    {
		public static bool musicStarted = false;
		public static Song song;
		public static bool EnablePowerUps = true;
		public static ContentManager Content;
		public static List<Block> defaultBlocks = new List<Block>();
		public static List<NPC> enemies = new List<NPC>();
		public static Hero Hero;
		public static List<PowerUp> PowerUps = new List<PowerUp>();
		public static MouseState MouseState = new MouseState();
		public static void Reset()
		{
			PowerUps.Clear();
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
