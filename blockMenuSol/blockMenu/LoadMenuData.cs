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
            public ColorName Color { get; set; }
            public string FontFileName { get; set; }
            public SpriteFont Font { get; set; } // loaded after with the FontFileName
            public LineAlignment Alignment { get; set; }
            public float WidthLimit { get; set; } // percentage for Left and Right offset
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum LineAlignment
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