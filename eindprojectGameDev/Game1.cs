using eindprojectGameDev.Map;
using eindprojectGameDev.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using System.Windows.Forms;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Color = Microsoft.Xna.Framework.Color;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace eindprojectGameDev
{
    public class Game1 : Game
    {
        public GameStates? previousState;
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        private readonly ScreenManager _screenManager;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();

            _graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferHeight = GlobalSettings.Height;
            _graphics.PreferredBackBufferWidth = GlobalSettings.Width;
            _graphics.ApplyChanges();

            LoadStart();
            //GameManager.SetContent(Content);
            //LevelManager.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            //LevelManager.Update(gameTime);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
                Exit();
            //todo => add gamestates
            if (previousState != GameState.gameState)
            {
                switch (GameState.gameState)
                {
                    case GameStates.menu:
                        LoadStart();
                        break;
                    case GameStates.level1:
                        LoadLevel1();
                        break;
                    case GameStates.level2:
                        LoadLevel2();
                        break;
                    case GameStates.gameover:
                        break;
                    case GameStates.exit:
                        Application.Exit();
                        break;
                    default:
                        break;
                }
            }
            previousState = GameState.gameState;
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            //LevelManager.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void LoadStart()
        {
            _screenManager.LoadScreen(new Start(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadLevel1()
        {
            _screenManager.LoadScreen(new Level1(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadLevel2()
        {
            _screenManager.LoadScreen(new Level2(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
    }
}