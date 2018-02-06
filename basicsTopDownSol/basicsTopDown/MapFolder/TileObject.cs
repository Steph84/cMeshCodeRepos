using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static basicsTopDown.MapFolder.Map;

namespace basicsTopDown.MapFolder
{
    public class TileObject : SpriteObject
    {
        public int Id { get; set; }
        public MapTexture Texture { get; set; }
        public int Flag { get; set; }
        public Vector2 Coordinate { get; set; }

        public TileObject(ContentManager pContent, SpriteBatch pSpriteBatch, Rectangle pPosition, string pSpriteName, double pGameSizeCoefficient, MapTexture pTexture) : base(pContent, pSpriteBatch, pPosition, pSpriteName, pGameSizeCoefficient)
        {
            Texture = pTexture;
            Flag = -1;
            Position = pPosition;
            Size = new Rectangle(0, 0, pPosition.Width, pPosition.Height);
        }
    }
}
