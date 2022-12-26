using eindprojectGameDev.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindprojectGameDev.Characters.Enemies
{
    internal class porcupine : Enemy
    {
        public porcupine(int positionX, int positionY)
        {
            base.Position = new Vector2(positionX, positionY);
            base.nextPositionH = new Vector2(positionX, positionY);
            base.nextPositionV = new Vector2(positionX, positionY);
            base.Texture = GameManager.Content.Load<Texture2D>("Porcupine");
            base.spriteWidth = 160 / 5;
            base.animation.GetFramesFromTextureProperties(base.Texture.Width / 5 * 5, 0, 5, spriteWidth);
            base.Health = new Health(1, 20);
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
