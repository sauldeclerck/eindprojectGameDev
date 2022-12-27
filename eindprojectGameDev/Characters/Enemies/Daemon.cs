using eindprojectGameDev.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindprojectGameDev.Characters.Enemies
{
    internal class Daemon : Enemy
    {
        public Daemon(int positionX, int positionY, ContentManager content)
        {
            base.Position = new Vector2(positionX, positionY);
            base.nextPositionH = new Vector2(positionX, positionY);
            base.nextPositionV = new Vector2(positionX, positionY);
            base.Texture = content.Load<Texture2D>("Cacodaemon Sprite Sheet");
            base.spriteWidth = 160/3;
            base.animation.GetFramesFromTextureProperties(base.Texture.Width/8*6, 0, 6, 256/4);
            base.Health = new Health(1, 50);
            base.Type = EnemyType.daemon;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch _spriteBatch)
        {
            base.Draw(_spriteBatch);
        }
    }
}
