using Microsoft.Xna.Framework;

namespace blockMapGenerator.UtilFolder
{
    public class SpriteObject
    {
        public Rectangle Position { get; set; }

        public SpriteObject(Rectangle pPosition)
        {
            Position = pPosition;
        }
    }
}
