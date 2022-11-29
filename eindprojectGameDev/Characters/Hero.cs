using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace eindprojectGameDev.Characters
{
    internal class Hero : IGameObject
    {
        private IInputReader inputReader;
        private Vector2 positie;
        private int speed = 5;
        private Texture2D texture;
        private int spriteWidth = 96;
        private int spriteHeight = 96;
        private Animation animationIdle;
        private Animation animationWalk;
        private Animation animationDie;
        private Animation animationFight;
        public enum HeroStates { walkingRight, walkingLeft, idle, attacking }
        public HeroStates State = HeroStates.idle;
        public Hero(Texture2D texture, IInputReader inputReader)
        {
            this.texture = texture;
            this.inputReader = inputReader;

            animationFight = new Animation();
            /*animationIdle = new Animation();
            animationDie = new Animation();
            animationWalk = new Animation();
            
            animationIdle.GetFramesFromTextureProperties(2 * 160, 0 * 96, 2, 96);
            animationWalk.GetFramesFromTextureProperties(8 * 160, 1 * 96, 8, 96);
            animationDie.GetFramesFromTextureProperties(6 * 160, 3 * 96, 7, 96);
            */
            animationFight.GetFramesFromTextureProperties(7 * 160, 2 * 96, 7, 96);
            positie = new Vector2(100, 100);
        }

        public void Update(GameTime gameTime)
        {
            var direction = inputReader.ReadInput();
            //checkOutOfBounds hozizontal
            if (positie.X <= 0 - spriteWidth/2)
            {
                positie.X = 0 - spriteWidth/2;
            }
            else if (positie.X >= GlobalSettings.Width-spriteWidth*2)
            {
                positie.X = GlobalSettings.Width-spriteWidth*2;
            }
            //check out of bounds vertical
            if (positie.Y > GlobalSettings.Height-spriteHeight)
            {
                positie.Y = GlobalSettings.Height - spriteHeight;
            }
            else if (positie.Y < 0)
            {
                positie.Y = 0;
            }
            //create New Position
            direction *= speed;
            positie += direction;
            animationFight.Update(gameTime);
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(texture, positie, animationFight.CurrentFrame.SourceRectangle, Color.White,0,new Vector2(0,0),1f,SpriteEffects.FlipHorizontally,1);
        }
    }
}
