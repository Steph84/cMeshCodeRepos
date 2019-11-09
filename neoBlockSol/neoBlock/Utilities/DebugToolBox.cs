using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class DebugToolBox
{
    public static void ShowLine(string pText, Vector2 pPosition)
    {
        SpriteFont tempFont = Main.GlobalContent.Load<SpriteFont>("TimesNewRoman12");

        Main.GlobalSpriteBatch.DrawString(tempFont, pText, new Vector2(pPosition.X - 5, pPosition.Y - 12), Color.Black);
    }
}