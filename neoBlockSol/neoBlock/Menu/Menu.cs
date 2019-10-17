using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

public class Menu
{
    #region Constructor Menu
    public Menu()
    {
        LoadMenuData.MenuData MyMenuData;
        List<LoadMenuData.TitleProperties> MyMenuTitles;
        LoadMenuData.MenuSelection MyMenuSelection;
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

        StandardFontTitle = Main.content.Load<SpriteFont>("TimesNewRoman24");
        StandardFontLines = Main.content.Load<SpriteFont>("TimesNewRoman12");

        #region Manage the titles on the main screen
        //CreditsTitle = "The Credits";
        if (MyMenuTitles != null)
        {
            foreach (LoadMenuData.TitleProperties item in MyMenuTitles)
            {
                // Load the Font
                item.Font = Main.content.Load<SpriteFont>(item.FontFileName);

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
                MyMenuSelection.Font = Main.content.Load<SpriteFont>(MyMenuSelection.FontFileName);
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
}
