using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static basicsTopDown.MapGenFolder.MapGenerator;

namespace basicsTopDown.MapGenFolder
{
    public class TileObject : SpriteObject
    {
        public MapTexture Texture { get; set; }
        public int Flag { get; set; }

        public TileObject(ContentManager pContent, SpriteBatch pSpriteBatch, Rectangle pPosition, string pSpriteName, MapTexture pTexture) : base(pContent, pSpriteBatch, pPosition, pSpriteName)
        {
            Texture = pTexture;
            Flag = -1;
            Position = pPosition;
            Size = new Rectangle(0, 0, pPosition.Width, pPosition.Height);
        }
    }
}
