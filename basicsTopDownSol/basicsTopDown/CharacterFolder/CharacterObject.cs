using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace basicsTopDown.CharacterFolder
{
    public class CharacterObject : SpriteObject
    {
        public string Name { get; set; }
        
        public CharacterObject(ContentManager pContent, SpriteBatch pSpriteBatch, Rectangle pPosition, string pSpriteName) : base(pContent, pSpriteBatch, pPosition, pSpriteName)
        {
            SpriteData = Content.Load<Texture2D>(pSpriteName);
            Size = new Rectangle(0, 0, SpriteData.Width, SpriteData.Height);
            Position = new Rectangle(pPosition.X, pPosition.Y, SpriteData.Width, SpriteData.Height);
        }

        public void CharacterDraw(GameTime pGameTime)
        {
            SpriteBatch.Draw(SpriteData, Position, Size, Color.White);
        }
    }
}
