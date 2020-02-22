﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

public class PersonnalColors
{
    public enum EnumColorName
    {
        White = 0,
        Red = 1,
        Cinnabar = 2,
        Purple = 3,
        Violet = 4,
        Blue = 5,
        Teal = 6,
        Green = 7,
        Chartreuse = 8,
        Yellow = 9,
        Amber = 10,
        Orange = 11,
        Vermilion = 12
    };

    #region RGBA values
    // List of the RGBA values foreach Color
    public static List<Vector4> ListColors = new List<Vector4> {
                                            new Vector4(255, 0, 0, 255),
                                            new Vector4(192, 0, 64, 255),
                                            new Vector4(128, 0, 128, 255),
                                            new Vector4(64, 0, 192, 255),
                                            new Vector4(0, 0, 255, 255),
                                            new Vector4(0, 64, 128, 255),
                                            new Vector4(0, 128, 0, 255),
                                            new Vector4(128, 192, 0, 255),
                                            new Vector4(255, 255, 0, 255),
                                            new Vector4(255, 192, 0, 255),
                                            new Vector4(255, 128, 0, 255),
                                            new Vector4(255, 64, 0, 255)
    };
    public static List<Tuple<EnumColorName, Vector4>> ListTupleColorNameRGB = new List<Tuple<EnumColorName, Vector4>>
    {
        new Tuple<EnumColorName, Vector4>(EnumColorName.White, new Vector4(255, 255, 255, 255)),
        new Tuple<EnumColorName, Vector4>(EnumColorName.Red, new Vector4(255, 0, 0, 255)),
        new Tuple<EnumColorName, Vector4>(EnumColorName.Cinnabar, new Vector4(255, 0, 0, 255)),
        new Tuple<EnumColorName, Vector4>(EnumColorName.Purple, new Vector4(255, 0, 0, 255)),
        new Tuple<EnumColorName, Vector4>(EnumColorName.Violet, new Vector4(255, 0, 0, 255)),
        new Tuple<EnumColorName, Vector4>(EnumColorName.Blue, new Vector4(255, 0, 0, 255)),
        new Tuple<EnumColorName, Vector4>(EnumColorName.Teal, new Vector4(255, 0, 0, 255)),
        new Tuple<EnumColorName, Vector4>(EnumColorName.Green, new Vector4(255, 0, 0, 255)),
        new Tuple<EnumColorName, Vector4>(EnumColorName.Chartreuse, new Vector4(255, 0, 0, 255)),
        new Tuple<EnumColorName, Vector4>(EnumColorName.Yellow, new Vector4(255, 0, 0, 255)),
        new Tuple<EnumColorName, Vector4>(EnumColorName.Amber, new Vector4(255, 0, 0, 255)),
        new Tuple<EnumColorName, Vector4>(EnumColorName.Orange, new Vector4(255, 0, 0, 255)),
        new Tuple<EnumColorName, Vector4>(EnumColorName.Vermilion, new Vector4(255, 0, 0, 255))
    };
    #endregion

    #region Method to get color in relation to the enum
    // Method to get the RGBA values from the EnumColor
    public static Vector4 GetRGBFromColorName(EnumColorName pColorName)
    {
        Vector4 ColorToReturn = new Vector4(255, 255, 255, 255);

        switch (pColorName)
        {
            case EnumColorName.Red:
                ColorToReturn = (ListColors[(int)EnumColorName.Red - 1]);
                break;
            case EnumColorName.Cinnabar:
                ColorToReturn = (ListColors[(int)EnumColorName.Cinnabar - 1]);
                break;
            case EnumColorName.Purple:
                ColorToReturn = (ListColors[(int)EnumColorName.Purple - 1]);
                break;
            case EnumColorName.Violet:
                ColorToReturn = (ListColors[(int)EnumColorName.Violet - 1]);
                break;
            case EnumColorName.Blue:
                ColorToReturn = (ListColors[(int)EnumColorName.Blue - 1]);
                break;
            case EnumColorName.Teal:
                ColorToReturn = (ListColors[(int)EnumColorName.Teal - 1]);
                break;
            case EnumColorName.Green:
                ColorToReturn = (ListColors[(int)EnumColorName.Green - 1]);
                break;
            case EnumColorName.Chartreuse:
                ColorToReturn = (ListColors[(int)EnumColorName.Chartreuse - 1]);
                break;
            case EnumColorName.Yellow:
                ColorToReturn = (ListColors[(int)EnumColorName.Yellow - 1]);
                break;
            case EnumColorName.Amber:
                ColorToReturn = (ListColors[(int)EnumColorName.Amber - 1]);
                break;
            case EnumColorName.Orange:
                ColorToReturn = (ListColors[(int)EnumColorName.Orange - 1]);
                break;
            case EnumColorName.Vermilion:
                ColorToReturn = (ListColors[(int)EnumColorName.Vermilion - 1]);
                break;
            case EnumColorName.White:
            default:
                break;
        }
        return ColorToReturn;
    }

    // Method to get the EnumColor values from the RGB values
    public static EnumColorName GetColorNameFromRGB(Vector4 pRGBValues)
    {
        EnumColorName ColorToReturn = EnumColorName.White;

        Vector4 colorIndex = ListColors
            .Where(x => x.X == pRGBValues.X & x.Y == pRGBValues.Y & x.Z == pRGBValues.Z)
            .Select(x => x.)
            .FirstOrDefault()
            .;

        switch (pColorName)
        {
            case EnumColorName.Red:
                ColorToReturn = (ListColors[(int)EnumColorName.Red - 1]);
                break;
            case EnumColorName.Cinnabar:
                ColorToReturn = (ListColors[(int)EnumColorName.Cinnabar - 1]);
                break;
            case EnumColorName.Purple:
                ColorToReturn = (ListColors[(int)EnumColorName.Purple - 1]);
                break;
            case EnumColorName.Violet:
                ColorToReturn = (ListColors[(int)EnumColorName.Violet - 1]);
                break;
            case EnumColorName.Blue:
                ColorToReturn = (ListColors[(int)EnumColorName.Blue - 1]);
                break;
            case EnumColorName.Teal:
                ColorToReturn = (ListColors[(int)EnumColorName.Teal - 1]);
                break;
            case EnumColorName.Green:
                ColorToReturn = (ListColors[(int)EnumColorName.Green - 1]);
                break;
            case EnumColorName.Chartreuse:
                ColorToReturn = (ListColors[(int)EnumColorName.Chartreuse - 1]);
                break;
            case EnumColorName.Yellow:
                ColorToReturn = (ListColors[(int)EnumColorName.Yellow - 1]);
                break;
            case EnumColorName.Amber:
                ColorToReturn = (ListColors[(int)EnumColorName.Amber - 1]);
                break;
            case EnumColorName.Orange:
                ColorToReturn = (ListColors[(int)EnumColorName.Orange - 1]);
                break;
            case EnumColorName.Vermilion:
                ColorToReturn = (ListColors[(int)EnumColorName.Vermilion - 1]);
                break;
            case EnumColorName.White:
            default:
                break;
        }
        return ColorToReturn;
    }
    #endregion
}