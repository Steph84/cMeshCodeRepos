using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace basicsTopDown.SpriteFolder
{
    [Flags]
    public enum EnumDirection
    {
        North = 1,
        East = 2,
        South = 4,
        West = 8,
        None = 16
    }

    public class SpriteObject
    {
        public Texture2D SpriteData { get; set; }
        public Rectangle Position { get; set; }
        public EnumDirection DirectionMoving { get; set; }

        protected bool IsMoving { get; set; }
        protected double GameSizeCoefficient { get; set; }
        protected Rectangle Size { get; set; }
        protected ContentManager Content { get; set; }
        protected SpriteBatch SpriteBatch { get; set; }
        protected Map Map { get; set; }
        protected Rectangle OldPosition { get; set; }
        protected NineSlicePoints NSPointsInPixel { get; set; }
        protected NineSlicePoints OldNSPointsInPixel { get; set; }

        private NineSlicePoints NSPointsInCoordinate { get; set; }

        List<MapTexture> TextureListToCheck = new List<MapTexture>();
        
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

            TextureListToCheck.Add(MapTexture.Wall);
        }

        #region SpriteUpdate
        public virtual void SpriteUpdate(GameTime pGameTime, Map pMap)
        {
            #region Manage collision Sprite on Map
            // optimisation if the sprite is moving
            // so if it is not a tile sprite
            if (IsMoving == true)
            {
                // calculate the NineSlice Coordinates
                CalculateSpriteCoordinates(pMap);
                
                // check the collision of the sprite on the map
                TileObject tileCollided = CollisionSpriteOnMap(pGameTime, pMap, Position, TextureListToCheck);
                if (tileCollided != null)
                {
                    // if DirectionMoving has more than 1 flag : diagonale movement
                    if ((DirectionMoving & (DirectionMoving - 1)) != 0)
                    {
                        // check if the sprite can move along 1 of the 2 directions

                        // check the movement along X
                        Rectangle tempPositionAlongX = new Rectangle(Position.X, OldPosition.Y, OldPosition.Width, OldPosition.Height);
                        bool collideAlongX = CollisionSpriteOnMap(pGameTime, pMap, tempPositionAlongX, TextureListToCheck) != null;

                        // check the movement along Y
                        Rectangle tempPositionAlongY = new Rectangle(OldPosition.X, Position.Y, OldPosition.Width, OldPosition.Height);
                        bool collideAlongY = CollisionSpriteOnMap(pGameTime, pMap, tempPositionAlongY, TextureListToCheck) != null;
                        
                        // if both collide (corner), don't move and exit the if statement
                        if (collideAlongX && collideAlongY)
                        {
                            Position = OldPosition;
                            return;
                        }

                        // if along X collide, let's move along Y
                        if (collideAlongX)
                        {
                            Position = tempPositionAlongY;
                        }

                        // if along Y collide, let's move along X
                        if (collideAlongY)
                        {
                            Position = tempPositionAlongX;
                        }
                    }
                    else // if no diagonal, back to the old position
                    {
                        Position = OldPosition;
                    }
                }
            }
            #endregion
        }
        #endregion

        public virtual void SpriteDraw(GameTime pGameTime) { }

        #region Method to check if a sprite collide on a specific list of texture of the map
        public static TileObject CollisionSpriteOnMap(GameTime pGameTime, Map pMap, Rectangle pSpritePosition, List<MapTexture> pListTextureToCheck)
        {
            TileObject tile = null;

            for (int row = 0; row < pMap.MapSizeInTile.Height; row++)
            {
                for (int column = 0; column < pMap.MapSizeInTile.Width; column++)
                {
                    if (pListTextureToCheck.Contains(pMap.MapGrid[row, column].Texture))
                    {
                        bool collide = false;
                        collide = CollisionObject.CheckCollision(pMap.MapGrid[row, column].Position, pSpritePosition);
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
                Center = CalculateCoordinatesInTileWithPixel(NSPointsInPixel.Center, pMap.TileSizeShowing),
                NorthEast = CalculateCoordinatesInTileWithPixel(NSPointsInPixel.NorthEast, pMap.TileSizeShowing),
                SouthEast = CalculateCoordinatesInTileWithPixel(NSPointsInPixel.SouthEast, pMap.TileSizeShowing),
                SouthWest = CalculateCoordinatesInTileWithPixel(NSPointsInPixel.SouthWest, pMap.TileSizeShowing),
                NorthWest = CalculateCoordinatesInTileWithPixel(NSPointsInPixel.NorthWest, pMap.TileSizeShowing)
            };
            #endregion
        }
        #endregion

        #region Method to calculate coordinates in tile with coordinates in pixel
        private Vector2 CalculateCoordinatesInTileWithPixel(Vector2 pCoordInPixel, Rectangle pTileSize)
        {
            return new Vector2((float)Math.Floor(pCoordInPixel.X / pTileSize.Width), (float)Math.Floor(pCoordInPixel.Y / pTileSize.Height));
        }
        #endregion
    }
}
