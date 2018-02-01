using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace basicsTopDown.CharacterFolder
{
    public class CharacterObject : SpriteObject
    {
        public string Name { get; set; }

        SpriteFont font;

        // properties for the movement

        public CharacterObject(ContentManager pContent, SpriteBatch pSpriteBatch, Rectangle pPosition, string pSpriteName) : base(pContent, pSpriteBatch, pPosition, pSpriteName)
        {
            font = Content.Load<SpriteFont>("TimesNewRoman12");

            SpriteData = Content.Load<Texture2D>(pSpriteName);
            Size = new Rectangle(0, 0, SpriteData.Width, SpriteData.Height);
            Position = new Rectangle(pPosition.X, pPosition.Y, SpriteData.Width, SpriteData.Height);
        }
        
        public void CharacterDraw(GameTime pGameTime)
        {
            SpriteBatch.Draw(SpriteData, Position, Size, Color.White);
            SpriteBatch.DrawString(font, Position.ToString(), new Vector2(Position.X - 50, Position.Y - 50), Color.Black);
        }
    }
}
