using basicsTopDown.MapGenFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace basicsTopDown
{
    public class SpriteObject
    {
        public Texture2D SpriteData { get; set; }
        public Rectangle Position { get; set; }
        public Rectangle Size { get; set; }
        public bool IsMoving { get; set; }

        public ContentManager Content { get; set; }
        public SpriteBatch SpriteBatch { get; set; }

        public SpriteObject(ContentManager pContent, SpriteBatch pSpriteBatch, Rectangle pPosition, string pSpriteName)
        {
            Content = pContent;
            SpriteBatch = pSpriteBatch;
            IsMoving = false;
        }


        public static TileObject SpriteObjectCollision(GameTime pGameTime, MapGenerator pMap, SpriteObject pLink)
        {
            TileObject tile = null;

            for (int row = 0; row < pMap.MapSizeInTile.Height; row++)
            {
                for (int column = 0; column < pMap.MapSizeInTile.Width; column++)
                {
                    if (pMap.MapGrid[row, column].Texture == MapGenerator.MapTexture.Wall)
                    {
                        bool collide = false;
                        collide = CollisionObject.CheckCollision(pMap.MapGrid[row, column].Position, pLink.Position);
                        if (collide == true)
                        {
                            tile = pMap.MapGrid[row, column];
                            Console.WriteLine("tile : " + tile.Id);
                        }
                    }
                    
                }
            }

            return tile;
        }

        public void SpriteObjectDraw(GameTime pGameTime)
        {
            // not in general
            // TODO for each subClass
        }
    }
}
