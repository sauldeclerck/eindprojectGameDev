using eindprojectGameDev.Characters.Animations;
using eindprojectGameDev.interfaces;
using eindprojectGameDev.Map;
using eindprojectGameDev.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace eindprojectGameDev.Characters
{
    internal class Hero : Entity, ICharacter
    {
        #region initialize
        private Hearts Hearts;
        private PlayerMovement PlayerMovement;
        //public Rectangle hitbox;        
        #endregion

        public Hero(ContentManager content, int positionX, int positionY)
        {
            Jumpable = true;
            PlayerMovement = new PlayerMovement();
            Position = new Vector2(positionX, positionY);
            nextPositionH = new Vector2(positionX, positionY);
            nextPositionV = new Vector2(positionX, positionY);
            Texture = content.Load<Texture2D>("Cacodaemon Sprite Sheet");
            HealthBar = new HealthBar(content.Load<Texture2D>("Red_Rectangle"));
            Hearts = new Hearts(content.Load<Texture2D>("heart"));
            spriteWidth = 160 / 3;
            //animation.GetFramesFromTextureProperties(2 * 160, 0 * 96, 2, 96);
            Health = new Health(3, 100);
            Texture = content.Load<Texture2D>("GoblinHero");
        }
        public void Update(GameTime gameTime)
        {
            base.direction = PlayerMovement.ReadMovementInput();
            base.JumpInput = PlayerMovement.ReadIsJumping();
            base.Update(gameTime);
            Hearts.Update(this);
            Hitbox = new Rectangle((int)nextPositionH.X + 50, (int)nextPositionV.Y + 42, spriteWidth, spriteWidth);
            CheckEnemyHit();
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            base.Draw(_spriteBatch);
            Hearts.Draw(_spriteBatch);
        }

        public void CheckEnemyHit()
        {
            foreach (var item in GameManager.enemies)
            {
                if (Hitbox.Intersects(item.hitbox))
                {
                    GameManager.DoDamage(50, item);
                    TakeDamage(50);
                }
            }
        }

        private void Jump()
        {
            // credits => https://flatformer.blogspot.com/
            if (IsJumping)
            {
                if (canJump)
                {
                    nextPositionV += new Vector2(0, jumpSpeed);//Making it go up
                    jumpSpeed += 1;//Some math (explained later)
                }

                if (nextPositionV.Y >= startY)
                //If it's farther than ground
                {
                    nextPositionV = new Vector2(0, startY);//Then set it on
                    canJump = false;
                    IsJumping = false;
                }
            }
            else
            {
                if (JumpInput)
                {
                    IsJumping = true;
                    canJump = false;
                    jumpSpeed = -14;//Give it upward thrust
                }
            }
        }

    }
}