using blockAStarAlgo.MapFolder;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockAStarAlgo.AlgoFolder
{
    public class PathFinding
    {
        public static List<int> AStar(Map pMap)
        {
            List<int> listToReturn = new List<int>();

            // return path, ie ordered list of tile ids
            return listToReturn;
        }

        public static void Explore(Map pMap, CharacterFolder.CharacterObject pCharacter)
        {
            pCharacter.DirectionMoving = EnumDirection.North;
            pCharacter.SpeedMove = 1;

            switch (pCharacter.DirectionMoving)
            {
                case EnumDirection.North:
                    pCharacter.IsMoving = true;
                    pCharacter.Position = new Rectangle(pCharacter.Position.X, pCharacter.Position.Y - (int)pCharacter.SpeedMove, pCharacter.Position.Width, pCharacter.Position.Height);
                    break;
                case EnumDirection.East:
                    break;
                case EnumDirection.South:
                    break;
                case EnumDirection.West:
                    break;
                case EnumDirection.None:
                    break;
                default:
                    break;
            }
        }
    }
}
