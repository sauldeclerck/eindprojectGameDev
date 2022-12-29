using eindprojectGameDev.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace eindprojectGameDev.Characters.Enemies
{
    public class Enemy : NPC
    {
        public Enemy(int positionX, int positionY, ContentManager content, EnemyTypes.EnemyType enemyType)
        {
            EnemyType = EnemyType;
            isActive = true;
            vector = new Vector2(1, 0);
            StartPosition = new Vector2(positionX, positionY);
            Position = StartPosition;
            nextPositionH = new Vector2(positionX, positionY);
            nextPositionV = new Vector2(positionX, positionY);
            //Texture = content.Load<Texture2D>("Cacodaemon Sprite Sheet");
            CurrentAnimation = new Animation();
            switch (enemyType)
            {
                case EnemyTypes.EnemyType.Porcupine:
                    spriteWidth = 160 / 5;
                    Texture = content.Load<Texture2D>("Porcupine");
                    CurrentAnimation.GetFramesFromTextureProperties(base.Texture.Width / 5 * 5, 0, 5, spriteWidth);
                    break;
                case EnemyTypes.EnemyType.Daemon:
                    spriteWidth = 512 / 8;
                    Texture = content.Load<Texture2D>("Cacodaemon Sprite Sheet");
                    CurrentAnimation.GetFramesFromTextureProperties(base.Texture.Width / 8 * 6, 0, 6, 256 / 4);
                    break;
                default:
                    break;
            }
            Health = new Health(1, 50);
        }

        public override void Update(GameTime gameTime)
        {
            if (isActive)
            {
                SetNextPositions();
                SetHitbox();
                SetPosition();
                CurrentAnimation.Update(gameTime);
                isActive = CheckActive();
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch)
        {
            if (isActive)
            {
                if (CurrentAnimation.CurrentFrame != null)
                {
                    DrawAnimation(_spriteBatch);
                }
            }
        }
    }
}
