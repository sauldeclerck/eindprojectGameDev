using eindprojectGameDev.Characters;
using eindprojectGameDev.Map;
using eindprojectGameDev.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace eindprojectGameDev
{
    public class Game1 : Game
    {
        private Texture2D _BackgroundTexture;
        public GameStates gameState = GameStates.menu;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            
            _graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferHeight = GlobalSettings.Height;
            _graphics.PreferredBackBufferWidth = GlobalSettings.Width;
            _graphics.ApplyChanges();
            
            GameManager.SetContent(Content);
            LevelManager.Initialize();
            Background.Initialize(_BackgroundTexture);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _BackgroundTexture = Content.Load<Texture2D>("background");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            LevelManager.Update(gameTime);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //todo => add gamestates
            switch (gameState)
            {
                case GameStates.menu:
                    break;
                case GameStates.level1:
                    break;
                case GameStates.level2:
                    break;
                case GameStates.gameover:
                    break;
                default:
                    break;
            }
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            Background.Draw(_spriteBatch);
            LevelManager.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}