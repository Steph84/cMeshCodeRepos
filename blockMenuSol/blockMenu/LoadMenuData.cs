using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace blockMenu
{
    public class LoadMenuData
    {
        #region DTO
        public class MenuData
        {
            public List<TitleProperties> ListeMenuTitles { get; set; }
            public MenuSelection MenuSelection { get; set; }
            public List<CreditsProperties> Credits { get; set; }
        }
        
        public class TitleProperties
        {
            public string ItemName { get; set; }
            public string Value { get; set; } // the title itself
            public Vector2 AnchorPosition { get; set; }
            public PersonnalColors.EnumColorName EnumColor { get; set; }
            public string FontFileName { get; set; }
            public TextAlignment.EnumLineAlignment Alignment { get; set; }
            public float WidthLimit { get; set; } // percentage for Left and Right offset
            public Color Color { get; set; } // setted after with the EnumColor
            public SpriteFont Font { get; set; } // loaded after with the FontFileName
        }

        public class MenuSelection
        {
            public List<string> SelectionItems { get; set; }
            public List<Vector2> AnchorItems { get; set; }
            public Vector2 AnchorPosition { get; set; }
            public PersonnalColors.EnumColorName EnumColor { get; set; }
            public string FontFileName { get; set; }
            public TextAlignment.EnumLineAlignment Alignment { get; set; }
            public float WidthLimit { get; set; } // percentage for Left and Right offset
            public int ItemSelected { get; set; }
            public Color Color { get; set; } // setted after with the EnumColor
            public SpriteFont Font { get; set; } // loaded after with the FontFileName
        }

        public class CreditsProperties
        {
            public List<Vector2> AnchorPosition { get; set; }
            public string Assets { get; set; }
            public string Name { get; set; }
            public string Source { get; set; }
        }
        #endregion

        #region Method to extract data and set properties from Json file
        public MenuData LoadJsonData()
        {
            MenuData temp = new MenuData();

            string filename = "../../../../menuData.json";

            using (StreamReader streamReader = new StreamReader(filename))
            {
                string json = streamReader.ReadToEnd();
                temp = JsonConvert.DeserializeObject<MenuData>(json);
            }
            return temp;
        }
        #endregion
    }
}