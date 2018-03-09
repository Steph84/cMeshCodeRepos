using blockAStarAlgo.MapFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace blockAStarAlgo.CharacterFolder
{
    public class Robot : CharacterObject
    {
        KeyboardState oldState = new KeyboardState();
        KeyboardState newState = new KeyboardState();

        double robotScale = 1.5d;
        int robotSpeed = 2;

        public Robot(ContentManager pContent, SpriteBatch pSpriteBatch, Rectangle pPosition, string pSpriteName, Rectangle pFrameSize, double pGameSizeCoefficient, Map pMap) : base(pContent, pSpriteBatch, pPosition, pSpriteName, pFrameSize, pGameSizeCoefficient, pMap)
        {
            #region custom robot size
            int tempWidth = (int)(Size.Width * robotScale);
            int tempHeight = (int)(Size.Height * robotScale);

            Size = new Rectangle(0, 0, tempWidth, tempHeight);
            Position = new Rectangle(pPosition.X, pPosition.Y, tempWidth, tempHeight);
            #endregion

            // custom robot speed walk
            SpeedWalking = robotSpeed * GameSizeCoefficient;
        }

        #region override Update to manage the Player control
        public override void SpriteUpdate(GameTime pGameTime, Map pMap)
        {
            // save the OldPosition before moving
            OldPosition = new Rectangle(Position.X, Position.Y, Position.Width, Position.Height);
            OldNSPointsInPixel = NSPointsInPixel;

            #region Manage SpriteDirection in relation to the keyboard : 8 Directions
            newState = Keyboard.GetState();

            IsMoving = false;
            
            // algo for movement

            oldState = newState;

            #endregion

            // Call the CharacterUpdate
            base.SpriteUpdate(pGameTime, pMap);
        }
        #endregion
    }
}
