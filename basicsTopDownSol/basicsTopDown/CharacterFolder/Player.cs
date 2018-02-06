using basicsTopDown.MapFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace basicsTopDown.CharacterFolder
{
    public class Player : CharacterObject
    {
        KeyboardState oldState = new KeyboardState();
        KeyboardState newState = new KeyboardState();

        int playerScale = 2;
        int playerSpeed = 2;

        public Player(ContentManager pContent, SpriteBatch pSpriteBatch, Rectangle pPosition, string pSpriteName, Rectangle pFrameSize, double pGameSizeCoefficient) : base(pContent, pSpriteBatch, pPosition, pSpriteName, pFrameSize, pGameSizeCoefficient)
        {
            #region custom player size
            int tempWidth = SpriteSizeShowing.Width * playerScale;
            int tempHeight = SpriteSizeShowing.Height * playerScale;

            Size = new Rectangle(0, 0, tempWidth, tempHeight);
            Position = new Rectangle(pPosition.X, pPosition.Y, tempWidth, tempHeight);
            #endregion

            SpeedWalking = playerSpeed * GameSizeCoefficient;
        }

        #region override Update to manage the Player control
        public override void SpriteUpdate(GameTime pGameTime, MapFolder.Map pMap)
        {
            base.SpriteUpdate(pGameTime, pMap);
            
            newState = Keyboard.GetState();
            Rectangle oldPosition = Position;

            IsMoving = false;

            if (newState.IsKeyDown(Keys.Up) && oldState.IsKeyDown(Keys.Up))
            {
                IsMoving = true;
                Position = new Rectangle(Position.X, Position.Y - (int)SpeedWalking, Position.Width, Position.Height);
                DirectionMoving = EnumDirection.North;
            }

            if (newState.IsKeyDown(Keys.Right) && oldState.IsKeyDown(Keys.Right))
            {
                IsMoving = true;
                Position = new Rectangle(Position.X + (int)SpeedWalking, Position.Y, Position.Width, Position.Height);
                DirectionMoving = EnumDirection.East;
            }

            if (newState.IsKeyDown(Keys.Down) && oldState.IsKeyDown(Keys.Down))
            {
                IsMoving = true;
                Position = new Rectangle(Position.X, Position.Y + (int)SpeedWalking, Position.Width, Position.Height);
                DirectionMoving = EnumDirection.South;
            }

            if (newState.IsKeyDown(Keys.Left) && oldState.IsKeyDown(Keys.Left))
            {
                IsMoving = true;
                Position = new Rectangle(Position.X - (int)SpeedWalking, Position.Y, Position.Width, Position.Height);
                DirectionMoving = EnumDirection.West;
            }

            if (IsMoving == true && CollisionSpriteOnMap(pGameTime, pMap, this) != null)
            {
                TileObject tileWall = CollisionSpriteOnMap(pGameTime, pMap, this);
        
                if (tileWall != null)
                {
                    DirectionBumping = EnumDirection.None;

                    if(tileWall.Position.X < Position.X && tileWall.Position.Y < Position.Y)
                    {
                        DirectionBumping = EnumDirection.North;
                    }

                    if (tileWall.Position.X > Position.X && tileWall.Position.Y < Position.Y)
                    {
                        DirectionBumping = EnumDirection.East;
                    }

                    if (tileWall.Position.X > Position.X && tileWall.Position.Y > Position.Y)
                    {
                        DirectionBumping = EnumDirection.South;
                    }

                    if (tileWall.Position.X < Position.X && tileWall.Position.Y > Position.Y)
                    {
                        DirectionBumping = EnumDirection.West;
                    }

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

            }

            oldState = newState;
        }
        #endregion
    }
}
