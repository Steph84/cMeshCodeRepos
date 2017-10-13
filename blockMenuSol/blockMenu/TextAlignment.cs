using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace blockMenu
{
    public class TextAlignment
    {
        public int GameWindowWidth { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum EnumLineAlignment
        {
            Left = 1,
            Center = 2,
            Right = 3
        };
        
        public TextAlignment(int pGameWindowWidth)
        {
            GameWindowWidth = pGameWindowWidth;
        }

        #region Method to apply alignment
        public void ApplyAlignment(LoadMenuData.LineProperties pItem)
        {
            switch (pItem.Alignment)
            {
                case TextAlignment.EnumLineAlignment.Left:
                    float tempNewXLeft = GameWindowWidth * (1 - pItem.WidthLimit);
                    float tempOldYLeft = pItem.AnchorPosition.Y;
                    pItem.AnchorPosition = new Vector2(tempNewXLeft, tempOldYLeft);
                    break;
                case TextAlignment.EnumLineAlignment.Center:
                    float availableSpaceCenter = (GameWindowWidth - pItem.AnchorPosition.X);
                    Vector2 sizeCenter = pItem.Font.MeasureString(pItem.Value);
                    float tempNewXCenter = (availableSpaceCenter - sizeCenter.X) / 2;
                    float tempOldYCenter = pItem.AnchorPosition.Y;
                    pItem.AnchorPosition = new Vector2(tempNewXCenter, tempOldYCenter);
                    break;
                case TextAlignment.EnumLineAlignment.Right:
                    float availableSpaceRight = (GameWindowWidth - pItem.AnchorPosition.X) * pItem.WidthLimit;
                    Vector2 sizeRight = pItem.Font.MeasureString(pItem.Value);
                    float tempNewXRight = (availableSpaceRight - sizeRight.X);
                    float tempOldYRight = pItem.AnchorPosition.Y;
                    pItem.AnchorPosition = new Vector2(tempNewXRight, tempOldYRight);
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
