using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace basicsTopDown.UtilFolder
{
    public class DebugToolBox
    {
        public static void ShowLine(ContentManager pContent, SpriteBatch pSpriteBatch, string pText, Vector2 pPosition)
        {
            SpriteFont tempFont = pContent.Load<SpriteFont>("TimesNewRoman12");

            pSpriteBatch.DrawString(tempFont, pText, new Vector2(pPosition.X - 5, pPosition.Y - 12), Color.Black);
        }
    }
}
