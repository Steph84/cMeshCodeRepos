using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace blockMenu
{
    public class Menu
    {
        public int GameWindowWidth { get; private set; }
        public int GameWindowHeight { get; private set; }

        SpriteBatch SpriteBatch;
        LoadMenuData.MenuData MyMenuData;
        List<LoadMenuData.LineProperties> MyMenuTitles;
        LoadMenuData.MenuSelection MyMenuSelection;
        Color tempColor;
        KeyBoardManager MyKeyBoardManager = new KeyBoardManager();
        
        public Menu(Tuple<int, int> pGameWindowSize, ContentManager pContent, SpriteBatch pSpriteBatch)
        {
            GameWindowWidth = pGameWindowSize.Item1;
            GameWindowHeight = pGameWindowSize.Item2;
            SpriteBatch = pSpriteBatch;

            LoadMenuData LoadMenuData = new LoadMenuData();
            PersonnalColors PersonnalColors = new PersonnalColors();

            MyMenuData = LoadMenuData.LoadJsonData();
            MyMenuTitles = MyMenuData.ListeMenuTitles;
            MyMenuSelection = MyMenuData.MenuSelection;
            
            foreach(LoadMenuData.LineProperties item in MyMenuTitles)
            {
                // Load the Font
                item.Font = pContent.Load<SpriteFont>(item.FontFileName);

                // Manage the alignment
                switch (item.Alignment)
                {
                    case TextAlignment.EnumLineAlignment.Left:
                        float tempNewXLeft = GameWindowWidth * (1 - item.WidthLimit);
                        float tempOldYLeft = item.AnchorPosition.Y;
                        item.AnchorPosition = new Vector2(tempNewXLeft, tempOldYLeft);
                        break;
                    case TextAlignment.EnumLineAlignment.Center:
                        float availableSpaceCenter = (GameWindowWidth - item.AnchorPosition.X);
                        Vector2 sizeCenter = item.Font.MeasureString(item.Value);
                        float tempNewXCenter = (availableSpaceCenter - sizeCenter.X) / 2;
                        float tempOldYCenter = item.AnchorPosition.Y;
                        item.AnchorPosition = new Vector2(tempNewXCenter, tempOldYCenter);
                        break;
                    case TextAlignment.EnumLineAlignment.Right:
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
                if (item.EnumColor == PersonnalColors.EnumColorName.White)
                    item.Color = Color.White;
                else
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

            // Load the Font for the Selection
            MyMenuSelection.Font = pContent.Load<SpriteFont>(MyMenuSelection.FontFileName);

            // Manage the position of each selection item
            for (int i = 0; i < MyMenuSelection.SelectionItems.Count; i++)
            {
                string selectionItem = MyMenuSelection.SelectionItems[i];
                float availableSpaceCenter = (GameWindowWidth - MyMenuSelection.AnchorItems[i].X);
                Vector2 sizeCenter = MyMenuSelection.Font.MeasureString(selectionItem);
                float tempNewXCenter = (availableSpaceCenter - sizeCenter.X) / 2;
                float tempNewYCenter = 300 + (i-1) * 75;
                MyMenuSelection.AnchorItems[i] = new Vector2(tempNewXCenter, tempNewYCenter);
            }
        }

        public void MenuUpdate(GameTime pGameTime)
        {
            if (MyKeyBoardManager.KeyBoardAction(Keys.Down) == KeyBoardManager.EnumKeyBoard.Press)
                MyMenuSelection.ItemSelected += 1;

            if (MyKeyBoardManager.KeyBoardAction(Keys.Up) == KeyBoardManager.EnumKeyBoard.Up)
                MyMenuSelection.ItemSelected -= 1;
            
            if (MyMenuSelection.ItemSelected < 0)
                MyMenuSelection.ItemSelected = MyMenuSelection.SelectionItems.Count - 1;

            if (MyMenuSelection.ItemSelected > MyMenuSelection.SelectionItems.Count - 1)
                MyMenuSelection.ItemSelected = 0;
        }

        public void MenuDraw(GameTime pGameTime)
        {
            foreach (LoadMenuData.LineProperties item in MyMenuTitles)
                SpriteBatch.DrawString(item.Font, item.Value, item.AnchorPosition, item.Color);

            for (int i = 0; i < MyMenuSelection.SelectionItems.Count; i++)
            {
                tempColor = Color.SlateGray;
                if (MyMenuSelection.ItemSelected == i)
                {
                    tempColor = Color.White;
                    //MyMenuSelection.SelectionItems[i] = string.Format("> {0} <", MyMenuSelection.SelectionItems[i]);
                }

                SpriteBatch.DrawString(MyMenuSelection.Font, MyMenuSelection.SelectionItems[i], MyMenuSelection.AnchorItems[i], tempColor);
            }
        }
    }
}
