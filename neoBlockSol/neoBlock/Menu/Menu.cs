using Microsoft.Xna.Framework;
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

    // sound stuff
    SoundEffect SoundHeadBack, SoundMoveSelect, SoundValidateSelect;
    float SoundVolumeEffects;

    // keyboard stuff
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

    // backArrow stuff
    private Texture2D BackArrowPic;
    private Rectangle BackArrowTarget;
    private Vector2 BackArrowTextPos;
    private string BackArrowText;

    #region Data for the tweening
    private double InitTime = 0;
    private double InitDuration = 1.25;
    private Tweening MyTweening = new Tweening();
    // the selection items arrive or to go out
    private Tweening.DirectionObject DirectionMenu = Tweening.DirectionObject.In;
    private bool IsMenuStable = false; // at the beginning, the menu is not usable
    private Main.EnumMainState TargetState = Main.EnumMainState.MenuTitle; // target state for the tweening

    // List of positions for each selection item
    List<float> TweeningTargetPosIn;
    List<float> TweeningOriginPosIn;
    List<float> TweeningTargetPosOut;
    List<float> TweeningOriginPosOut;
    #endregion

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

        SoundMoveSelect = Main.GlobalContent.Load<SoundEffect>("moveSelect");
        SoundValidateSelect = Main.GlobalContent.Load<SoundEffect>("validateSelect");
        SoundHeadBack = Main.GlobalContent.Load<SoundEffect>("headBack");
        SoundVolumeEffects = 0.25f;

        StandardFontTitle = Main.GlobalContent.Load<SpriteFont>("TimesNewRoman24");
        StandardFontLines = Main.GlobalContent.Load<SpriteFont>("TimesNewRoman12");

        if (MyMenuTitles != null)
        {
            #region Manage the titles on the main screen
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
            #endregion

            #region Manage the Selection part
            // Load the Font for the Selection
            if (MyMenuSelection.FontFileName != null)
            {
                MyMenuSelection.Font = Main.GlobalContent.Load<SpriteFont>(MyMenuSelection.FontFileName);
            }
            else { throw new Exception("Missing Data Error - FontFileName"); }
            
            // Manage the position of each selection item
            TweeningOriginPosIn = new List<float>();
            TweeningTargetPosIn = new List<float>();
            TweeningOriginPosOut = new List<float>();
            TweeningTargetPosOut = new List<float>();

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

                    // determine positions for the tweening
                    TweeningTargetPosIn.Add(MyMenuSelection.AnchorItems[i].X);
                    TweeningOriginPosIn.Add(-100 * (i + 1));
                    TweeningOriginPosOut.Add(MyMenuSelection.AnchorItems[i].X);
                    TweeningTargetPosOut.Add(WindowDimension.GameWindowWidth + (100 * ((3 - i) + 1)));
                }
            }
            else { throw new Exception("Missing Data Error - SelectionItems"); }
            #endregion
        }
        else { throw new Exception("Loading Error - MyMenuSelection"); }
        
        #region Manage the Credits
        if (MyMenuCredits != null)
        {
            CreditsTitle = "The Credits";

            // Load fonts
            CreditsFontTitle = StandardFontTitle;
            CreditsFontLines = StandardFontLines;

            // measure size text
            Vector2 sizeCreditsTitle = CreditsFontTitle.MeasureString(CreditsTitle);
            Vector2 sizeCreditsLine = new Vector2();
            if (MyMenuCredits.Count != 0)
                sizeCreditsLine = CreditsFontLines.MeasureString(MyMenuCredits[0].Assets);

            // manage the centered title
            float tempNewXCreditsTitle = (WindowDimension.GameWindowWidth - sizeCreditsTitle.X) / 2;
            CreditsTitlePosition = new Vector2(tempNewXCreditsTitle, WindowDimension.GameWindowHeight / 12);

            // manage the positions of the credits
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
        if (MyMenuInstructions != null)
        {
            InstructionsTitle = "The Instructions";

            // Load fonts
            InstructionsFontTitle = StandardFontTitle;
            InstructionsFontLines = StandardFontLines;

            // measure size text
            Vector2 sizeInstructionsTitle = InstructionsFontTitle.MeasureString(InstructionsTitle);
            Vector2 sizeInstructionsLine = new Vector2();
            if (MyMenuInstructions.Count != 0)
                sizeInstructionsLine = InstructionsFontLines.MeasureString(MyMenuInstructions[0].Action);

            // manage the centered title
            float tempNewXInstructionsTitle = (WindowDimension.GameWindowWidth - sizeInstructionsTitle.X) / 2;
            InstructionsTitlePosition = new Vector2(tempNewXInstructionsTitle, WindowDimension.GameWindowHeight / 12);

            // manage the positions of the instructions
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

        // Initialize back arrow data
        BackArrowPic = Main.GlobalContent.Load<Texture2D>("backArrow");
        BackArrowTarget = new Rectangle(5, 5, 32, 32);
        BackArrowTextPos = new Vector2(50, 5);
        BackArrowText = "Esc";

        // initialize the tweening
        MyTweening = new Tweening(InitTime, InitDuration);
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
                SoundMoveSelect.Play(SoundVolumeEffects, 0.0f, 0.0f);
                MyMenuSelection.ItemSelected += 1;
            }
            if (NewState.IsKeyDown(Keys.Up) && !OldState.IsKeyDown(Keys.Up))
            {
                SoundMoveSelect.Play(SoundVolumeEffects, 0.0f, 0.0f);
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
                SoundValidateSelect.Play(SoundVolumeEffects, 0.0f, 0.0f);
                
                switch (MyMenuSelection.SelectionItems[MyMenuSelection.ItemSelected].Item1)
                {
                    case LoadMenuData.EnumMenuItem.NewGame:
                        // initialize tweening parameters
                        DirectionMenu = Tweening.DirectionObject.Out;
                        IsMenuStable = false;
                        MyTweening.InitializeTweening(InitTime, InitDuration);
                        TargetState = Main.EnumMainState.GamePlayable; // or GameAnimation maybe
                        break;
                    case LoadMenuData.EnumMenuItem.Instructions:
                        // initialize tweening parameters
                        DirectionMenu = Tweening.DirectionObject.Out;
                        IsMenuStable = false;
                        MyTweening.InitializeTweening(InitTime, InitDuration);
                        TargetState = Main.EnumMainState.MenuInstructions;
                        break;
                    case LoadMenuData.EnumMenuItem.Credits:
                        // initialize tweening parameters
                        DirectionMenu = Tweening.DirectionObject.Out;
                        IsMenuStable = false;
                        MyTweening.InitializeTweening(InitTime, InitDuration);
                        TargetState = Main.EnumMainState.MenuCredits;
                        break;
                    case LoadMenuData.EnumMenuItem.Quit:
                        // wait for the sound to end then quit
                        System.Threading.Thread.Sleep(SoundValidateSelect.Duration);
                        pMyState = Main.EnumMainState.MenuQuit;
                        break;
                    default:
                        break;
                }
            }
            #endregion

            OldState = NewState;
        }
        else
        {   
            // call the tweening effect
            pMyState = TweeningSelectionLines(pGameTime, pMyState, TargetState);
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
            SoundHeadBack.Play(SoundVolumeEffects, 0.0f, 0.0f);
            // initialize the tweening parameters
            MyTweening.InitializeTweening(InitTime, InitDuration);
            IsMenuStable = false;
            DirectionMenu = Tweening.DirectionObject.In;
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
            SoundHeadBack.Play(SoundVolumeEffects, 0.0f, 0.0f);
            // initialize the tweening parameters
            MyTweening.InitializeTweening(InitTime, InitDuration);
            IsMenuStable = false;
            DirectionMenu = Tweening.DirectionObject.In;
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
        Main.GlobalSpriteBatch.Draw(BackArrowPic, BackArrowTarget, Color.White);
        Main.GlobalSpriteBatch.DrawString(InstructionsFontTitle, BackArrowText, BackArrowTextPos, Color.White);
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
        Main.GlobalSpriteBatch.Draw(BackArrowPic, BackArrowTarget, Color.White);
        Main.GlobalSpriteBatch.DrawString(CreditsFontTitle, BackArrowText, BackArrowTextPos, Color.White);
    }
    #endregion

    #region Methode to do the tweening on the selection lines In and Out
    private Main.EnumMainState TweeningSelectionLines(GameTime pGameTime,
                                                      Main.EnumMainState pMyState,
                                                      Main.EnumMainState pTargetState)
    {
        Main.EnumMainState temp = pMyState;

        if (MyTweening.Time < MyTweening.Duration)
            MyTweening.Time = MyTweening.Time + pGameTime.ElapsedGameTime.TotalSeconds;
        else
        {
            IsMenuStable = true;
            if (DirectionMenu == Tweening.DirectionObject.Out)
            {
                temp = pTargetState;
            }
            DirectionMenu = Tweening.DirectionObject.None;
        }

        if (MyMenuSelection != null && MyMenuSelection.SelectionItems != null)
        {
            for (int i = 0; i < MyMenuSelection.SelectionItems.Count; i++)
            {
                switch (DirectionMenu)
                {
                    case Tweening.DirectionObject.In:
                        MyMenuSelection.AnchorItems[i] =
                            new Vector2(MyTweening.EaseOutSin(MyTweening.Time,
                                                              TweeningOriginPosIn[i],
                                                              TweeningTargetPosIn[i] - TweeningOriginPosIn[i],
                                                              MyTweening.Duration),
                                        MyMenuSelection.AnchorItems[i].Y);
                        break;
                    case Tweening.DirectionObject.Out:
                        MyMenuSelection.AnchorItems[i] =
                            new Vector2(MyTweening.EaseInSin(MyTweening.Time,
                                                             TweeningOriginPosOut[i],
                                                             TweeningTargetPosOut[i] - TweeningOriginPosOut[i],
                                                             MyTweening.Duration),
                                        MyMenuSelection.AnchorItems[i].Y);
                        break;
                    case Tweening.DirectionObject.None:
                    default:
                        break;
                }
            }
        }
        return temp;
    }
    #endregion
}
