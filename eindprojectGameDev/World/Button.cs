using eindprojectGameDev.interfaces;
using eindprojectGameDev.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace eindprojectGameDev.World
{
    public class Button
    {
        private SpriteFont SpriteFont;
        private readonly Texture2D texture;
        private Rectangle mouseRect;
        private MouseState MouseState;
        private bool isHovering;
        private MouseState previousMouse;
        private Rectangle Rectangle;
        public string title;
        public Color color;
        Vector2 textPosition;
        public Button(Rectangle rectangle, string fileName, string title, ContentManager content)
        {
            texture = content.Load<Texture2D>(fileName);
            SpriteFont = content.Load<SpriteFont>("GameFont");
            Rectangle = rectangle;
            this.title = title;
        }

        public event EventHandler onClick;
        public void Draw(SpriteBatch _spriteBatch)
        {
            color = Color.White;
            if (isHovering) {
                color = Color.Purple; 
            }
            textPosition = new Vector2(Rectangle.X + Rectangle.Width / 4, Rectangle.Y + Rectangle.Height / 3);
            _spriteBatch.Draw(texture, Rectangle, color);
            _spriteBatch.DrawString(SpriteFont, title, textPosition, Color.DarkCyan);
        }

        public void Update()
        {
            previousMouse = MouseState;
            MouseState = Mouse.GetState();
            mouseRect  = new Rectangle(MouseState.X, MouseState.Y, 1, 1);
            isHovering = false;
            if (mouseRect.Intersects(Rectangle))
            {
                isHovering = true;
            }
            if (MouseState.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed && isHovering)
                onClick?.Invoke(this, EventArgs.Empty);
        }
    }
}
