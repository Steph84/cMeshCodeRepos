using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using static blockMenu.LoadMenuData;

namespace blockMenu
{
    public class Menu
    {
        public int GameWindowWidth { get; private set; }
        public int GameWindowHeight { get; private set; }

        SpriteBatch SpriteBatch;
        MenuDto MyMenuData;

        public Menu(Tuple<int, int> pGameWindowSize, ContentManager pContent, SpriteBatch pSpriteBatch)
        {
            GameWindowWidth = pGameWindowSize.Item1;
            GameWindowHeight = pGameWindowSize.Item2;
            SpriteBatch = pSpriteBatch;

            LoadMenuData LoadMenuData = new LoadMenuData();
            MyMenuData = LoadMenuData.LoadJson();

            foreach(LineProperties item in MyMenuData.MenuItems)
            {
                item.Font = pContent.Load<SpriteFont>(item.FontFileName);
            }

        }

        public void MenuDraw(GameTime pGameTime)
        {
            foreach (LineProperties item in MyMenuData.MenuItems)
            {
                SpriteBatch.DrawString(item.Font, item.Value, item.AnchorPosition, Color.Black);
            }
        }

        //Vector2 size = font.MeasureString(title1);
    }
}
