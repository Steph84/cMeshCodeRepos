using blockMenu.UtilFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace blockMenu.MenuFolder
{
    public class Menu
    {
        public int GameWindowWidth { get; private set; }
        public int GameWindowHeight { get; private set; }

        ContentManager Content;
        SpriteBatch SpriteBatch;
        
        LoadMenuData.MenuData MyMenuData;
        List<LoadMenuData.TitleProperties> MyMenuTitles;
        LoadMenuData.MenuSelection MyMenuSelection;
        List<LoadMenuData.CreditsProperties> MyMenuCredits;

        Color tempColor;

        // keyboard stuff
        //KeyBoardManager MyKeyBoardManager = new KeyBoardManager();
        KeyboardState oldState = new KeyboardState();
        KeyboardState newState = new KeyboardState();

        // sound stuff
        SoundEffect soundHeadBack, soundMoveSelect, soundValidateSelect;
        float volumeSoundEffects;

        // Credits stuff
        string CreditsTitle;
        Vector2 CreditsTitlePosition;
        SpriteFont CreditsFontTitle, CreditsFontLines;

        // backArrow stuff
        Texture2D backArrowPic;
        Rectangle backArrowTarget;
        Vector2 backArrowTextPos;
        string backArrowText;

        // prepare the tweening
        Tweening MyTweening;
        bool menuIn = true; // the selection items arrive
        bool menuOut = false; // for the selection items to go out
        bool bMenuStable = false; // at the beginning, the menu is not usable
        Main.EnumMainState TargetState = Main.EnumMainState.MenuTitle; // target state for the tweening

        // List of positions for each selection item
        List<float> tweeningTargetPosIn;
        List<float> tweeningOriginPosIn;
        List<float> tweeningTargetPosOut;
        List<float> tweeningOriginPosOut;

        #region Constructor Menu
        public Menu(Tuple<int, int> pGameWindowSize, ContentManager pContent, SpriteBatch pSpriteBatch)
        {
            GameWindowWidth = pGameWindowSize.Item1;
            GameWindowHeight = pGameWindowSize.Item2;
            SpriteBatch = pSpriteBatch;
            Content = pContent;

            LoadMenuData LoadMenuData = new LoadMenuData();
            PersonnalColors PersonnalColors = new PersonnalColors();
            TextAlignment TextAlignment = new TextAlignment(GameWindowWidth, GameWindowHeight);

            MyTweening = new Tweening();
            InitializeTweening();

            MyMenuData = LoadMenuData.LoadJsonData();
            MyMenuTitles = MyMenuData.ListeMenuTitles;
            MyMenuSelection = MyMenuData.MenuSelection;
            MyMenuCredits = MyMenuData.Credits;
            
            soundMoveSelect = Content.Load<SoundEffect>("moveSelect");
            soundValidateSelect = Content.Load<SoundEffect>("validateSelect");
            soundHeadBack = Content.Load<SoundEffect>("headBack");
            volumeSoundEffects = 0.25f;
            
            #region Manage the titles on the main screen
            CreditsTitle = "The Credits";
            foreach (LoadMenuData.TitleProperties item in MyMenuTitles)
            {
                // Load the Font
                item.Font = Content.Load<SpriteFont>(item.FontFileName);

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
            MyMenuSelection.Font = Content.Load<SpriteFont>(MyMenuSelection.FontFileName);

            // Manage the position of each selection item
            tweeningOriginPosIn = new List<float>();
            tweeningTargetPosIn = new List<float>();
            tweeningOriginPosOut = new List<float>();
            tweeningTargetPosOut = new List<float>();

            for (int i = 0; i < MyMenuSelection.SelectionItems.Count; i++)
            {
                // determine positions for the selection lines
                float availableSpaceCenter = (GameWindowWidth - MyMenuSelection.AnchorItems[i].X);
                Vector2 sizeCenter = MyMenuSelection.Font.MeasureString(MyMenuSelection.SelectionItems[i]);
                
                MyMenuSelection.AnchorItems[i] =
                    new Vector2((availableSpaceCenter - sizeCenter.X) / 2,
                                (MyMenuSelection.AnchorPosition.Y / 12) * GameWindowHeight + (i - 1) * sizeCenter.Y);

                // determine positions for the tweening
                tweeningTargetPosIn.Add(MyMenuSelection.AnchorItems[i].X);
                tweeningOriginPosIn.Add(-100 * (i + 1));
                tweeningOriginPosOut.Add(MyMenuSelection.AnchorItems[i].X);
                tweeningTargetPosOut.Add(GameWindowWidth + (100 * ((3 - i) + 1)));
            }
            #endregion

            #region Manage the Credits

            backArrowPic = Content.Load<Texture2D>("backArrow");
            backArrowTarget = new Rectangle(5, 5, 32, 32);
            backArrowTextPos = new Vector2(50, 5);
            backArrowText = "Esc";

            // Load fonts
            CreditsFontTitle = Content.Load<SpriteFont>("TimesNewRoman24");
            CreditsFontLines = Content.Load<SpriteFont>("TimesNewRoman12");

            // measure size text
            Vector2 sizeCreditsTitle = CreditsFontTitle.MeasureString(CreditsTitle);
            Vector2 sizeCreditsLine = CreditsFontLines.MeasureString(MyMenuCredits[0].Assets);
            
            // manage the centered title
            float tempNewXCreditsTitle = (GameWindowWidth - sizeCreditsTitle.X) / 2;
            CreditsTitlePosition = new Vector2(tempNewXCreditsTitle, GameWindowHeight/12);

            // manage the positions of the credits
            for (int i = 0; i < MyMenuCredits.Count; i++)
            {
                var credit = MyMenuCredits[i];
                for (int j = 0; j < credit.AnchorPosition.Count; j++)
                {
                    var anchor = credit.AnchorPosition[j];
                    MyMenuCredits[i].AnchorPosition[j] =
                        new Vector2(GameWindowWidth/12, sizeCreditsTitle.Y * 3 // anchor of the whole credits
                                                        + j * sizeCreditsLine.Y // anchor of each lines
                                                        + i * sizeCreditsLine.Y * 4); // anchor of each block
                }
            }
            #endregion
        }
        #endregion

        #region MenuTitleUpdate
        public Main.EnumMainState MenuTitleUpdate(GameTime pGameTime, Main.EnumMainState pMyState)
        {
            if (bMenuStable == true)
            {
                newState = Keyboard.GetState();

                #region Manage the move through the selection menu

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
                    { // wait for the sound to end then quit
                        System.Threading.Thread.Sleep(soundValidateSelect.Duration);
                        pMyState = Main.EnumMainState.MenuQuit;
                    }

                    if (MyMenuSelection.SelectionItems[MyMenuSelection.ItemSelected] == "Credits")
                    { // initialize tweening parameters
                        menuOut = true;
                        bMenuStable = false;
                        InitializeTweening();
                        TargetState = Main.EnumMainState.MenuCredits;
                    }

                    if (MyMenuSelection.SelectionItems[MyMenuSelection.ItemSelected] == "New game")
                    { // initialize tweening parameters
                        menuOut = true;
                        bMenuStable = false;
                        InitializeTweening();
                        TargetState = Main.EnumMainState.GamePlayable; // or GameAnimation maybe
                    }
                        
                }
                #endregion

                oldState = newState;
            }
            else
            { // call the tweening effect
                pMyState = TweeningSelectionLines(pGameTime, menuIn, menuOut, pMyState, TargetState);
            }

            return pMyState;
        }
        #endregion

        #region MenuCreditsUpdate
        public Main.EnumMainState MenuCreditsUpdate(GameTime pGameTime, Main.EnumMainState pMyState)
        {
            newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.Escape) && !oldState.IsKeyDown(Keys.Escape))
            {
                soundHeadBack.Play(volumeSoundEffects, 0.0f, 0.0f);
                // initialize the tweening parameters
                InitializeTweening();
                bMenuStable = false;
                menuIn = true;
                pMyState = Main.EnumMainState.MenuTitle;
            }

            oldState = newState;

            return pMyState;
        }
        #endregion

        #region MenuTitleDraw
        public void MenuTitleDraw(GameTime pGameTime)
        {
            #region Draw the Titles of the main menu
            foreach (LoadMenuData.TitleProperties item in MyMenuTitles)
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
            //SpriteBatch.DrawString(MyMenuSelection.Font, MyMenuSelection.SelectionItems[0], tweeningPos, tempColor);
            #endregion
        }
        #endregion

        #region MenuCreditsDraw
        public void MenuCreditsDraw(GameTime pGameTime)
        {
            // Draw Credits title
            SpriteBatch.DrawString(CreditsFontTitle, CreditsTitle, CreditsTitlePosition, Color.White);

            // Draw Credits themselves
            foreach (var credit in MyMenuCredits)
            {
                SpriteBatch.DrawString(CreditsFontLines, credit.Assets, credit.AnchorPosition[0], Color.White);
                SpriteBatch.DrawString(CreditsFontLines, credit.Name, credit.AnchorPosition[1], Color.White);
                SpriteBatch.DrawString(CreditsFontLines, credit.Source, credit.AnchorPosition[2], Color.White);
            }

            // Draw the BackArrow pic
            SpriteBatch.Draw(backArrowPic, backArrowTarget, Color.White);
            SpriteBatch.DrawString(CreditsFontTitle, backArrowText, backArrowTextPos, Color.White);
        }
        #endregion

        #region Methode to do the tweening on the selection lines In and Out
        private Main.EnumMainState TweeningSelectionLines(GameTime pGameTime, bool pMenuIn, bool pMenuOut,
                                                          Main.EnumMainState pMyState, Main.EnumMainState pTargetState)
        {
            Main.EnumMainState temp = pMyState;
            
            if (MyTweening.Time < MyTweening.Duration)
                MyTweening.Time = MyTweening.Time + pGameTime.ElapsedGameTime.TotalSeconds;
            else
            {
                bMenuStable = true;
                if(pMenuIn)
                    menuIn = false;
                if (pMenuOut)
                {
                    menuOut = false;
                    temp = pTargetState;
                }
            }

            for (int i = 0; i < MyMenuSelection.SelectionItems.Count; i++)
            {
                if (pMenuIn)
                {
                    MyMenuSelection.AnchorItems[i] =
                                        new Vector2(MyTweening.EaseOutSin(MyTweening.Time,
                                                                          tweeningOriginPosIn[i],
                                                                          tweeningTargetPosIn[i] - tweeningOriginPosIn[i],
                                                                          MyTweening.Duration),
                                                    MyMenuSelection.AnchorItems[i].Y);
                }

                if (pMenuOut)
                {
                    MyMenuSelection.AnchorItems[i] =
                                        new Vector2(MyTweening.EaseInSin(MyTweening.Time,
                                                                          tweeningOriginPosOut[i],
                                                                          tweeningTargetPosOut[i] - tweeningOriginPosOut[i],
                                                                          MyTweening.Duration),
                                                    MyMenuSelection.AnchorItems[i].Y);
                }
            }
            return temp;
        }
        #endregion

        #region Method to initialize parameters of tweening
        private void InitializeTweening()
        {
            double initializeTime = 0;
            double initializeDuration = 1.5;

            MyTweening.Time = initializeTime;
            MyTweening.Duration = initializeDuration;
        }
        #endregion
    }
}