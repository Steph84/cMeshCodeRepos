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
                // Load the Font
                item.Font = pContent.Load<SpriteFont>(item.FontFileName);

                // Manage the alignment
                switch (item.Alignment)
                {
                    case LineAlignment.Left:
                        float tempNewXLeft = GameWindowWidth * (1 - item.WidthLimit);
                        float tempOldYLeft = item.AnchorPosition.Y;
                        item.AnchorPosition = new Vector2(tempNewXLeft, tempOldYLeft);
                        break;
                    case LineAlignment.Center:
                        float availableSpaceCenter = (GameWindowWidth - item.AnchorPosition.X);
                        Vector2 sizeCenter = item.Font.MeasureString(item.Value);
                        float tempNewXCenter = (availableSpaceCenter - sizeCenter.X) / 2;
                        float tempOldYCenter = item.AnchorPosition.Y;
                        item.AnchorPosition = new Vector2(tempNewXCenter, tempOldYCenter);
                        break;
                    case LineAlignment.Right:
                        float availableSpaceRight = (GameWindowWidth - item.AnchorPosition.X) * item.WidthLimit;
                        Vector2 sizeRight = item.Font.MeasureString(item.Value);
                        float tempNewXRight = (availableSpaceRight - sizeRight.X);
                        float tempOldYRight = item.AnchorPosition.Y;
                        item.AnchorPosition = new Vector2(tempNewXRight, tempOldYRight);
                        break;
                    default:
                        break;
                }
            }

        }

        public void MenuUpdate(GameTime pGameTime)
        {
            // move of the selection with tweening
        }

        public void MenuDraw(GameTime pGameTime)
        {
            foreach (LineProperties item in MyMenuData.MenuItems)
                SpriteBatch.DrawString(item.Font, item.Value, item.AnchorPosition, Color.Black);
        }

        
    }
}
