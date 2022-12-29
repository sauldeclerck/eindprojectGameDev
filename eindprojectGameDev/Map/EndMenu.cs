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
    public class EndMenu : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        private Vector2 TextVector = new Vector2(GlobalSettings.Width / 2, 200);
        private Rectangle RestartRectangle = new Rectangle(GlobalSettings.Width / 2, 500, 200, 100);
        private Rectangle MenuRectangle = new Rectangle(GlobalSettings.Width / 2, 700, 200, 100);
        private Texture2D backgroundTexture;
        private string Text;
        private Rectangle backGroundRectangle = new Rectangle(0, 0, GlobalSettings.Width, GlobalSettings.Height);
        Button ButtonRestart;
        Button buttonMenu;
        SpriteFont SpriteFont;
        Color color;
        public EndMenu(Game1 game) : base(game) { }

        public override void LoadContent()
        {
            base.LoadContent();
            Text = GameState.gameState == GameStates.victory ? "VICTORY" : "GAME OVER";
            color = GameState.gameState == GameStates.victory ? Color.Green : Color.Red;
            SpriteFont = Game.Content.Load<SpriteFont>("GameFont");
            backgroundTexture = Content.Load<Texture2D>("background");
            ButtonRestart = new Button(RestartRectangle, "Red_Rectangle", "Restart", Content);
            ButtonRestart.onClick += delegate { GameState.gameState = GameStates.level1; };
            buttonMenu = new Button(MenuRectangle, "Red_Rectangle", "Menu", Content);
            buttonMenu.onClick += delegate { GameState.gameState = GameStates.menu; };
        }

        public override void Update(GameTime gameTime)
        {
            ButtonRestart.Update();
            buttonMenu.Update();
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.White);
            Game._spriteBatch.Begin();
            Game._spriteBatch.Draw(backgroundTexture, backGroundRectangle, Color.Purple);
            Game._spriteBatch.DrawString(SpriteFont, Text, TextVector, color);
            ButtonRestart.Draw(Game._spriteBatch);
            buttonMenu.Draw(Game._spriteBatch);
            Game._spriteBatch.End();
        }
    }
}
