using eindprojectGameDev.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Screens;
using System.Windows.Forms;
using Button = eindprojectGameDev.World.Button;

namespace eindprojectGameDev.Map
{
    public class Start : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        private string Title = "Goblins' Knight";
        private string powerupsEnabled { get; set; } = "on";
        private Vector2 titlePosition = new Vector2(GlobalSettings.Width / 3 - 100, 100);
        private Rectangle Level1Rectangle = new Rectangle(GlobalSettings.Width/2-100, 300, 200, 100);
        private Rectangle Level2Rectangle = new Rectangle(GlobalSettings.Width / 2-100, 450, 200, 100);
        private Rectangle Level3Rectangle = new Rectangle(GlobalSettings.Width / 2-100, 600, 200, 100);
        private Texture2D backgroundTexture;
        private Rectangle backGroundRectangle = new Rectangle(0, 0, GlobalSettings.Width, GlobalSettings.Height);
        private Rectangle PowerUpRectangle = new Rectangle(GlobalSettings.Width / 2-100, 750, 200, 100);
        private Rectangle ExitRectangle = new Rectangle(GlobalSettings.Width / 2-100, 900, 200, 100);
        Button buttonLvl1;
        Button buttonLvl2;
        Button buttonLvl3;
        Button buttonPowerUp;
        Button buttonExit;
        public Start(Game1 game) : base(game) { }

        public override void LoadContent()
        {
            base.LoadContent();
			if (!GameManager.musicStarted)
			{
				GameManager.song = Content.Load<Song>("music");
				MediaPlayer.IsRepeating = true;
				MediaPlayer.Play(GameManager.song);
				GameManager.musicStarted = true;
			}
			backgroundTexture = Content.Load<Texture2D>("background");
            buttonLvl1 = new Button(Level1Rectangle, "darkred", "Level 1", Content);
            buttonLvl1.onClick += delegate { GameState.gameState = GameStates.level1; };
            buttonLvl2 = new Button(Level2Rectangle, "darkred", "Level 2", Content);
            buttonLvl2.onClick += delegate { GameState.gameState = GameStates.level2; };
            buttonLvl3 = new Button(Level3Rectangle, "darkred", "Level 3", Content);
            buttonLvl3.onClick += delegate { GameState.gameState = GameStates.level3; };
            buttonPowerUp = new Button(PowerUpRectangle, "darkred", $"Powerups: {powerupsEnabled}", Content);
            buttonPowerUp.onClick += delegate { GameManager.EnablePowerUps = !GameManager.EnablePowerUps; };
            buttonExit = new Button(ExitRectangle, "darkred", "Exit", Content);
            buttonExit.onClick += delegate { Application.Exit(); };
        }

        public override void Update(GameTime gameTime)
        {
            powerupsEnabled = GameManager.EnablePowerUps ? "on" : "off";
            buttonPowerUp.title = $"P-up: {powerupsEnabled}";
            buttonLvl1.Update();
            buttonLvl2.Update();
            buttonLvl3.Update();
            buttonPowerUp.Update();
            buttonExit.Update();
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.White);
            Game._spriteBatch.Begin();
            Game._spriteBatch.Draw(backgroundTexture, backGroundRectangle, Color.Purple);
            Game._spriteBatch.DrawString(Content.Load<SpriteFont>("GameFont"), Title, titlePosition, Color.Firebrick,0f, new Vector2(0,0), 3f, SpriteEffects.None, 0f);
            buttonLvl1.Draw(Game._spriteBatch);
            buttonLvl2.Draw(Game._spriteBatch);
            buttonLvl3.Draw(Game._spriteBatch);
            buttonPowerUp.Draw(Game._spriteBatch);
            buttonExit.Draw(Game._spriteBatch);
            Game._spriteBatch.End();
        }
    }
}
