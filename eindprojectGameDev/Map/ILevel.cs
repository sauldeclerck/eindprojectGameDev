using eindprojectGameDev.Characters.Enemies;
using eindprojectGameDev.Characters.Player;
using System.Collections.Generic;

namespace eindprojectGameDev.Map
{
    public interface ILevel
    {
        List<NPC> Enemies { get; set; }
        Hero Hero { get; set; }
        Block[,] BlockArray { get; set; }
    }
}
