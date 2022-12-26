using eindprojectGameDev.interfaces;
using eindprojectGameDev.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using SharpDX.WIC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Button = eindprojectGameDev.World.Button;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace eindprojectGameDev.Map
{
    internal class Start : IMenu
    {
        public bool isActive = true;
        private Rectangle LevelRectangle = new Rectangle(100,0,200,100);
        private Rectangle ExitRectangle = new Rectangle(100,300,200,100);
        Button buttonLvl1;
        Button buttonExit;
        public Start()
        {
            buttonLvl1 = new Button(LevelRectangle,"Red_Rectangle", "Level 1");
            buttonLvl1.onClick += delegate { isActive = false;GameState.gameState = GameStates.level1; };
            buttonExit = new Button(ExitRectangle,"Red_Rectangle", "Exit");
            buttonExit.onClick += delegate { isActive = false; GameState.gameState = GameStates.exit; };
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isActive == true)
            {
                buttonLvl1.Draw(spriteBatch);
                buttonExit.Draw(spriteBatch);
            }
            
        }

        public void Update(GameTime gameTime)
        {
            if (isActive == true)
            {
                buttonLvl1.Update();
                buttonExit.Update();
            }
        }
    }
}
