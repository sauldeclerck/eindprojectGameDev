using eindprojectGameDev.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindprojectGameDev.Map
{
    public class PowerUp : Entity
    {
        public Rectangle BoundingBox { get; set; }
        public bool isActive { get; set; } = true;
        public PowerUpType.PowerUpTypes Type { get; set; }
        public PowerUp(ContentManager content, int positionX, int positionY, PowerUpType.PowerUpTypes power)
        {
            Type = power;
            base.Texture = content.Load<Texture2D>("tileset");
            base.Position = new Vector2(positionX, positionY);
            switch (power)
            {
                case PowerUpType.PowerUpTypes.speed:
                    base.Hitbox = new Rectangle(0, 16 * 8, 16, 16);
                    break;
                case PowerUpType.PowerUpTypes.damage:
                    base.Hitbox = new Rectangle(16, 16 * 8, 16, 16);
                    break;
                default:
                    break;
            }
            BoundingBox = new Rectangle(positionX, positionY, 16, 16);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isActive)
            {
                spriteBatch.Draw(Texture, Position, Hitbox, Color.White);
                //spriteBatch.Draw(Texture, Position, Hitbox, Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0f);
            }
        }
    }
}
