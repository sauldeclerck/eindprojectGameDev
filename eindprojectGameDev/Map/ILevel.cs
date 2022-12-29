using eindprojectGameDev.Characters.Enemies;
using eindprojectGameDev.Characters.Player;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace eindprojectGameDev.Map
{
    public interface ILevel
    {
        List<NPC> Enemies { get; set; }
        Hero Hero { get; set; }
        Block[,] BlockArray { get; set; }
        Texture2D BackgroundTexture { get; set; }
        Texture2D TileSetTexture { get; set; }
    }
}
