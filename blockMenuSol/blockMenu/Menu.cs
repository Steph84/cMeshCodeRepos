using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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

        ContentManager Content;
        SpriteBatch SpriteBatch;

        LoadMenuData.MenuData MyMenuData;
        List<LoadMenuData.LineProperties> MyMenuTitles;
        LoadMenuData.MenuSelection MyMenuSelection;
        List<LoadMenuData.CreditsProperties> MyMenuCredits;

        Color tempColor;
        //KeyBoardManager MyKeyBoardManager = new KeyBoardManager();
        KeyboardState oldState = new KeyboardState();
        KeyboardState newState = new KeyboardState();
        SoundEffect soundHeadBack, soundMoveSelect, soundValidateSelect;

        string CreditsTitle = "The Credits";
        Vector2 CreditsTitlePosition = new Vector2();
        SpriteFont CreditsFont;

        float volumeSoundEffects;

        public Menu(Tuple<int, int> pGameWindowSize, ContentManager pContent, SpriteBatch pSpriteBatch)
        {
            GameWindowWidth = pGameWindowSize.Item1;
            GameWindowHeight = pGameWindowSize.Item2;
            SpriteBatch = pSpriteBatch;
            Content = pContent;

            LoadMenuData LoadMenuData = new LoadMenuData();
            PersonnalColors PersonnalColors = new PersonnalColors();
            TextAlignment TextAlignment = new TextAlignment(GameWindowWidth);

            MyMenuData = LoadMenuData.LoadJsonData();
            MyMenuTitles = MyMenuData.ListeMenuTitles;
            MyMenuSelection = MyMenuData.MenuSelection;
            MyMenuCredits = MyMenuData.Credits;

            soundMoveSelect = Content.Load<SoundEffect>("moveSelect");
            soundValidateSelect = Content.Load<SoundEffect>("validateSelect");
            soundHeadBack = Content.Load<SoundEffect>("headBack");
            volumeSoundEffects = 0.25f;

            #region Manage the titles on the main screen
            foreach (LoadMenuData.LineProperties item in MyMenuTitles)
            {
                // Load the Font
                item.Font = Content.Load<SpriteFont>(item.FontFileName);

                // Manage the alignment
                TextAlignment.ApplyAlignment(item);
                
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
            #endregion

            #region Manage the Selection part
            // Load the Font for the Selection
            MyMenuSelection.Font = Content.Load<SpriteFont>(MyMenuSelection.FontFileName);

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
            #endregion

            #region Manage the Credits
            CreditsFont = Content.Load<SpriteFont>("TimesNewRoman");
            Vector2 sizeCreditsTitle = CreditsFont.MeasureString(CreditsTitle);
            float tempNewXCreditsTitle = (GameWindowWidth - sizeCreditsTitle.X) / 2;
            CreditsTitlePosition = new Vector2(tempNewXCreditsTitle, 20);

            for (int i = 0; i < MyMenuCredits.Count; i++)
            {
                var credit = MyMenuCredits[i];
                for (int j = 0; j < credit.AnchorPosition.Count; j++)
                {
                    var anchor = credit.AnchorPosition[j];
                    MyMenuCredits[i].AnchorPosition[j] = new Vector2(50, 150 + j * credit.TextGap + i * credit.TextGap * 4);
                }
            }
            #endregion
        }

        public Main.EnumMainState MenuTitleUpdate(GameTime pGameTime, Main.EnumMainState pMyState)
        {
            #region Manage the move through the selection menu
            newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.Down) && !oldState.IsKeyDown(Keys.Down))
            {
                soundMoveSelect.Play(volumeSoundEffects, 0.0f, 0.0f);
                MyMenuSelection.ItemSelected += 1;
            }
            if (newState.IsKeyDown(Keys.Up) && !oldState.IsKeyDown(Keys.Up))
            {
                soundMoveSelect.Play(volumeSoundEffects, 0.0f, 0.0f);
                MyMenuSelection.ItemSelected -= 1;
            }
            
            if (MyMenuSelection.ItemSelected < 0)
                MyMenuSelection.ItemSelected = MyMenuSelection.SelectionItems.Count - 1;
            if (MyMenuSelection.ItemSelected > MyMenuSelection.SelectionItems.Count - 1)
                MyMenuSelection.ItemSelected = 0;
            #endregion

            #region Manage the MainState status
            if (newState.IsKeyDown(Keys.Enter) && !oldState.IsKeyDown(Keys.Enter))
            {
                soundValidateSelect.Play(volumeSoundEffects, 0.0f, 0.0f);

                if (MyMenuSelection.SelectionItems[MyMenuSelection.ItemSelected] == "Quit")
                {
                    System.Threading.Thread.Sleep(soundValidateSelect.Duration);
                    pMyState = Main.EnumMainState.MenuQuit;
                }

                if (MyMenuSelection.SelectionItems[MyMenuSelection.ItemSelected] == "Credits")
                    pMyState = Main.EnumMainState.MenuCredits;
                
                if (MyMenuSelection.SelectionItems[MyMenuSelection.ItemSelected] == "New game")
                    pMyState = Main.EnumMainState.GamePlayable; // or GameAnimation maybe
            }

            oldState = newState;

            return pMyState;
            #endregion
        }

        public Main.EnumMainState MenuCreditsUpdate(GameTime pGameTime, Main.EnumMainState pMyState)
        {
            // wait for escape

            return pMyState;
        }

        public void MenuTitleDraw(GameTime pGameTime)
        {
            #region Draw the Titles of the main menu
            foreach (LoadMenuData.LineProperties item in MyMenuTitles)
                SpriteBatch.DrawString(item.Font, item.Value, item.AnchorPosition, item.Color);
            #endregion

            #region Draw the selection menu
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
            #endregion
        }

        public void MenuCreditsDraw(GameTime pGameTime)
        {
            SpriteBatch.DrawString(CreditsFont, CreditsTitle, CreditsTitlePosition, Color.White);

            foreach (var credit in MyMenuCredits)
            {
                SpriteBatch.DrawString(CreditsFont, credit.Assets, credit.AnchorPosition[0], Color.White);
                SpriteBatch.DrawString(CreditsFont, credit.Name, credit.AnchorPosition[1], Color.White);
                SpriteBatch.DrawString(CreditsFont, credit.Source, credit.AnchorPosition[2], Color.White);
            }
        }
    }
}
