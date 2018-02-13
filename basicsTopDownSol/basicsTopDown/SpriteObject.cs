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
            public Vector2 North { get; set; }
            public Vector2 East { get; set; }
            public Vector2 South { get; set; }
            public Vector2 West { get; set; }
            public Vector2 Center { get; set; }
            //public Vector2 NorthEast { get; set; }
            //public Vector2 SouthEast { get; set; }
            //public Vector2 SouthWest { get; set; }
            //public Vector2 NorthWest { get; set; }
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
