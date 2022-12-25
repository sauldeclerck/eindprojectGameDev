using eindprojectGameDev.Characters;
using eindprojectGameDev.interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace eindprojectGameDev.Map
{
    internal static class Background
    {
        private static Texture2D backgroundTexture;
    
        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, new Rectangle(0,0,GlobalSettings.Width, GlobalSettings.Height), Color.Purple);
        }
        public static void Initialize(Texture2D texture)
        {
            backgroundTexture = texture;
        }
    }
}
