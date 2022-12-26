using eindprojectGameDev.Characters;
using eindprojectGameDev.Characters.Enemies;
using eindprojectGameDev.interfaces;
using eindprojectGameDev.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace eindprojectGameDev.Map
{
    internal static class LevelManager
    {
        static Block[,] tileset;
        //hero setup
        public static Hero hero;
        public static List<Enemy> enemies = new List<Enemy>();
        private static HealthBar healthBarHero;
        private static Hearts hearts;

        public static void Initialize()
        {
            Level1 lvl1 = new Level1(GameManager.Content.Load<Texture2D>("tileset"));
            hero = new Hero();
            healthBarHero = new HealthBar(GameManager.Content.Load<Texture2D>("Red_rectangle"));
            hearts = new Hearts(GameManager.Content.Load<Texture2D>("Heart"));
        }
        public static void AddEnemy(Enemy enemy)
        {
            enemies.Add(enemy);
        }
        public static void ChangeBackground(Texture2D _backgroundTexture)
        {
            Background.Initialize(_backgroundTexture);
        }
        public static void ChangeSet(Block[,] tileset)
        {
            LevelManager.tileset = tileset;
            foreach (var item in tileset)
            {
                GameManager.defaultBlocks.Add(item);
            }
        }
        public static void Update(GameTime gameTime)
        {
            hero.Update(gameTime);
            foreach (var item in enemies)
            {
                item.Update(gameTime);
            }
            healthBarHero.Update(hero.Position, hero.Health);
            hearts.Update(hero);
        }
        public static void Draw(SpriteBatch _spriteBatch)
        {
            hero.Draw(_spriteBatch);
            healthBarHero.Draw(_spriteBatch);
            hearts.Draw(_spriteBatch);
            foreach (var item in enemies)
            {
                item.Draw(_spriteBatch);
            }
            foreach (var tile in tileset)
            {
                if (tile != null)
                {
                    tile.Draw(_spriteBatch);
                }
            }
        }

        public static void DoDamage(int amount, ICharacter character)
        {
            character.TakeDamage(amount);
        }
    }
}
