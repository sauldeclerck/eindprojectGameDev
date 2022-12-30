using eindprojectGameDev.Characters.Enemies;
using eindprojectGameDev.Characters.Player;
using eindprojectGameDev.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindprojectGameDev.Map
{
    public class BaseLevel : GameScreen, ILevel
    {
        public GameStates nextState, previousState, currentState;
        public Rectangle BackGroundRectangle = new Rectangle(0, 0, GlobalSettings.Width, GlobalSettings.Height);
        public char[,] CharArray { get; set; }
        public Block[,] BlockArray { get; set; }
        public Texture2D BackgroundTexture { get; set; }
        public Texture2D TileSetTexture { get; set; }
        public Color BackGroundColor { get; set; }
        private new Game1 Game => (Game1)base.Game;
        public BaseLevel(Game game) : base(game){}

        public override void LoadContent()
        {
            base.LoadContent();
            GameManager.Reset();
            //PowerUp.ForEach(e => GameManager.PowerUps.Add(e));
            BackgroundTexture = Content.Load<Texture2D>("background");
            TileSetTexture = Content.Load<Texture2D>("tileset");
            BlockArray = BlockFactory.CreateBlocks(CharArray, TileSetTexture, Game.Content);
            foreach (var item in BlockArray)
            {
                GameManager.defaultBlocks.Add(item);
            }
        }

        public override void Update(GameTime gameTime)
        {
            GameManager.Hero.Update(gameTime);
            GameManager.enemies.ForEach(e => e.Update(gameTime));
            GameState.gameState = (GameManager.Hero.Hitbox.Right > GlobalSettings.Width) ? nextState: currentState;
            if (GameManager.Hero.Health.health <= 0) GameState.gameState = GameStates.gameover;
            if (GameManager.Hero.Hitbox.Left < 0) GameState.gameState = previousState;
        }
        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(new Color(16, 139, 204));
            Game._spriteBatch.Begin();
            Game._spriteBatch.Draw(BackgroundTexture, BackGroundRectangle, BackGroundColor);
            GameManager.PowerUps.ForEach(e => e.Draw(Game._spriteBatch));
            GameManager.Hero.Draw(Game._spriteBatch);
            GameManager.enemies.ForEach(e => e.Draw(Game._spriteBatch));
            foreach (var item in GameManager.defaultBlocks)
            {
                if (item != null) item.Draw(Game._spriteBatch);
            }
            Game._spriteBatch.End();
        }

    }
}
