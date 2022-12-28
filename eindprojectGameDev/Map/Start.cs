using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eindprojectGameDev.World;
using SharpDX.Direct2D1;
using System.Windows.Forms;
using Button = eindprojectGameDev.World.Button;

namespace eindprojectGameDev.Map
{
    public class Start : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        private Rectangle Level1Rectangle = new Rectangle(100, 0, 200, 100);
        private Rectangle Level2Rectangle = new Rectangle(100, 150, 200, 100);
        private Texture2D backgroundTexture;
        private Rectangle backGroundRectangle = new Rectangle(0, 0, GlobalSettings.Width, GlobalSettings.Height);
        private Rectangle ExitRectangle = new Rectangle(100, 300, 200, 100);
        Button buttonLvl1;
        Button buttonLvl2;
        Button buttonExit;
        public Start(Game1 game) : base(game) { }

        public override void LoadContent()
        {
            base.LoadContent();
            backgroundTexture = Content.Load<Texture2D>("background");
            buttonLvl1 = new Button(Level1Rectangle, "Red_Rectangle", "Level 1", Content);
            buttonLvl1.onClick += delegate { Game.LoadLevel1(); };
            buttonLvl2 = new Button(Level2Rectangle, "Red_Rectangle", "Level 2", Content);
            buttonLvl2.onClick += delegate { Game.LoadLevel2(); };
            buttonExit = new Button(ExitRectangle, "Red_Rectangle", "Exit", Content);
            buttonExit.onClick += delegate { Application.Exit(); };
        }

        public override void Update(GameTime gameTime)
        {
            buttonLvl1.Update();
            buttonLvl2.Update();
            buttonExit.Update();
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.White);
            Game._spriteBatch.Begin();
            Game._spriteBatch.Draw(backgroundTexture, backGroundRectangle, Color.Purple);
            buttonLvl1.Draw(Game._spriteBatch);
            buttonLvl2.Draw(Game._spriteBatch);
            buttonExit.Draw(Game._spriteBatch);
            Game._spriteBatch.End();
        }
    }
}
