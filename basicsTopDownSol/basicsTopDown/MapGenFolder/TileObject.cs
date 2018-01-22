using Microsoft.Xna.Framework;
using static basicsTopDown.MapGenFolder.MapGenerator;

namespace basicsTopDown.MapGenFolder
{
    public class TileObject : SpriteObject
    {
        public MapTexture Texture { get; set; }
        public int Flag { get; set; }

        public TileObject(Rectangle pPosition, MapTexture pTexture) : base(pPosition)
        {
            Texture = pTexture;
            Flag = -1;
        }
    }
}
