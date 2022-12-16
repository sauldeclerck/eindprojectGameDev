using eindprojectGameDev.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindprojectGameDev.Map
{
    internal class Background : IGameObject
    {
        public Rectangle BoundingBox { get; set; }
        public bool Passable { get; set; }
        public Color Color { get; set; }
        public Texture2D texture;
        public Vector2 Position { get; set; }
        public Health Health { get; set; }
        //public CollideWithEvent CollideWithEvent { get; set; }

        public Background(Texture2D texture, int x, int y)
        {
            Position = new Vector2(0, 0);
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, GlobalSettings.Width, GlobalSettings.Height);
            Color = Color.Purple;
            this.texture = texture;
            //CollideWithEvent = new NoEvent();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, BoundingBox, Color);
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
