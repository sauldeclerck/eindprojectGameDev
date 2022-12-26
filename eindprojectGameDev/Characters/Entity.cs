using eindprojectGameDev.interfaces;
using eindprojectGameDev.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eindprojectGameDev.Characters
{
    internal class Entity //: ICharacter
    {
        //public enum HeroStates { walkingRight, walkingLeft, idle, attackingRight, attackingLeft, dead }
        //public Health Health { get => health; set => this.health = value; }
        //public Vector2 Position { get; set; }
        //private Health health;
        //public Rectangle hitbox;
        //public Rectangle nextHitboxH { get; set; }
        //public Rectangle nextHitboxV { get; set; }
        //public Vector2 nextPositionH { get; set; }
        //public Vector2 nextPositionV { get; set; }
        //private IInputReader inputReader;
        //public Vector2 position;
        //private int speed = 5;
        //private Texture2D Texture;
        //private int spriteWidth = 96, spriteHeight = 96;
        //public HeroStates State = HeroStates.idle;
        //private Animation currentAnimation;
        //private bool onGround = false;
        //public virtual void Draw(SpriteBatch _spriteBatch)
        //{
        //    _spriteBatch.Draw(Texture,position, Color.White);
        //}

        //public virtual void Update(GameTime gameTime)
        //{
        //    Position = new Vector2((int)position.X, (int)position.Y);
            
        //    //nextPositionH = position;
        //    //nextPositionV = position;
        //    //hitbox = new Rectangle((int)position.X + 50, (int)position.Y + 50, spriteWidth - 50, spriteHeight - 50);
        //    //nextPositionV = Gravity(nextPositionV);
        //    //direction *= speed;
        //    //nextPositionH += direction;
        //    //nextHitboxH = new Rectangle((int)nextPositionH.X + 50, (int)nextPositionH.Y + 50, spriteWidth - 50, spriteHeight - 50);
        //    //nextHitboxV = new Rectangle((int)nextPositionV.X + 50, (int)nextPositionV.Y + 50, spriteWidth - 50, spriteHeight - 50);
        //}
    }
}
