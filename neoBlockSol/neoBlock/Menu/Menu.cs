using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

public class Menu
{
    LoadMenuData.MenuSelection MyMenuSelection;
    List<LoadMenuData.TitleProperties> MyMenuTitles;

    Color tempColor;

    // keyboard stuff
    //KeyBoardManager MyKeyBoardManager = new KeyBoardManager();
    KeyboardState OldState = new KeyboardState();
    KeyboardState NewState = new KeyboardState();

    // prepare the tweening
    //Tweening MyTweening;
    bool MenuIn = true; // the selection items arrive
    bool MenuOut = false; // for the selection items to go out
    bool IsMenuStable = false; // at the beginning, the menu is not usable
    Main.EnumMainState TargetState = Main.EnumMainState.MenuTitle; // target state for the tweening

    #region Constructor Menu
    public Menu()
    {
        LoadMenuData.MenuData MyMenuData;
        List<LoadMenuData.CreditsProperties> MyMenuCredits;
        List<LoadMenuData.InstructionsProperties> MyMenuInstructions;

        LoadMenuData LoadMenuData = new LoadMenuData();
        SpriteFont StandardFontTitle, StandardFontLines;

        MyMenuData = LoadMenuData.LoadHardData();
        if (MyMenuData != null)
        {
            MyMenuTitles = MyMenuData.ListeMenuTitles;
            MyMenuSelection = MyMenuData.MenuSelection;
            MyMenuCredits = MyMenuData.Credits;
            MyMenuInstructions = MyMenuData.Instructions;
        }
        else { throw new Exception("Loading Error - MyMenuData"); }

        //soundMoveSelect = Main.content.Load<SoundEffect>("moveSelect");
        //soundValidateSelect = Main.content.Load<SoundEffect>("validateSelect");
        //soundHeadBack = Main.content.Load<SoundEffect>("headBack");
        //volumeSoundEffects = 0.25f;

        StandardFontTitle = Main.GlobalContent.Load<SpriteFont>("TimesNewRoman24");
        StandardFontLines = Main.GlobalContent.Load<SpriteFont>("TimesNewRoman12");

        #region Manage the titles on the main screen
        //CreditsTitle = "The Credits";
        if (MyMenuTitles != null)
        {
            foreach (LoadMenuData.TitleProperties item in MyMenuTitles)
            {
                // Load the Font
                item.Font = Main.GlobalContent.Load<SpriteFont>(item.FontFileName);

                // Manage the alignments
                TextAlignment.ApplyHorizontalAlignment(item);
                TextAlignment.ApplyVerticalAlignment(item);

                // set the color of each item
                Tuple<int, int, int, int> tempColor = PersonnalColors.SetPersonnalColor(item.EnumColor);
                if (item.EnumColor == PersonnalColors.EnumColorName.White)
                    item.Color = Color.White;
                else
                    item.Color = new Color(tempColor.Item1, tempColor.Item2, tempColor.Item3, tempColor.Item4);
            }
        }
        else { throw new Exception("Loading Error - MyMenuTitles"); }
        #endregion

        #region Manage the Selection part
        if (MyMenuSelection != null)
        {
            // Load the Font for the Selection
            if (MyMenuSelection.FontFileName != null)
            {
                MyMenuSelection.Font = Main.GlobalContent.Load<SpriteFont>(MyMenuSelection.FontFileName);
            }
            else { throw new Exception("Missing Data Error - FontFileName"); }

            if (MyMenuSelection.SelectionItems != null)
            {
                for (int i = 0; i < MyMenuSelection.SelectionItems.Count; i++)
                {
                    // determine positions for the selection lines
                    float availableSpaceCenter = (WindowDimension.GameWindowWidth - MyMenuSelection.AnchorItems[i].X);
                    Vector2 sizeCenter = MyMenuSelection.Font.MeasureString(MyMenuSelection.SelectionItems[i]);

                    MyMenuSelection.AnchorItems[i] =
                        new Vector2((availableSpaceCenter - sizeCenter.X) / 2,
                                    (MyMenuSelection.AnchorPosition.Y / 12) * WindowDimension.GameWindowHeight + (i - 1) * sizeCenter.Y);

                }
            }
            else { throw new Exception("Missing Data Error - SelectionItems"); }
        }
        else { throw new Exception("Loading Error - MyMenuSelection"); }
        #endregion

        //backArrowPic = Main.content.Load<Texture2D>("backArrow");
        //backArrowTarget = new Rectangle(5, 5, 32, 32);
        //backArrowTextPos = new Vector2(50, 5);
        //backArrowText = "Esc";

        #region Manage the Credits

        // Load fonts
        //CreditsFontTitle = StandardFontTitle;
        //CreditsFontLines = StandardFontLines;

        //// measure size text
        //Vector2 sizeCreditsTitle = CreditsFontTitle.MeasureString(CreditsTitle);
        //Vector2 sizeCreditsLine = new Vector2();
        //if (MyMenuCredits != null && MyMenuCredits.Count != 0)
        //    sizeCreditsLine = CreditsFontLines.MeasureString(MyMenuCredits[0].Assets);

        //// manage the centered title
        //float tempNewXCreditsTitle = (GameWindowWidth - sizeCreditsTitle.X) / 2;
        //CreditsTitlePosition = new Vector2(tempNewXCreditsTitle, GameWindowHeight / 12);

        //// manage the positions of the credits
        //if (MyMenuCredits != null)
        //{
        //    for (int i = 0; i < MyMenuCredits.Count; i++)
        //    {
        //        var credit = MyMenuCredits[i];
        //        for (int j = 0; j < credit.AnchorPosition.Count; j++)
        //        {
        //            var anchor = credit.AnchorPosition[j];
        //            MyMenuCredits[i].AnchorPosition[j] =
        //                new Vector2(GameWindowWidth / 12, sizeCreditsTitle.Y * 3 // anchor of the whole credits
        //                                                + j * sizeCreditsLine.Y // anchor of each lines
        //                                                + i * sizeCreditsLine.Y * 4); // anchor of each block
        //        }
        //    }
        //}

        #region Manage the Instructions part

        //InstructionsTitle = "The Instructions";

        //// Load fonts
        //InstructionsFontTitle = StandardFontTitle;
        //InstructionsFontLines = StandardFontLines;

        //// measure size text
        //Vector2 sizeInstructionsTitle = InstructionsFontTitle.MeasureString(InstructionsTitle);
        //Vector2 sizeInstructionsLine = new Vector2();
        //if (MyMenuInstructions != null && MyMenuInstructions.Count != 0)
        //    sizeInstructionsLine = InstructionsFontLines.MeasureString(MyMenuInstructions[0].Action);

        //// manage the centered title
        //float tempNewXInstructionsTitle = (GameWindowWidth - sizeInstructionsTitle.X) / 2;
        //InstructionsTitlePosition = new Vector2(tempNewXInstructionsTitle, GameWindowHeight / 12);

        //// manage the positions of the instructions
        //if (MyMenuInstructions != null)
        //{
        //    for (int i = 0; i < MyMenuInstructions.Count; i++)
        //    {
        //        var instruction = MyMenuInstructions[i];
        //        for (int j = 0; j < instruction.AnchorPosition.Count; j++)
        //        {
        //            var anchor = instruction.AnchorPosition[j];
        //            MyMenuInstructions[i].AnchorPosition[j] =
        //                new Vector2(GameWindowWidth / 12, sizeInstructionsTitle.Y * 3 // anchor of the whole credits
        //                                                + j * sizeInstructionsLine.Y // anchor of each lines
        //                                                + i * sizeInstructionsLine.Y * 4); // anchor of each block
        //        }
        //    }
        //}
        #endregion

        #endregion
    }
    #endregion

    #region MenuTitleUpdate
    public Main.EnumMainState MenuTitleUpdate(GameTime pGameTime, Main.EnumMainState pMyState)
    {
        if (IsMenuStable == true)
        {
            NewState = Keyboard.GetState();

            #region Manage the move through the selection menu

            if (NewState.IsKeyDown(Keys.Down) && !OldState.IsKeyDown(Keys.Down))
            {
                //soundMoveSelect.Play(volumeSoundEffects, 0.0f, 0.0f);
                MyMenuSelection.ItemSelected += 1;
            }
            if (NewState.IsKeyDown(Keys.Up) && !OldState.IsKeyDown(Keys.Up))
            {
                //soundMoveSelect.Play(volumeSoundEffects, 0.0f, 0.0f);
                MyMenuSelection.ItemSelected -= 1;
            }

            if (MyMenuSelection != null && MyMenuSelection.SelectionItems != null)
            {
                if (MyMenuSelection.ItemSelected < 0)
                    MyMenuSelection.ItemSelected = MyMenuSelection.SelectionItems.Count - 1;
                if (MyMenuSelection.ItemSelected > MyMenuSelection.SelectionItems.Count - 1)
                    MyMenuSelection.ItemSelected = 0;
            }
            #endregion

            #region Manage the MainState status
            if (NewState.IsKeyDown(Keys.Enter) && !OldState.IsKeyDown(Keys.Enter))
            {
                //soundValidateSelect.Play(volumeSoundEffects, 0.0f, 0.0f);

                if (MyMenuSelection.SelectionItems[MyMenuSelection.ItemSelected] == "Quit")
                { // wait for the sound to end then quit
                    //System.Threading.Thread.Sleep(soundValidateSelect.Duration);
                    pMyState = Main.EnumMainState.MenuQuit;
                }

                if (MyMenuSelection.SelectionItems[MyMenuSelection.ItemSelected] == "Credits")
                { // initialize tweening parameters
                    MenuOut = true;
                    IsMenuStable = false;
                    //InitializeTweening();
                    TargetState = Main.EnumMainState.MenuCredits;
                }

                if (MyMenuSelection.SelectionItems[MyMenuSelection.ItemSelected] == "New game")
                { // initialize tweening parameters
                    MenuOut = true;
                    IsMenuStable = false;
                    //InitializeTweening();
                    TargetState = Main.EnumMainState.GamePlayable; // or GameAnimation maybe
                }

                if (MyMenuSelection.SelectionItems[MyMenuSelection.ItemSelected] == "Instructions")
                { // initialize tweening parameters
                    MenuOut = true;
                    IsMenuStable = false;
                    //InitializeTweening();
                    TargetState = Main.EnumMainState.MenuInstructions;
                }
            }
            #endregion

            OldState = NewState;
        }
        else
        { // call the tweening effect
            //pMyState = TweeningSelectionLines(pGameTime, MenuIn, MenuOut, pMyState, TargetState);
        }

        return pMyState;
    }
    #endregion

    #region MenuTitleDraw
    public void MenuTitleDraw(GameTime pGameTime)
    {
        #region Draw the Titles of the main menu
        if (MyMenuTitles != null)
        {
            foreach (LoadMenuData.TitleProperties item in MyMenuTitles)
                Main.GlobalSpriteBatch.DrawString(item.Font, item.Value, item.AnchorPosition, item.Color);
        }
        #endregion

        #region Draw the selection menu
        if (MyMenuSelection != null && MyMenuSelection.SelectionItems != null)
        {
            for (int i = 0; i < MyMenuSelection.SelectionItems.Count; i++)
            {
                tempColor = Color.SlateGray;
                if (MyMenuSelection.ItemSelected == i)
                {
                    tempColor = Color.White;
                    //MyMenuSelection.SelectionItems[i] = string.Format("> {0} <", MyMenuSelection.SelectionItems[i]);
                }

                Main.GlobalSpriteBatch.DrawString(MyMenuSelection.Font, MyMenuSelection.SelectionItems[i], MyMenuSelection.AnchorItems[i], tempColor);
            }
            //SpriteBatch.DrawString(MyMenuSelection.Font, MyMenuSelection.SelectionItems[0], tweeningPos, tempColor);
        }
        #endregion
    }
    #endregion
}
