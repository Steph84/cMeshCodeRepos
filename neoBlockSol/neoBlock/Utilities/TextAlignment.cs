using Microsoft.Xna.Framework;

public class TextAlignment
{
    public enum EnumLineAlignment
    {
        Left = 1,
        Center = 2,
        Right = 3
    };

    //public TextAlignment() { }

    #region Method to apply horizontal alignment
    public static void ApplyHorizontalAlignment(LoadMenuData.TitleProperties pItem)
    {
        switch (pItem.Alignment)
        {
            case EnumLineAlignment.Left:
                pItem.AnchorPosition = new Vector2(WindowDimension.GameWindowWidth * (1 - pItem.WidthLimit), pItem.AnchorPosition.Y);
                break;
            case EnumLineAlignment.Center:
                float availableSpaceCenter = (WindowDimension.GameWindowWidth - pItem.AnchorPosition.X);
                Vector2 sizeCenter = pItem.Font.MeasureString(pItem.Value);
                pItem.AnchorPosition = new Vector2((availableSpaceCenter - sizeCenter.X) / 2, pItem.AnchorPosition.Y);
                break;
            case EnumLineAlignment.Right:
                float availableSpaceRight = (WindowDimension.GameWindowWidth - pItem.AnchorPosition.X) * pItem.WidthLimit;
                Vector2 sizeRight = pItem.Font.MeasureString(pItem.Value);
                pItem.AnchorPosition = new Vector2((availableSpaceRight - sizeRight.X), pItem.AnchorPosition.Y);
                break;
            default:
                break;
        }
    }
    #endregion

    #region Method to apply vertical alignment
    public static void ApplyVerticalAlignment(LoadMenuData.TitleProperties pItem)
    {
        pItem.AnchorPosition = new Vector2(pItem.AnchorPosition.X, (pItem.AnchorPosition.Y / 12) * WindowDimension.GameWindowHeight);
    }
    #endregion
}