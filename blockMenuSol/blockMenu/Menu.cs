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
            PersonnalColors PersonnalColors = new PersonnalColors();

            MyMenuData = LoadMenuData.LoadJson();

            foreach(LineProperties item in MyMenuData.MenuItems)
            {
                // Load the Font
                item.Font = pContent.Load<SpriteFont>(item.FontFileName);

                // Manage the alignment
                switch (item.Alignment)
                {
                    case EnumLineAlignment.Left:
                        float tempNewXLeft = GameWindowWidth * (1 - item.WidthLimit);
                        float tempOldYLeft = item.AnchorPosition.Y;
                        item.AnchorPosition = new Vector2(tempNewXLeft, tempOldYLeft);
                        break;
                    case EnumLineAlignment.Center:
                        float availableSpaceCenter = (GameWindowWidth - item.AnchorPosition.X);
                        Vector2 sizeCenter = item.Font.MeasureString(item.Value);
                        float tempNewXCenter = (availableSpaceCenter - sizeCenter.X) / 2;
                        float tempOldYCenter = item.AnchorPosition.Y;
                        item.AnchorPosition = new Vector2(tempNewXCenter, tempOldYCenter);
                        break;
                    case EnumLineAlignment.Right:
                        float availableSpaceRight = (GameWindowWidth - item.AnchorPosition.X) * item.WidthLimit;
                        Vector2 sizeRight = item.Font.MeasureString(item.Value);
                        float tempNewXRight = (availableSpaceRight - sizeRight.X);
                        float tempOldYRight = item.AnchorPosition.Y;
                        item.AnchorPosition = new Vector2(tempNewXRight, tempOldYRight);
                        break;
                    default:
                        break;
                }

                // set the color of each item
                Tuple<int, int, int, int> tempColor = PersonnalColors.SetPersonnalColor(item.EnumColor);
                item.Color = new Color(tempColor.Item1, tempColor.Item2, tempColor.Item3, tempColor.Item4);

                // manage the version
                if(item.ItemName == "Version")
                {
                    Vector2 sizeVersion = item.Font.MeasureString(item.Value);
                    float tempOldXVersion = item.AnchorPosition.X;
                    float tempNewYVersion = GameWindowHeight - sizeVersion.Y;
                    item.AnchorPosition = new Vector2(tempOldXVersion, tempNewYVersion);
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
                SpriteBatch.DrawString(item.Font, item.Value, item.AnchorPosition, item.Color);
        }

        
    }
}
