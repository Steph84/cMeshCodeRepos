using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace blockMenu
{
    public class TextAlignment
    {
        public int GameWindowWidth { get; private set; }
        public int GameWindowHeight { get; private set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum EnumLineAlignment
        {
            Left = 1,
            Center = 2,
            Right = 3
        };
        
        public TextAlignment(int pGameWindowWidth, int pGameWindowHeight)
        {
            GameWindowWidth = pGameWindowWidth;
            GameWindowHeight = pGameWindowHeight;
        }

        #region Method to apply horizontal alignment
        public void ApplyHorizontalAlignment(LoadMenuData.TitleProperties pItem)
        {
            switch (pItem.Alignment)
            {
                case TextAlignment.EnumLineAlignment.Left:
                    pItem.AnchorPosition = new Vector2(GameWindowWidth * (1 - pItem.WidthLimit), pItem.AnchorPosition.Y);
                    break;
                case TextAlignment.EnumLineAlignment.Center:
                    float availableSpaceCenter = (GameWindowWidth - pItem.AnchorPosition.X);
                    Vector2 sizeCenter = pItem.Font.MeasureString(pItem.Value);
                    pItem.AnchorPosition = new Vector2((availableSpaceCenter - sizeCenter.X) / 2, pItem.AnchorPosition.Y);
                    break;
                case TextAlignment.EnumLineAlignment.Right:
                    float availableSpaceRight = (GameWindowWidth - pItem.AnchorPosition.X) * pItem.WidthLimit;
                    Vector2 sizeRight = pItem.Font.MeasureString(pItem.Value);
                    pItem.AnchorPosition = new Vector2((availableSpaceRight - sizeRight.X), pItem.AnchorPosition.Y);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Method to apply vertical alignment
        public void ApplyVerticalAlignment(LoadMenuData.TitleProperties pItem)
        {
            pItem.AnchorPosition = new Vector2(pItem.AnchorPosition.X, (pItem.AnchorPosition.Y / 12) * GameWindowHeight);
        }
        #endregion
    }
}