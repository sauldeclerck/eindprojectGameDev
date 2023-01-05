using eindprojectGameDev.Characters.Enemies;
using eindprojectGameDev.Characters.Player;
using eindprojectGameDev.World;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace eindprojectGameDev.Map
{
    public interface ILevel
    {
        Block[,] BlockArray { get; set; }
        Texture2D BackgroundTexture { get; set; }
        Texture2D TileSetTexture { get; set; }
        GameStates nextState { get; set; }
        GameStates previousState { get; set; }
        GameStates currentState { get; set; }
    }
}
