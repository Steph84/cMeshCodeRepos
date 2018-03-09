using blockAStarAlgo.AlgoFolder;
using blockAStarAlgo.MapFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace blockAStarAlgo.CharacterFolder
{
    public class Robot : CharacterObject
    {
        double robotScale = 1.8d;
        int robotSpeed = 2;

        public bool TargetLocked { get; set; }
        public List<int> Path { get; set; }

        public Robot(ContentManager pContent, SpriteBatch pSpriteBatch, Rectangle pPosition, string pSpriteName, Rectangle pFrameSize, double pGameSizeCoefficient, Map pMap) : base(pContent, pSpriteBatch, pPosition, pSpriteName, pFrameSize, pGameSizeCoefficient, pMap)
        {
            #region custom robot size
            int tempWidth = (int)(Size.Width * robotScale);
            int tempHeight = (int)(Size.Height * robotScale);

            Size = new Rectangle(0, 0, tempWidth, tempHeight);
            Position = new Rectangle(pPosition.X, pPosition.Y, tempWidth, tempHeight);
            #endregion

            // custom robot speed walk
            SpeedMove = robotSpeed * GameSizeCoefficient;

            TargetLocked = false;
            Path = null;
        }

        #region override Update to manage the Player control
        public override void SpriteUpdate(GameTime pGameTime, Map pMap)
        {
            // save the OldPosition before moving
            OldPosition = new Rectangle(Position.X, Position.Y, Position.Width, Position.Height);
            OldNSPointsInPixel = NSPointsInPixel;

            #region Manage SpriteDirection in relation to the keyboard : 8 Directions

            IsMoving = false;

            // algo for movement
            DirectionMoving = EnumDirection.West;
            // call method to A Star with arguments like
            // map, target, mapUnveiled, mapVisible in relation to the walls

            // if target, AStar
            if(TargetLocked)
            {
                if (Path == null)
                {
                    Path = PathFinding.AStar(pMap);
                }
            }
            else
            {
                PathFinding.Explore(pMap, this);
            }

            #endregion

            // Call the CharacterUpdate
            base.SpriteUpdate(pGameTime, pMap);
        }
        #endregion
    }
}
