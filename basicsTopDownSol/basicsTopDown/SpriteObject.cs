using basicsTopDown.MapFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace basicsTopDown
{
    public class SpriteObject
    {
        protected bool IsMoving { get; set; }
        protected Rectangle Size { get; set; }

        public Texture2D SpriteData { get; set; }
        public Rectangle Position { get; set; }

        public ContentManager Content { get; set; }
        public SpriteBatch SpriteBatch { get; set; }

        public SpriteObject(ContentManager pContent, SpriteBatch pSpriteBatch, Rectangle pPosition, string pSpriteName)
        {
            Content = pContent;
            SpriteBatch = pSpriteBatch;
            IsMoving = false;
        }

        public virtual void SpriteUpdate(GameTime pGameTime, Map pMap)
        {

        }


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
