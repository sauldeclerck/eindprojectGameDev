using eindprojectGameDev.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindprojectGameDev.Characters
{
    public abstract class Character : Entity
    {
        public Animation[] Animations { get; set; }
        public Animation CurrentAnimation { get; set; }
        public Rectangle nextHitboxH { get; set; }
        public Rectangle nextHitboxV { get; set; }
        public Vector2 nextPositionH { get; set; }
        public Vector2 nextPositionV { get; set; }
        public Vector2 direction { get; set; }
        public Health Health { get; set; }
        public SpriteEffects Flip { get; set; }
    }
}
