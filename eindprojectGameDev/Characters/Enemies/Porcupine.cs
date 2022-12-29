using eindprojectGameDev.Animations;
using eindprojectGameDev.interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace eindprojectGameDev.Characters.Enemies
{
    public class porcupine : NPC, ICharacter
    {
        public porcupine(int positionX, int positionY, ContentManager content)
        {
            EnemyType = EnemyTypes.EnemyType.Porcupine;
            isActive = true;
            vector = new Vector2(1, 0);
            StartPosition = new Vector2(positionX, positionY);
            Position = StartPosition;
            nextPositionH = new Vector2(positionX, positionY);
            nextPositionV = new Vector2(positionX, positionY);
            Texture = content.Load<Texture2D>("Porcupine");
            spriteWidth = 160 / 5;
            CurrentAnimation = new Animation();
            CurrentAnimation.GetFramesFromTextureProperties(base.Texture.Width / 5 * 5, 0, 5, spriteWidth);
            Health = new Health(1, 20);
        }
        public override void Update(GameTime gameTime)
        {
            if (isActive)
            {
                SetNextPositions();
                Hitbox = new Rectangle((int)nextPositionH.X, (int)nextPositionV.Y, spriteWidth, spriteWidth);
                Position = new Vector2(nextPositionH.X, Position.Y);
                CurrentAnimation.Update(gameTime);
                isActive = CheckActive();
            }
        }
        public override void Draw(SpriteBatch _spriteBatch)
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
