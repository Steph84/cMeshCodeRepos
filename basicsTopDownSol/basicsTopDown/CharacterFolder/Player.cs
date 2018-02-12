using basicsTopDown.MapFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace basicsTopDown.CharacterFolder
{
    public class Player : CharacterObject
    {
        KeyboardState oldState = new KeyboardState();
        KeyboardState newState = new KeyboardState();
        Rectangle oldPosition = new Rectangle();

        int playerScale = 2;
        int playerSpeed = 2;

        public Player(ContentManager pContent, SpriteBatch pSpriteBatch, Rectangle pPosition, string pSpriteName, Rectangle pFrameSize, double pGameSizeCoefficient, Map pMap) : base(pContent, pSpriteBatch, pPosition, pSpriteName, pFrameSize, pGameSizeCoefficient, pMap)
        {
            #region custom player size
            int tempWidth = Size.Width * playerScale;
            int tempHeight = Size.Height * playerScale;

            Size = new Rectangle(0, 0, tempWidth, tempHeight);
            Position = new Rectangle(pPosition.X, pPosition.Y, tempWidth, tempHeight);
            #endregion

            SpeedWalking = playerSpeed * GameSizeCoefficient;
        }

        #region override Update to manage the Player control
        protected override void SpriteUpdate(GameTime pGameTime, Map pMap)
        {
            base.SpriteUpdate(pGameTime, pMap);
            
            newState = Keyboard.GetState();
            oldPosition = Position;
            DirectionBumping = EnumDirection.None;

            IsMoving = false;

            #region Manage SpriteDirection in relation to the keyboard
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
                DirectionMoving = EnumDirection.NorthEast;
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
                DirectionMoving = EnumDirection.SouthEast;
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
                DirectionMoving = EnumDirection.SouthWest;
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
                DirectionMoving = EnumDirection.NorthWest;
            }
            #endregion


            if (IsMoving == true)
            {
                List<EnumDirection> tempListDirections = CollisionCharacterOnMap(pGameTime, pMap, this);

                if(tempListDirections.Count != 0)
                {
                    if(tempListDirections.Count == 1)
                    {
                        #region If only 1 spot hit
                        // if easy direction
                        if((int)tempListDirections[0] < 10)
                        {
                            DirectionBumping = tempListDirections[0];
                        }
                        else
                        {
                            // if harder direction
                            #region Direction Bumping in relation to Direction Moving
                            // check the direction moving to conclude
                            switch (DirectionMoving)
                            {
                                case EnumDirection.North:
                                    switch (tempListDirections[0])
                                    {
                                        case EnumDirection.North:
                                            DirectionBumping = EnumDirection.North;
                                            break;
                                        case EnumDirection.NorthEast:
                                            DirectionBumping = EnumDirection.North;
                                            break;
                                        case EnumDirection.NorthWest:
                                            DirectionBumping = EnumDirection.North;
                                            break;
                                        default:
                                            DirectionBumping = EnumDirection.None;
                                            break;
                                    }
                                    break;
                                case EnumDirection.East:
                                    switch (tempListDirections[0])
                                    {
                                        case EnumDirection.East:
                                            DirectionBumping = EnumDirection.East;
                                            break;
                                        case EnumDirection.NorthEast:
                                            DirectionBumping = EnumDirection.East;
                                            break;
                                        case EnumDirection.SouthEast:
                                            DirectionBumping = EnumDirection.East;
                                            break;
                                        default:
                                            DirectionBumping = EnumDirection.None;
                                            break;
                                    }
                                    break;
                                case EnumDirection.South:
                                    switch (tempListDirections[0])
                                    {
                                        case EnumDirection.South:
                                            DirectionBumping = EnumDirection.South;
                                            break;
                                        case EnumDirection.SouthEast:
                                            DirectionBumping = EnumDirection.South;
                                            break;
                                        case EnumDirection.SouthWest:
                                            DirectionBumping = EnumDirection.South;
                                            break;
                                        default:
                                            DirectionBumping = EnumDirection.None;
                                            break;
                                    }
                                    break;
                                case EnumDirection.West:
                                    switch (tempListDirections[0])
                                    {
                                        case EnumDirection.West:
                                            DirectionBumping = EnumDirection.West;
                                            break;
                                        case EnumDirection.SouthWest:
                                            DirectionBumping = EnumDirection.West;
                                            break;
                                        case EnumDirection.NorthWest:
                                            DirectionBumping = EnumDirection.West;
                                            break;
                                        default:
                                            DirectionBumping = EnumDirection.None;
                                            break;
                                    }
                                    break;
                                case EnumDirection.NorthEast:
                                    switch (tempListDirections[0])
                                    {
                                        case EnumDirection.NorthEast:
                                            // very tricky, let's say North
                                            DirectionBumping = EnumDirection.North;
                                            break;
                                        case EnumDirection.SouthEast:
                                            DirectionBumping = EnumDirection.East;
                                            break;
                                        case EnumDirection.SouthWest:
                                            DirectionBumping = EnumDirection.None;
                                            break;
                                        case EnumDirection.NorthWest:
                                            DirectionBumping = EnumDirection.North;
                                            break;
                                        default:
                                            DirectionBumping = EnumDirection.None;
                                            break;
                                    }
                                    break;
                                case EnumDirection.SouthEast:
                                    switch (tempListDirections[0])
                                    {
                                        case EnumDirection.NorthEast:
                                            DirectionBumping = EnumDirection.East;
                                            break;
                                        case EnumDirection.SouthEast:
                                            // very tricky, let's say South
                                            DirectionBumping = EnumDirection.South;
                                            break;
                                        case EnumDirection.SouthWest:
                                            DirectionBumping = EnumDirection.South;
                                            break;
                                        case EnumDirection.NorthWest:
                                            DirectionBumping = EnumDirection.None;
                                            break;
                                        default:
                                            DirectionBumping = EnumDirection.None;
                                            break;
                                    }
                                    break;
                                case EnumDirection.SouthWest:
                                    switch (tempListDirections[0])
                                    {
                                        case EnumDirection.NorthEast:
                                            DirectionBumping = EnumDirection.None;
                                            break;
                                        case EnumDirection.SouthEast:
                                            DirectionBumping = EnumDirection.South;
                                            break;
                                        case EnumDirection.SouthWest:
                                            // very tricky, let's say South
                                            DirectionBumping = EnumDirection.South;
                                            break;
                                        case EnumDirection.NorthWest:
                                            DirectionBumping = EnumDirection.West;
                                            break;
                                        default:
                                            DirectionBumping = EnumDirection.None;
                                            break;
                                    }
                                    break;
                                case EnumDirection.NorthWest:
                                    switch (tempListDirections[0])
                                    {
                                        case EnumDirection.NorthEast:
                                            DirectionBumping = EnumDirection.North;
                                            break;
                                        case EnumDirection.SouthEast:
                                            DirectionBumping = EnumDirection.None;
                                            break;
                                        case EnumDirection.SouthWest:
                                            DirectionBumping = EnumDirection.West;
                                            break;
                                        case EnumDirection.NorthWest:
                                            // very tricky, let's say North
                                            DirectionBumping = EnumDirection.North;
                                            break;
                                        default:
                                            DirectionBumping = EnumDirection.None;
                                            break;
                                    }
                                    break;
                                default:
                                    DirectionBumping = EnumDirection.None;
                                    break;
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        #region If more than 1 spot hit

                        #endregion
                    }
                }

                //Position = new Rectangle(oldPosition.X, oldPosition.Y, Position.Width, Position.Height);
                
                switch (DirectionBumping)
                {
                    case EnumDirection.North:
                        Position = new Rectangle(Position.X, oldPosition.Y, Position.Width, Position.Height);
                        break;
                    case EnumDirection.East:
                        Position = new Rectangle(oldPosition.X, Position.Y, Position.Width, Position.Height);
                        break;
                    case EnumDirection.South:
                        Position = new Rectangle(Position.X, oldPosition.Y, Position.Width, Position.Height);
                        break;
                    case EnumDirection.West:
                        Position = new Rectangle(oldPosition.X, Position.Y, Position.Width, Position.Height);
                        break;
                    case EnumDirection.None:
                        // nothing to do
                        break;
                    default:
                        break;
                }
            }
            oldState = newState;
        }
        #endregion

        private bool KeyWentDown(Keys pKey)
        {
            return newState.IsKeyDown(pKey) && oldState.IsKeyDown(pKey);
        }
    }
}
