using eindprojectGameDev.Characters;
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
        private Texture2D _heroTexture;
        private Texture2D _colorTexture;
        private Texture2D _heartTexture;
        private Texture2D _Hitbox;
        public GameStates gameState = GameStates.menu;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //hero setup
        private Hero hero;
        private Hitbox hitboxHero;
        private HealthBar healthBarHero;
        
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
            hero = new Hero(_heroTexture, _heartTexture, new PlayerMovement());
            hitboxHero = new Hitbox(_Hitbox, 96 - 50, 96 - 45);
            healthBarHero = new HealthBar(_colorTexture);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _heroTexture = Content.Load<Texture2D>("GoblinHero");
            _colorTexture = Content.Load<Texture2D>("Red_rectangle");
            _Hitbox = new Texture2D(GraphicsDevice, 1, 1);
            _Hitbox.SetData(new[] { Color.White });
            _heartTexture = Content.Load<Texture2D>("heart");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            //update hero
            hero.Update(gameTime);
            hitboxHero.Update(hero);
            healthBarHero.Update(hero);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            hero.Draw(_spriteBatch);
            hitboxHero.Draw(_spriteBatch);
            healthBarHero.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}