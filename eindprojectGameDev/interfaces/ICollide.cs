﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindprojectGameDev.interfaces
{
    internal interface ICollidable
    {
        bool CheckCollision(Rectangle rec);
        void Draw(SpriteBatch spriteBatch);
    }
}
