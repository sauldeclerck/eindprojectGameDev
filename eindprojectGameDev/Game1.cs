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
        private Level1 lvl1;
        private Texture2D _heroTexture;
        private Texture2D _colorTexture;
        private Texture2D _heartTexture;
        private Texture2D _hitboxTexture;
        private Texture2D _BackgroundTexture;
        private Texture2D _BlockTexture;
        public GameStates gameState = GameStates.menu;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //hero setup
        private Hero hero;
        private HealthBar healthBarHero;
        private Hearts hearts;
        //level setup
        private Background background;
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
            
            //hero initializing
            hero = new Hero(_heroTexture, new PlayerMovement());
            healthBarHero = new HealthBar(_colorTexture);
            hearts = new Hearts(_heartTexture);
            background = new Background(_BackgroundTexture, 1920, 1080);
            lvl1 = new Level1(_BlockTexture);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _heroTexture = Content.Load<Texture2D>("GoblinHero");
            _colorTexture = Content.Load<Texture2D>("Red_rectangle");
            _heartTexture = Content.Load<Texture2D>("heart");
            _BackgroundTexture = Content.Load<Texture2D>("background");
            _BlockTexture = Content.Load<Texture2D>("tileset");
            _hitboxTexture = new Texture2D(GraphicsDevice, 1, 1);
            _hitboxTexture.SetData(new[] { Color.Red });

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            //update hero
            hero.Update(gameTime);
            healthBarHero.Update(hero);
            hearts.Update(hero);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            background.Draw(_spriteBatch);
            hero.Draw(_spriteBatch);
            healthBarHero.Draw(_spriteBatch);
            hearts.Draw(_spriteBatch);
            lvl1.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}