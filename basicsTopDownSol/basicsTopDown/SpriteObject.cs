using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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


        public void SpriteObjectUpdate(GameTime pGameTime)
        {
            // detection collision
        }

        public void SpriteObjectDraw(GameTime pGameTime)
        {
            // not in general
            // TODO for each subClass
        }
    }
}
