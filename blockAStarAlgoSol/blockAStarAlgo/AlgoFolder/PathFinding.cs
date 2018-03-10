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
            //pCharacter.DirectionMoving = EnumDirection.North;
            pCharacter.SpeedMove = 2;

            switch (pCharacter.DirectionMoving)
            {
                case EnumDirection.North:
                    pCharacter.IsMoving = true;
                    pCharacter.Position = new Rectangle(pCharacter.Position.X, pCharacter.Position.Y - (int)pCharacter.SpeedMove, pCharacter.Position.Width, pCharacter.Position.Height);
                    break;
                case EnumDirection.East:
                    pCharacter.IsMoving = true;
                    pCharacter.Position = new Rectangle(pCharacter.Position.X + (int)pCharacter.SpeedMove, pCharacter.Position.Y, pCharacter.Position.Width, pCharacter.Position.Height);
                    break;
                case EnumDirection.South:
                    pCharacter.IsMoving = true;
                    pCharacter.Position = new Rectangle(pCharacter.Position.X, pCharacter.Position.Y + (int)pCharacter.SpeedMove, pCharacter.Position.Width, pCharacter.Position.Height);
                    break;
                case EnumDirection.West:
                    pCharacter.IsMoving = true;
                    pCharacter.Position = new Rectangle(pCharacter.Position.X - (int)pCharacter.SpeedMove, pCharacter.Position.Y, pCharacter.Position.Width, pCharacter.Position.Height);
                    break;
                default:
                    break;
            }

            if(pCharacter.IsCollidingWall)
            {
                var rand = new Random();
                EnumDirection[] allValues = (EnumDirection[])Enum.GetValues(typeof(EnumDirection));
                
                //allValues = allValues - pCharacter.DirectionMoving;
                EnumDirection value = allValues[rand.Next(allValues.Length)];
                pCharacter.DirectionMoving = value;
                pCharacter.IsCollidingWall = false;
            }
        }
    }
}
