﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

public class Menu
{
    private LoadMenuData.MenuSelection MyMenuSelection;
    private List<LoadMenuData.TitleProperties> MyMenuTitles;
    private List<LoadMenuData.CreditsProperties> MyMenuCredits;
    private List<LoadMenuData.InstructionsProperties> MyMenuInstructions;

    private Color tempColor;

    // keyboard stuff
    //KeyBoardManager MyKeyBoardManager = new KeyBoardManager();
    private KeyboardState OldState = new KeyboardState();
    private KeyboardState NewState = new KeyboardState();

    // Credits stuff
    private string CreditsTitle;
    private Vector2 CreditsTitlePosition;
    private SpriteFont CreditsFontTitle, CreditsFontLines;

    // Instructions stuff
    private string InstructionsTitle;
    private Vector2 InstructionsTitlePosition;
    private SpriteFont InstructionsFontTitle, InstructionsFontLines;

    // prepare the tweening
    //Tweening MyTweening;
    private bool MenuIn = true; // the selection items arrive
    private bool MenuOut = false; // for the selection items to go out
    private bool IsMenuStable = true; // at the beginning, the menu is not usable
    private Main.EnumMainState TargetState = Main.EnumMainState.MenuTitle; // target state for the tweening

    #region Constructor Menu
    public Menu()
    {
        LoadMenuData.MenuData MyMenuData;

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
        CreditsTitle = "The Credits";
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
                    Vector2 sizeCenter = MyMenuSelection.Font.MeasureString(MyMenuSelection.SelectionItems[i].Item2);

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

        // TODO test if we have Credits

        // Load fonts
        CreditsFontTitle = StandardFontTitle;
        CreditsFontLines = StandardFontLines;

        // measure size text
        Vector2 sizeCreditsTitle = CreditsFontTitle.MeasureString(CreditsTitle);
        Vector2 sizeCreditsLine = new Vector2();
        if (MyMenuCredits != null && MyMenuCredits.Count != 0)
            sizeCreditsLine = CreditsFontLines.MeasureString(MyMenuCredits[0].Assets);

        // manage the centered title
        float tempNewXCreditsTitle = (WindowDimension.GameWindowWidth - sizeCreditsTitle.X) / 2;
        CreditsTitlePosition = new Vector2(tempNewXCreditsTitle, WindowDimension.GameWindowHeight / 12);

        // manage the positions of the credits
        if (MyMenuCredits != null)
        {
            for (int i = 0; i < MyMenuCredits.Count; i++)
            {
                var credit = MyMenuCredits[i];
                for (int j = 0; j < credit.AnchorPosition.Count; j++)
                {
                    var anchor = credit.AnchorPosition[j];
                    MyMenuCredits[i].AnchorPosition[j] =
                        new Vector2(WindowDimension.GameWindowHeight / 12,
                                    sizeCreditsTitle.Y * 3 // anchor of the whole credits
                                    + j * sizeCreditsLine.Y // anchor of each lines
                                    + i * sizeCreditsLine.Y * 4); // anchor of each block
                }
            }
        }

        #endregion

        #region Manage the Instructions part

        // TODO test if we have Instructions

        InstructionsTitle = "The Instructions";

        // Load fonts
        InstructionsFontTitle = StandardFontTitle;
        InstructionsFontLines = StandardFontLines;

        // measure size text
        Vector2 sizeInstructionsTitle = InstructionsFontTitle.MeasureString(InstructionsTitle);
        Vector2 sizeInstructionsLine = new Vector2();
        if (MyMenuInstructions != null && MyMenuInstructions.Count != 0)
            sizeInstructionsLine = InstructionsFontLines.MeasureString(MyMenuInstructions[0].Action);

        // manage the centered title
        float tempNewXInstructionsTitle = (WindowDimension.GameWindowWidth - sizeInstructionsTitle.X) / 2;
        InstructionsTitlePosition = new Vector2(tempNewXInstructionsTitle, WindowDimension.GameWindowHeight / 12);

        // manage the positions of the instructions
        if (MyMenuInstructions != null)
        {
            for (int i = 0; i < MyMenuInstructions.Count; i++)
            {
                var instruction = MyMenuInstructions[i];
                for (int j = 0; j < instruction.AnchorPosition.Count; j++)
                {
                    var anchor = instruction.AnchorPosition[j];
                    MyMenuInstructions[i].AnchorPosition[j] =
                        new Vector2(WindowDimension.GameWindowHeight / 12,
                                    sizeInstructionsTitle.Y * 3 // anchor of the whole credits
                                    + j * sizeInstructionsLine.Y // anchor of each lines
                                    + i * sizeInstructionsLine.Y * 4); // anchor of each block
                }
            }
        }
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

                // TODO put a switch now we have Enum

                if (MyMenuSelection.SelectionItems[MyMenuSelection.ItemSelected].Item1 == LoadMenuData.EnumMenuItem.Quit)
                { // wait for the sound to end then quit
                    //System.Threading.Thread.Sleep(soundValidateSelect.Duration);
                    pMyState = Main.EnumMainState.MenuQuit;
                }

                if (MyMenuSelection.SelectionItems[MyMenuSelection.ItemSelected].Item1 == LoadMenuData.EnumMenuItem.Credits)
                { // initialize tweening parameters
                    //MenuOut = true;
                    //IsMenuStable = false;
                    //InitializeTweening();
                    TargetState = Main.EnumMainState.MenuCredits;
                    return Main.EnumMainState.MenuCredits; // TO REMOVE
                }

                if (MyMenuSelection.SelectionItems[MyMenuSelection.ItemSelected].Item1 == LoadMenuData.EnumMenuItem.NewGame)
                { // initialize tweening parameters
                    //MenuOut = true;
                    //IsMenuStable = false;
                    //InitializeTweening();
                    TargetState = Main.EnumMainState.GamePlayable; // or GameAnimation maybe
                }

                if (MyMenuSelection.SelectionItems[MyMenuSelection.ItemSelected].Item1 == LoadMenuData.EnumMenuItem.Instructions)
                { // initialize tweening parameters
                    //MenuOut = true;
                    //IsMenuStable = false;
                    //InitializeTweening();
                    TargetState = Main.EnumMainState.MenuInstructions;
                    return Main.EnumMainState.MenuInstructions; // TO REMOVE
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

    #region MenuInstructionsUpdate
    public Main.EnumMainState MenuInstructionsUpdate(GameTime pGameTime, Main.EnumMainState pMyState)
    {
        NewState = Keyboard.GetState();

        if (NewState.IsKeyDown(Keys.Escape) && !OldState.IsKeyDown(Keys.Escape))
        {
            //soundHeadBack.Play(volumeSoundEffects, 0.0f, 0.0f);
            // initialize the tweening parameters
            //InitializeTweening();
            //IsMenuStable = false;
            //MenuIn = true;
            pMyState = Main.EnumMainState.MenuTitle;
        }

        OldState = NewState;

        return pMyState;
    }
    #endregion

    #region MenuCreditsUpdate
    public Main.EnumMainState MenuCreditsUpdate(GameTime pGameTime, Main.EnumMainState pMyState)
    {
        NewState = Keyboard.GetState();

        if (NewState.IsKeyDown(Keys.Escape) && !OldState.IsKeyDown(Keys.Escape))
        {
            //soundHeadBack.Play(volumeSoundEffects, 0.0f, 0.0f);
            // initialize the tweening parameters
            //InitializeTweening();
            //IsMenuStable = false;
            //MenuIn = true;
            pMyState = Main.EnumMainState.MenuTitle;
        }

        OldState = NewState;

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

                Main.GlobalSpriteBatch.DrawString(MyMenuSelection.Font, MyMenuSelection.SelectionItems[i].Item2, MyMenuSelection.AnchorItems[i], tempColor);
            }
            //SpriteBatch.DrawString(MyMenuSelection.Font, MyMenuSelection.SelectionItems[0], tweeningPos, tempColor);
        }
        #endregion
    }
    #endregion

    #region MenuInstructionsDraw
    public void MenuInstructionsDraw(GameTime pGameTime)
    {
        // Draw Instructions title
        Main.GlobalSpriteBatch.DrawString(InstructionsFontTitle, InstructionsTitle, InstructionsTitlePosition, Color.White);

        // Draw Instructions themselves
        foreach (var instruction in MyMenuInstructions)
        {
            Main.GlobalSpriteBatch.DrawString(InstructionsFontLines, instruction.Action, instruction.AnchorPosition[0], Color.White);
            Main.GlobalSpriteBatch.DrawString(InstructionsFontLines, instruction.Control, instruction.AnchorPosition[1], Color.White);
        }

        // Draw the BackArrow pic
        //SpriteBatch.Draw(backArrowPic, backArrowTarget, Color.White);
        //SpriteBatch.DrawString(InstructionsFontTitle, backArrowText, backArrowTextPos, Color.White);
    }
    #endregion

    #region MenuCreditsDraw
    public void MenuCreditsDraw(GameTime pGameTime)
    {
        // Draw Credits title
        Main.GlobalSpriteBatch.DrawString(CreditsFontTitle, CreditsTitle, CreditsTitlePosition, Color.White);

        // Draw Credits themselves
        foreach (var credit in MyMenuCredits)
        {
            Main.GlobalSpriteBatch.DrawString(CreditsFontLines, credit.Assets, credit.AnchorPosition[0], Color.White);
            Main.GlobalSpriteBatch.DrawString(CreditsFontLines, credit.Name, credit.AnchorPosition[1], Color.White);
            Main.GlobalSpriteBatch.DrawString(CreditsFontLines, credit.Source, credit.AnchorPosition[2], Color.White);
        }

        // Draw the BackArrow pic
        //SpriteBatch.Draw(backArrowPic, backArrowTarget, Color.White);
        //SpriteBatch.DrawString(CreditsFontTitle, backArrowText, backArrowTextPos, Color.White);
    }
    #endregion
}
