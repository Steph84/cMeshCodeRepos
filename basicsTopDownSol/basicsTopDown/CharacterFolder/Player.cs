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
            Rectangle oldPosition = Position;

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
                        if((int)tempListDirections[0] < 10)
                        {
                            DirectionBumping = tempListDirections[0];
                        }
                        else
                        {
                            // check the direction moving to conclude
                        }
                    }
                    else
                    {

                    }
                    
                    // manage easy directions



                // manage harder directions
                }

                //Position = new Rectangle(oldPosition.X, oldPosition.Y, Position.Width, Position.Height);

                





                //if (tileWall != null)
                //{
                //    DirectionBumping = EnumDirection.None;

                //    if(tileWall.Position.X < Position.X && tileWall.Position.Y < Position.Y)
                //    {
                //        DirectionBumping = EnumDirection.North;
                //    }

                //    if (tileWall.Position.X > Position.X && tileWall.Position.Y < Position.Y)
                //    {
                //        DirectionBumping = EnumDirection.East;
                //    }

                //    if (tileWall.Position.X > Position.X && tileWall.Position.Y > Position.Y)
                //    {
                //        DirectionBumping = EnumDirection.South;
                //    }

                //    if (tileWall.Position.X < Position.X && tileWall.Position.Y > Position.Y)
                //    {
                //        DirectionBumping = EnumDirection.West;
                //    }

                //switch (DirectionBumping)
                //{
                //    case EnumDirection.North:
                //        Position = new Rectangle(Position.X, oldPosition.Y, Position.Width, Position.Height);
                //        break;
                //    case EnumDirection.East:
                //        Position = new Rectangle(oldPosition.X, Position.Y, Position.Width, Position.Height);
                //        break;
                //    case EnumDirection.South:
                //        Position = new Rectangle(Position.X, oldPosition.Y, Position.Width, Position.Height);
                //        break;
                //    case EnumDirection.West:
                //        Position = new Rectangle(oldPosition.X, Position.Y, Position.Width, Position.Height);
                //        break;
                //    case EnumDirection.None:
                //        // nothing to do
                //        break;
                //    default:
                //        break;
                //}
                //}

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
