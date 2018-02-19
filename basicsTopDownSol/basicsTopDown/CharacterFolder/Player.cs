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

        int playerScale = 2;
        int playerSpeed = 2;

        public int DirectionNumber { get; set; }

        public Player(ContentManager pContent, SpriteBatch pSpriteBatch, Rectangle pPosition, string pSpriteName, Rectangle pFrameSize, double pGameSizeCoefficient, Map pMap, int pNumDir) : base(pContent, pSpriteBatch, pPosition, pSpriteName, pFrameSize, pGameSizeCoefficient, pMap)
        {
            #region custom player size
            int tempWidth = Size.Width * playerScale;
            int tempHeight = Size.Height * playerScale;

            Size = new Rectangle(0, 0, tempWidth, tempHeight);
            Position = new Rectangle(pPosition.X, pPosition.Y, tempWidth, tempHeight);
            #endregion

            SpeedWalking = playerSpeed * GameSizeCoefficient;

            DirectionNumber = pNumDir;
        }

        #region override Update to manage the Player control
        public override void SpriteUpdate(GameTime pGameTime, Map pMap)
        {
            // save the OldPosition before moving
            OldPosition = new Rectangle(Position.X, Position.Y, Position.Width, Position.Height);
            //Console.WriteLine("Old1" + OldPosition);
            OldNSPointsInPixel = NSPointsInPixel;

            #region Manage SpriteDirection in relation to the keyboard : 8 Directions
            newState = Keyboard.GetState();

            IsMoving = false;

            if (KeyWentDown(Keys.Up) && !KeyWentDown(Keys.Right) && !KeyWentDown(Keys.Down) && !KeyWentDown(Keys.Left))
            {
                IsMoving = true;
                Position = new Rectangle(Position.X, Position.Y - (int)SpeedWalking, Position.Width, Position.Height);
                DirectionMoving = EnumDirection.North;
            }
            else if (KeyWentDown(Keys.Up) && KeyWentDown(Keys.Right) && !KeyWentDown(Keys.Down) && !KeyWentDown(Keys.Left))
            {
                IsMoving = true;
                Position = new Rectangle(Position.X + (int)SpeedWalking, Position.Y - (int)SpeedWalking, Position.Width, Position.Height);
                DirectionMoving = EnumDirection.North | EnumDirection.East;
            }
            else if (!KeyWentDown(Keys.Up) && KeyWentDown(Keys.Right) && !KeyWentDown(Keys.Down) && !KeyWentDown(Keys.Left))
            {
                IsMoving = true;
                Position = new Rectangle(Position.X + (int)SpeedWalking, Position.Y, Position.Width, Position.Height);
                DirectionMoving = EnumDirection.East;
            }
            else if (!KeyWentDown(Keys.Up) && KeyWentDown(Keys.Right) && KeyWentDown(Keys.Down) && !KeyWentDown(Keys.Left))
            {
                IsMoving = true;
                Position = new Rectangle(Position.X + (int)SpeedWalking, Position.Y + (int)SpeedWalking, Position.Width, Position.Height);
                DirectionMoving = EnumDirection.South | EnumDirection.East;
            }
            else if (!KeyWentDown(Keys.Up) && !KeyWentDown(Keys.Right) && KeyWentDown(Keys.Down) && !KeyWentDown(Keys.Left))
            {
                IsMoving = true;
                Position = new Rectangle(Position.X, Position.Y + (int)SpeedWalking, Position.Width, Position.Height);
                DirectionMoving = EnumDirection.South;
            }
            else if (!KeyWentDown(Keys.Up) && !KeyWentDown(Keys.Right) && KeyWentDown(Keys.Down) && KeyWentDown(Keys.Left))
            {
                IsMoving = true;
                Position = new Rectangle(Position.X - (int)SpeedWalking, Position.Y + (int)SpeedWalking, Position.Width, Position.Height);
                DirectionMoving = EnumDirection.South | EnumDirection.West;
            }
            else if (!KeyWentDown(Keys.Up) && !KeyWentDown(Keys.Right) && !KeyWentDown(Keys.Down) && KeyWentDown(Keys.Left))
            {
                IsMoving = true;
                Position = new Rectangle(Position.X - (int)SpeedWalking, Position.Y, Position.Width, Position.Height);
                DirectionMoving = EnumDirection.West;
            }
            else if (KeyWentDown(Keys.Up) && !KeyWentDown(Keys.Right) && !KeyWentDown(Keys.Down) && KeyWentDown(Keys.Left))
            {
                IsMoving = true;
                Position = new Rectangle(Position.X - (int)SpeedWalking, Position.Y - (int)SpeedWalking, Position.Width, Position.Height);
                DirectionMoving = EnumDirection.North | EnumDirection.West;
            }

            oldState = newState;

            #endregion

            //Console.WriteLine("Pos" + Position);

            //Console.WriteLine("Old2" + OldPosition);
            // Call the CharacterUpdate
            base.SpriteUpdate(pGameTime, pMap);
        }
        #endregion

        private bool KeyWentDown(Keys pKey)
        {
            return newState.IsKeyDown(pKey) && oldState.IsKeyDown(pKey);
        }

        private void BackToOldAlongY(Rectangle oldPosition)
        {
            Position = new Rectangle(Position.X, oldPosition.Y, Position.Width, Position.Height);
        }

        private void BackToOldAlongX(Rectangle oldPosition)
        {
            Position = new Rectangle(oldPosition.X, Position.Y, Position.Width, Position.Height);
        }
    }
}
