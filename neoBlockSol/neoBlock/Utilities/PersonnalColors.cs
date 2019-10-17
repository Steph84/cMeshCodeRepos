using System;

public class PersonnalColors
{
    public enum EnumColorName
    {
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
        Vermilion = 12,
        White = 13
    };

    #region RGBA values
    // Array of the RGBA values foreach Color
    public static Tuple<int, int, int, int>[] ArrayColors = new Tuple<int, int, int, int>[12] {
                                            new Tuple<int, int, int, int>(255, 0, 0, 255),
                                            new Tuple<int, int, int, int>(192, 0, 64, 255),
                                            new Tuple<int, int, int, int>(128, 0, 128, 255),
                                            new Tuple<int, int, int, int>(64, 0, 192, 255),
                                            new Tuple<int, int, int, int>(0, 0, 255, 255),
                                            new Tuple<int, int, int, int>(0, 64, 128, 255),
                                            new Tuple<int, int, int, int>(0, 128, 0, 255),
                                            new Tuple<int, int, int, int>(128, 192, 0, 255),
                                            new Tuple<int, int, int, int>(255, 255, 0, 255),
                                            new Tuple<int, int, int, int>(255, 192, 0, 255),
                                            new Tuple<int, int, int, int>(255, 128, 0, 255),
                                            new Tuple<int, int, int, int>(255, 64, 0, 255) };
    #endregion

    #region Method to set color in relation to the enum
    // Method to get the RGBA values from the EnumColor
    public static Tuple<int, int, int, int> SetPersonnalColor(EnumColorName pColorName)
    {
        Tuple<int, int, int, int> ColorToReturn = new Tuple<int, int, int, int>(255, 255, 255, 255);

        switch (pColorName)
        {
            case EnumColorName.Red:
                ColorToReturn = (ArrayColors[(int)EnumColorName.Red - 1]);
                break;
            case EnumColorName.Cinnabar:
                ColorToReturn = (ArrayColors[(int)EnumColorName.Cinnabar - 1]);
                break;
            case EnumColorName.Purple:
                ColorToReturn = (ArrayColors[(int)EnumColorName.Purple - 1]);
                break;
            case EnumColorName.Violet:
                ColorToReturn = (ArrayColors[(int)EnumColorName.Violet - 1]);
                break;
            case EnumColorName.Blue:
                ColorToReturn = (ArrayColors[(int)EnumColorName.Blue - 1]);
                break;
            case EnumColorName.Teal:
                ColorToReturn = (ArrayColors[(int)EnumColorName.Teal - 1]);
                break;
            case EnumColorName.Green:
                ColorToReturn = (ArrayColors[(int)EnumColorName.Green - 1]);
                break;
            case EnumColorName.Chartreuse:
                ColorToReturn = (ArrayColors[(int)EnumColorName.Chartreuse - 1]);
                break;
            case EnumColorName.Yellow:
                ColorToReturn = (ArrayColors[(int)EnumColorName.Yellow - 1]);
                break;
            case EnumColorName.Amber:
                ColorToReturn = (ArrayColors[(int)EnumColorName.Amber - 1]);
                break;
            case EnumColorName.Orange:
                ColorToReturn = (ArrayColors[(int)EnumColorName.Orange - 1]);
                break;
            case EnumColorName.Vermilion:
                ColorToReturn = (ArrayColors[(int)EnumColorName.Vermilion - 1]);
                break;
            case EnumColorName.White:
                break;
            default:
                break;
        }
        return ColorToReturn;
    }
    #endregion
}