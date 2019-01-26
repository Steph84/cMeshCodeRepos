using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ChessAI.UtilFolder
{
    public class DebugToolBox
    {
        public static void ShowLine(ContentManager pContent, SpriteBatch pSpriteBatch, string pText, Vector2 pPosition)
        {
            SpriteFont tempFont = pContent.Load<SpriteFont>("Arial12");

            pSpriteBatch.DrawString(tempFont, pText, new Vector2(pPosition.X - 5, pPosition.Y - 12), Color.Black);
        }
    }
}
