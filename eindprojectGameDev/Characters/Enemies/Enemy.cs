using eindprojectGameDev.Animations;
using eindprojectGameDev.interfaces;
using eindprojectGameDev.Map;
using eindprojectGameDev.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static eindprojectGameDev.Characters.Enemies.porcupine;

namespace eindprojectGameDev.Characters.Enemies
{
    public abstract class Enemy : Character
    {
        public bool isActive { get; set; }
        public Vector2 vector { get; set; }
        

    }
}
