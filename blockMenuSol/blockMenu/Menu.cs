using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace blockMenu
{
    public class Menu
    {
        public int GameWindowWidth { get; private set; }
        public int GameWindowHeight { get; private set; }

        SpriteBatch SpriteBatch;
        SpriteFont Font;
        string title1 = "Title 01 oooooooooooooooooooooooooooooooooooooooooooooooooo";

        

        public Menu(Tuple<int, int> pGameWindowSize, ContentManager pContent, SpriteBatch pSpriteBatch)
        {
            GameWindowWidth = pGameWindowSize.Item1;
            GameWindowHeight = pGameWindowSize.Item2;
            Font = pContent.Load<SpriteFont>("title01");
            SpriteBatch = pSpriteBatch;

            LoadMenuData truc = new LoadMenuData();
            truc.LoadJson();

        }

        public void MenuDraw(GameTime pGameTime)
        {
            SpriteBatch.DrawString(Font, title1, new Vector2(100, 100), Color.Black);
        }

        //Vector2 size = font.MeasureString(title1);
    }
}
