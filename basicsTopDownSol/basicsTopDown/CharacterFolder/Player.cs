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
        Rectangle oldPosition = new Rectangle();

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
        protected override void SpriteUpdate(GameTime pGameTime, Map pMap)
        {
            newState = Keyboard.GetState();
            oldPosition = Position;
            DirectionBumping = EnumDirection.None;

            IsMoving = false;

            #region Manage SpriteDirection in relation to the keyboard : 8 Directions
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
            #endregion
            
            base.SpriteUpdate(pGameTime, pMap);

            if (IsMoving == true)
            {
                CollisionCharacterOnMap(pGameTime, pMap, this);

                if(DirectionBumping != EnumDirection.None)
                {
                    #region Check the number of flags hit
                    var numberOfFlags = 0;
                    var checkTypeValues = Enum.GetValues(typeof(EnumDirection));
                    foreach (EnumDirection value in checkTypeValues)
                    {
                        if(DirectionBumping.HasFlag(value))
                        {
                            numberOfFlags++;
                        }
                        //var item0 = DirectionBumping & value;
                        //var item1 = (DirectionBumping & value) == value;
                        //var item2 = (DirectionBumping & value) > 0;
                    }
                    #endregion

                    // if 1 flag hit, have to check the diagonal on the same side to fix the stuff

                    #region Precise the DirectionBumping depending on the flags number
                    switch (numberOfFlags)
                    {
                        #region CASE 1 flag hit
                        case 1:
                            // 5 possible directions
                            switch (DirectionBumping)
                            {
                                case EnumDirection.North:
                                    switch (DirectionMoving)
                                    {
                                        case EnumDirection.North:
                                            // nothing to do
                                            break;
                                        case EnumDirection.East:
                                            DirectionBumping = EnumDirection.East;
                                            break;
                                        case EnumDirection.West:
                                            DirectionBumping = EnumDirection.West;
                                            break;
                                        case EnumDirection.North | EnumDirection.East:
                                            // ambiguity
                                            break;
                                        case EnumDirection.North | EnumDirection.West:
                                            // ambiguity
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                                case EnumDirection.East:
                                    switch (DirectionMoving)
                                    {
                                        case EnumDirection.North:
                                            DirectionBumping = EnumDirection.North;
                                            break;
                                        case EnumDirection.East:
                                            // nothing to do
                                            break;
                                        case EnumDirection.South:
                                            DirectionBumping = EnumDirection.South;
                                            break;
                                        case EnumDirection.NorthEast:
                                            // ambiguity
                                            break;
                                        case EnumDirection.SouthEast:
                                            // ambiguity
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                                case EnumDirection.South:
                                    switch (DirectionMoving)
                                    {
                                        case EnumDirection.East:
                                            DirectionBumping = EnumDirection.East;
                                            break;
                                        case EnumDirection.South:
                                            // nothing to do
                                            break;
                                        case EnumDirection.West:
                                            DirectionBumping = EnumDirection.West;
                                            break;
                                        case EnumDirection.SouthEast:
                                            // ambiguity
                                            break;
                                        case EnumDirection.SouthWest:
                                            // ambiguity
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                                case EnumDirection.West:
                                    switch (DirectionMoving)
                                    {
                                        case EnumDirection.North:
                                            DirectionBumping = EnumDirection.North;
                                            break;
                                        case EnumDirection.South:
                                            DirectionBumping = EnumDirection.South;
                                            break;
                                        case EnumDirection.West:
                                            // nothing to do
                                            break;
                                        case EnumDirection.SouthWest:
                                            // ambiguity
                                            break;
                                        case EnumDirection.NorthWest:
                                            // ambiguity
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        #endregion

                        #region CASE 2 falgs hit
                        case 2:
                            // 2 or 3 possible directions
                            switch (DirectionMoving)
                            {
                                case EnumDirection.North:
                                    DirectionBumping = EnumDirection.North;
                                    break;
                                case EnumDirection.East:
                                    DirectionBumping = EnumDirection.East;
                                    break;
                                case EnumDirection.South:
                                    DirectionBumping = EnumDirection.South;
                                    break;
                                case EnumDirection.West:
                                    DirectionBumping = EnumDirection.West;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        #endregion

                        #region CASE 3 flags hit
                        case 3:
                            // 1 possible direction
                            switch (DirectionMoving)
                            {
                                case EnumDirection.North:
                                    DirectionBumping = EnumDirection.North;
                                    break;
                                case EnumDirection.East:
                                    DirectionBumping = EnumDirection.East;
                                    break;
                                case EnumDirection.South:
                                    DirectionBumping = EnumDirection.South;
                                    break;
                                case EnumDirection.West:
                                    DirectionBumping = EnumDirection.West;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        #endregion

                        #region CASE 4 flags hit
                        case 4:
                            throw new System.Exception("Collision error !!");
                        default:
                            break;
                        #endregion
                    }
                    #endregion
                    
                    #region Manage the coordinate depending on the DirectionBumping
                    switch (DirectionBumping)
                    {
                        case EnumDirection.North:
                            switch (DirectionMoving)
                            {
                                case EnumDirection.North:
                                    BackToOldAlongY(oldPosition);
                                    break;
                                case EnumDirection.East:
                                    BackToOldAlongY(oldPosition);
                                    break;
                                case EnumDirection.West:
                                    BackToOldAlongY(oldPosition);
                                    break;
                                case EnumDirection.NorthEast:
                                    BackToOldAlongY(oldPosition);
                                    break;
                                case EnumDirection.NorthWest:
                                    BackToOldAlongY(oldPosition);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case EnumDirection.East:
                            switch (DirectionMoving)
                            {
                                case EnumDirection.North:
                                    BackToOldAlongX(oldPosition);
                                    break;
                                case EnumDirection.East:
                                    BackToOldAlongX(oldPosition);
                                    break;
                                case EnumDirection.South:
                                    BackToOldAlongX(oldPosition);
                                    break;
                                case EnumDirection.NorthEast:
                                    BackToOldAlongX(oldPosition);
                                    break;
                                case EnumDirection.SouthEast:
                                    BackToOldAlongX(oldPosition);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case EnumDirection.South:
                            switch (DirectionMoving)
                            {
                                case EnumDirection.East:
                                    BackToOldAlongY(oldPosition);
                                    break;
                                case EnumDirection.South:
                                    BackToOldAlongY(oldPosition);
                                    break;
                                case EnumDirection.West:
                                    BackToOldAlongY(oldPosition);
                                    break;
                                case EnumDirection.SouthEast:
                                    BackToOldAlongY(oldPosition);
                                    break;
                                case EnumDirection.SouthWest:
                                    BackToOldAlongY(oldPosition);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case EnumDirection.West:
                            switch (DirectionMoving)
                            {
                                case EnumDirection.North:
                                    BackToOldAlongX(oldPosition);
                                    break;
                                case EnumDirection.South:
                                    BackToOldAlongX(oldPosition);
                                    break;
                                case EnumDirection.West:
                                    BackToOldAlongX(oldPosition);
                                    break;
                                case EnumDirection.SouthWest:
                                    BackToOldAlongX(oldPosition);
                                    break;
                                case EnumDirection.NorthWest:
                                    BackToOldAlongX(oldPosition);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case EnumDirection.None:
                            // nothing to do
                            break;
                        default:
                            break;
                    }
                    #endregion
                }
            }
            oldState = newState;
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
