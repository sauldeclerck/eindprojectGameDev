using eindprojectGameDev.Animations;
using eindprojectGameDev.interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace eindprojectGameDev.Characters.Enemies
{
    public class Daemon : NPC, ICharacter
    {
        public Daemon(int positionX, int positionY, ContentManager content)
        {
            EnemyType = EnemyTypes.EnemyType.Daemon;
            isActive = true;
            vector = new Vector2(1, 0);
            StartPosition = new Vector2(positionX, positionY);
            Position = StartPosition;
            nextPositionH = new Vector2(positionX, positionY);
            nextPositionV = new Vector2(positionX, positionY);
            spriteWidth = 512 / 8;
            Texture = content.Load<Texture2D>("Cacodaemon Sprite Sheet");
            CurrentAnimation = new Animation();
            CurrentAnimation.GetFramesFromTextureProperties(base.Texture.Width / 8 * 6, 0, 6, 256 / 4);
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
