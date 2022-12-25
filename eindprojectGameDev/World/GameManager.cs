using eindprojectGameDev.interfaces;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindprojectGameDev.World
{
    internal static class GameManager
    {
        public static ContentManager Content;
        public static List<Block> defaultBlocks = new List<Block>();
        public static void ResetBlocks()
        {
            defaultBlocks.Clear();
        }
        public static void SetContent(ContentManager Content)
        {
            GameManager.Content = Content;
        }
    }
}
