using basicsTopDown.MapFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace basicsTopDown
{
    [Flags]
    public enum EnumDirection
    {
        North = 1,
        East = 2,
        South = 4,
        West = 8,
        NorthEast = 16,
        SouthEast = 32,
        SouthWest = 64,
        NorthWest = 128,
        None = 256
    }

    public class SpriteObject
    {
        public Texture2D SpriteData { get; set; }
        public Rectangle Position { get; set; }
        public NineSlicePoints NSPointsInPixel { get; set; }
        public NineSlicePoints NSPointsInCoordinate { get; set; }
        public EnumDirection DirectionMoving { get; set; }
        public EnumDirection DirectionBumping { get; set; }

        protected bool IsMoving { get; set; }
        protected double GameSizeCoefficient { get; set; }
        protected Rectangle Size { get; set; }
        protected ContentManager Content { get; set; }
        protected SpriteBatch SpriteBatch { get; set; }
        protected Map Map { get; set; }
        protected Rectangle OldPosition = new Rectangle();

        public class NineSlicePoints
        {
            //public Vector2 North { get; set; }
            //public Vector2 East { get; set; }
            //public Vector2 South { get; set; }
            //public Vector2 West { get; set; }
            public Vector2 Center { get; set; }
            public Vector2 NorthEast { get; set; }
            public Vector2 SouthEast { get; set; }
            public Vector2 SouthWest { get; set; }
            public Vector2 NorthWest { get; set; }
        }

        public SpriteObject(ContentManager pContent, SpriteBatch pSpriteBatch, Rectangle pPosition, string pSpriteName, double pGameSizeCoefficient, Map pMap)
        {
            Content = pContent;
            SpriteBatch = pSpriteBatch;
            IsMoving = false;
            GameSizeCoefficient = pGameSizeCoefficient;
            Map = pMap;
        }

        #region SpriteUpdate
        public virtual void SpriteUpdate(GameTime pGameTime, Map pMap)
        {
            #region Manage collision Sprite on Map
            if (IsMoving == true)
            {
                DirectionBumping = EnumDirection.None;
                CalculateSpriteCoordinates(pMap);

                if (CollisionSpriteOnMap(pGameTime, pMap, this) != null)
                {
                    Position = OldPosition;
                }
            }
            #endregion
        }
        #endregion

        public virtual void SpriteDraw(GameTime pGameTime) { }

        #region Method to check if a sprite collide with the map basic
        public static TileObject CollisionSpriteOnMap(GameTime pGameTime, Map pMap, SpriteObject pSprite)
        {
            TileObject tile = null;

            for (int row = 0; row < pMap.MapSizeInTile.Height; row++)
            {
                for (int column = 0; column < pMap.MapSizeInTile.Width; column++)
                {
                    if (pMap.MapGrid[row, column].Texture == Map.MapTexture.Wall)
                    {
                        bool collide = false;
                        collide = CollisionObject.CheckCollision(pMap.MapGrid[row, column].Position, pSprite.Position);
                        if (collide == true)
                        {
                            tile = pMap.MapGrid[row, column];
                        }
                    }
                }
            }
            return tile;
        }
        #endregion
        
        #region Method to calculate Charcater positions (9 slices) in pixel and in coordinates
        protected void CalculateSpriteCoordinates(Map pMap)
        {
            #region 9 slices points position of the sprite in pixel
            NSPointsInPixel = new NineSlicePoints
            {
                //North = new Vector2(Position.X + Size.Width / 2, Position.Y),
                //East = new Vector2(Position.X + Size.Width, Position.Y + Size.Height / 2),
                //South = new Vector2(Position.X + Size.Width / 2, Position.Y + Size.Height),
                //West = new Vector2(Position.X, Position.Y + Size.Height / 2),
                Center = new Vector2(Position.X + Size.Width / 2, Position.Y + Size.Height / 2),
                NorthEast = new Vector2(Position.X + Size.Width, Position.Y),
                SouthEast = new Vector2(Position.X + Size.Width, Position.Y + Size.Height),
                SouthWest = new Vector2(Position.X, Position.Y + Size.Height),
                NorthWest = new Vector2(Position.X, Position.Y)
            };
            #endregion

            #region 9 slices points position of the sprite in coordinate
            NSPointsInCoordinate = new NineSlicePoints
            {
                //North = new Vector2((float)Math.Floor(NSPointsInPixel.North.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.North.Y / pMap.TileSizeShowing.Height)),
                //East = new Vector2((float)Math.Floor(NSPointsInPixel.East.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.East.Y / pMap.TileSizeShowing.Height)),
                //South = new Vector2((float)Math.Floor(NSPointsInPixel.South.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.South.Y / pMap.TileSizeShowing.Height)),
                //West = new Vector2((float)Math.Floor(NSPointsInPixel.West.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.West.Y / pMap.TileSizeShowing.Height)),
                Center = new Vector2((float)Math.Floor(NSPointsInPixel.Center.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.Center.Y / pMap.TileSizeShowing.Height)),
                NorthEast = new Vector2((float)Math.Floor(NSPointsInPixel.NorthEast.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.NorthEast.Y / pMap.TileSizeShowing.Height)),
                SouthEast = new Vector2((float)Math.Floor(NSPointsInPixel.SouthEast.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.SouthEast.Y / pMap.TileSizeShowing.Height)),
                SouthWest = new Vector2((float)Math.Floor(NSPointsInPixel.SouthWest.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.SouthWest.Y / pMap.TileSizeShowing.Height)),
                NorthWest = new Vector2((float)Math.Floor(NSPointsInPixel.NorthWest.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.NorthWest.Y / pMap.TileSizeShowing.Height))
            };
            #endregion
        }
        #endregion
    }
}
