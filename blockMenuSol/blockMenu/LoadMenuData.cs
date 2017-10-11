using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.IO;
using static blockMenu.PersonnalColors;

namespace blockMenu
{
    public class LoadMenuData
    {
        public class MenuDto
        {
            public List<LineProperties> MenuItems { get; set; }
        }
        
        public class LineProperties
        {
            public string ItemName { get; set; }
            public string Value { get; set; } // the title itself
            public Vector2 AnchorPosition { get; set; }
            public EnumColorName EnumColor { get; set; }
            public string FontFileName { get; set; }
            public EnumLineAlignment Alignment { get; set; }
            public float WidthLimit { get; set; } // percentage for Left and Right offset

            public Color Color { get; set; } // setted after with the EnumColor
            public SpriteFont Font { get; set; } // loaded after with the FontFileName
            public List<string> SelectionItems { get; set; } // setted after if Selection
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum EnumLineAlignment
        {
            Left = 1,
            Center = 2,
            Right = 3
        };

        public MenuDto LoadJson()
        {
            MenuDto temp = new MenuDto();

            string filename = "../../../../menuData.json";

            using (StreamReader streamReader = new StreamReader(filename))
            {
                string json = streamReader.ReadToEnd();
                temp = JsonConvert.DeserializeObject<MenuDto>(json);
            }
            return temp;
        }
    }
}