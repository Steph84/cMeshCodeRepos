using basicsTopDown.MapFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace basicsTopDown.CharacterFolder
{
    public class Player : CharacterObject
    {
        KeyboardState oldState = new KeyboardState();
        KeyboardState newState = new KeyboardState();

        double playerScale = 1.5d;
        int playerSpeed = 2;

        public Player(ContentManager pContent, SpriteBatch pSpriteBatch, Rectangle pPosition, string pSpriteName, Rectangle pFrameSize, double pGameSizeCoefficient, Map pMap) : base(pContent, pSpriteBatch, pPosition, pSpriteName, pFrameSize, pGameSizeCoefficient, pMap)
        {
            #region custom player size
            int tempWidth = (int)(Size.Width * playerScale);
            int tempHeight = (int)(Size.Height * playerScale);

            Size = new Rectangle(0, 0, tempWidth, tempHeight);
            Position = new Rectangle(pPosition.X, pPosition.Y, tempWidth, tempHeight);
            #endregion

            // custom player speed walk
            SpeedWalking = playerSpeed * GameSizeCoefficient;
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

            // North
            if (KeyWentDown(Keys.Up) && !KeyWentDown(Keys.Right) && !KeyWentDown(Keys.Down) && !KeyWentDown(Keys.Left))
            {
                IsMoving = true;
                Position = new Rectangle(Position.X, Position.Y - (int)SpeedWalking, Position.Width, Position.Height);
                DirectionMoving = EnumDirection.North;
            } // North East
            else if (KeyWentDown(Keys.Up) && KeyWentDown(Keys.Right) && !KeyWentDown(Keys.Down) && !KeyWentDown(Keys.Left))
            {
                IsMoving = true;
                Position = new Rectangle(Position.X + (int)SpeedWalking, Position.Y - (int)SpeedWalking, Position.Width, Position.Height);
                DirectionMoving = EnumDirection.North | EnumDirection.East;
            } // East
            else if (!KeyWentDown(Keys.Up) && KeyWentDown(Keys.Right) && !KeyWentDown(Keys.Down) && !KeyWentDown(Keys.Left))
            {
                IsMoving = true;
                Position = new Rectangle(Position.X + (int)SpeedWalking, Position.Y, Position.Width, Position.Height);
                DirectionMoving = EnumDirection.East;
            } // South East
            else if (!KeyWentDown(Keys.Up) && KeyWentDown(Keys.Right) && KeyWentDown(Keys.Down) && !KeyWentDown(Keys.Left))
            {
                IsMoving = true;
                Position = new Rectangle(Position.X + (int)SpeedWalking, Position.Y + (int)SpeedWalking, Position.Width, Position.Height);
                DirectionMoving = EnumDirection.South | EnumDirection.East;
            } // South
            else if (!KeyWentDown(Keys.Up) && !KeyWentDown(Keys.Right) && KeyWentDown(Keys.Down) && !KeyWentDown(Keys.Left))
            {
                IsMoving = true;
                Position = new Rectangle(Position.X, Position.Y + (int)SpeedWalking, Position.Width, Position.Height);
                DirectionMoving = EnumDirection.South;
            } // South West
            else if (!KeyWentDown(Keys.Up) && !KeyWentDown(Keys.Right) && KeyWentDown(Keys.Down) && KeyWentDown(Keys.Left))
            {
                IsMoving = true;
                Position = new Rectangle(Position.X - (int)SpeedWalking, Position.Y + (int)SpeedWalking, Position.Width, Position.Height);
                DirectionMoving = EnumDirection.South | EnumDirection.West;
            } // West
            else if (!KeyWentDown(Keys.Up) && !KeyWentDown(Keys.Right) && !KeyWentDown(Keys.Down) && KeyWentDown(Keys.Left))
            {
                IsMoving = true;
                Position = new Rectangle(Position.X - (int)SpeedWalking, Position.Y, Position.Width, Position.Height);
                DirectionMoving = EnumDirection.West;
            } // North West
            else if (KeyWentDown(Keys.Up) && !KeyWentDown(Keys.Right) && !KeyWentDown(Keys.Down) && KeyWentDown(Keys.Left))
            {
                IsMoving = true;
                Position = new Rectangle(Position.X - (int)SpeedWalking, Position.Y - (int)SpeedWalking, Position.Width, Position.Height);
                DirectionMoving = EnumDirection.North | EnumDirection.West;
            }

            oldState = newState;

            #endregion

            // Call the CharacterUpdate
            base.SpriteUpdate(pGameTime, pMap);
        }
        #endregion

        #region Method to manage down key
        private bool KeyWentDown(Keys pKey)
        {
            return newState.IsKeyDown(pKey) && oldState.IsKeyDown(pKey);
        }
        #endregion
    }
}
