using basicsTopDown.MapFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace basicsTopDown
{
    public class SpriteObject
    {
        protected bool IsMoving { get; set; }
        protected double GameSizeCoefficient { get; set; }
        protected Rectangle Size { get; set; }

        public Texture2D SpriteData { get; set; }
        public Rectangle Position { get; set; }
        public NineSlicePoints NSPointsInPixel { get; set; }
        public NineSlicePoints NSPointsInCoordinate { get; set; }

        protected ContentManager Content { get; set; }
        protected SpriteBatch SpriteBatch { get; set; }

        public class NineSlicePoints
        {
            public Vector2 Coord1North { get; set; }
            public Vector2 Coord2NorthEast { get; set; }
            public Vector2 Coord3East { get; set; }
            public Vector2 Coord4SouthEast { get; set; }
            public Vector2 Coord5South { get; set; }
            public Vector2 Coord6SouthWest { get; set; }
            public Vector2 Coord7West { get; set; }
            public Vector2 Coord8NorthWest { get; set; }
            public Vector2 Coord9Center { get; set; }
        }

        public SpriteObject(ContentManager pContent, SpriteBatch pSpriteBatch, Rectangle pPosition, string pSpriteName, double pGameSizeCoefficient)
        {
            Content = pContent;
            SpriteBatch = pSpriteBatch;
            IsMoving = false;
            GameSizeCoefficient = pGameSizeCoefficient;
        }

        // method to run for each character
        public void RunSpriteUpdate(GameTime pGameTime, Map pMap)
        {
            if (IsMoving)
            {
                #region 9 slices points position of the sprite in pixel
                NSPointsInPixel = new NineSlicePoints
                {
                    Coord1North = new Vector2(Position.X + Size.Width / 2, Position.Y),
                    Coord2NorthEast = new Vector2(Position.X + Size.Width, Position.Y),
                    Coord3East = new Vector2(Position.X + Size.Width, Position.Y + Size.Height / 2),
                    Coord4SouthEast = new Vector2(Position.X + Size.Width, Position.Y + Size.Height),
                    Coord5South = new Vector2(Position.X + Size.Width / 2, Position.Y + Size.Height),
                    Coord6SouthWest = new Vector2(Position.X, Position.Y + Size.Height),
                    Coord7West = new Vector2(Position.X, Position.Y + Size.Height / 2),
                    Coord8NorthWest = new Vector2(Position.X, Position.Y),
                    Coord9Center = new Vector2(Position.X + Size.Width / 2, Position.Y + Size.Height / 2)
                };
                #endregion

                #region 9 slices points position of the sprite in coordinate
                NSPointsInCoordinate = new NineSlicePoints
                {
                    Coord1North = new Vector2((float)Math.Floor(NSPointsInPixel.Coord1North.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.Coord1North.Y / pMap.TileSizeShowing.Height)),
                    Coord2NorthEast = new Vector2((float)Math.Floor(NSPointsInPixel.Coord2NorthEast.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.Coord2NorthEast.Y / pMap.TileSizeShowing.Height)),
                    Coord3East = new Vector2((float)Math.Floor(NSPointsInPixel.Coord3East.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.Coord3East.Y / pMap.TileSizeShowing.Height)),
                    Coord4SouthEast = new Vector2((float)Math.Floor(NSPointsInPixel.Coord4SouthEast.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.Coord4SouthEast.Y / pMap.TileSizeShowing.Height)),
                    Coord5South = new Vector2((float)Math.Floor(NSPointsInPixel.Coord5South.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.Coord5South.Y / pMap.TileSizeShowing.Height)),
                    Coord6SouthWest = new Vector2((float)Math.Floor(NSPointsInPixel.Coord6SouthWest.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.Coord6SouthWest.Y / pMap.TileSizeShowing.Height)),
                    Coord7West = new Vector2((float)Math.Floor(NSPointsInPixel.Coord7West.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.Coord7West.Y / pMap.TileSizeShowing.Height)),
                    Coord8NorthWest = new Vector2((float)Math.Floor(NSPointsInPixel.Coord8NorthWest.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.Coord8NorthWest.Y / pMap.TileSizeShowing.Height)),
                    Coord9Center = new Vector2((float)Math.Floor(NSPointsInPixel.Coord9Center.X / pMap.TileSizeShowing.Width), (float)Math.Floor(NSPointsInPixel.Coord9Center.Y / pMap.TileSizeShowing.Height))
                };
                #endregion
            }

            SpriteUpdate(pGameTime, pMap); // call the virtual method
        }

        // virtual method to override
        protected virtual void SpriteUpdate(GameTime pGameTime, Map pMap) { }
        
        public virtual void SpriteDraw(GameTime pGameTime)
        {

        }

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
    }
}
