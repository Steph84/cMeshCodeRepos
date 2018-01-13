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
            public List<InstructionsProperties> Instructions { get; set; }
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

        public class InstructionsProperties
        {
            public List<Vector2> AnchorPosition { get; set; }
            public string Action { get; set; }
            public string Control { get; set; }
        }
        #endregion

        #region Method to extract data and set properties from Json file
        //public MenuData LoadJsonData()
        //{
        //    MenuData temp = new MenuData();

        //    string filename = "../../../../MenuFolder/menuData.json";

        //    using (StreamReader streamReader = new StreamReader(filename))
        //    {
        //        string json = streamReader.ReadToEnd();
        //        temp = JsonConvert.DeserializeObject<MenuData>(json);
        //    }
        //    return temp;
        //}
        #endregion

        #region Method to initialize the Data
        public MenuData LoadHardData()
        {
            MenuData MenuData = new MenuData(); ;

            #region ListeMenuTitles
            MenuData.ListeMenuTitles = new List<TitleProperties>();
            MenuData.ListeMenuTitles.Add(new TitleProperties
            {
                ItemName = "MaintTitle",
                Value = "Salem",
                AnchorPosition = new Vector2(0, 1),
                Alignment = TextAlignment.EnumLineAlignment.Center,
                EnumColor = PersonnalColors.EnumColorName.Red,
                FontFileName = "Capture_it"
            });
            MenuData.ListeMenuTitles.Add(new TitleProperties
            {
                ItemName = "SubTitle",
                Value = "Story",
                AnchorPosition = new Vector2(0, 3),
                Alignment = TextAlignment.EnumLineAlignment.Center,
                EnumColor = PersonnalColors.EnumColorName.Blue,
                FontFileName = "AlexBrush",
                WidthLimit = 0.9f
            });
            MenuData.ListeMenuTitles.Add(new TitleProperties
            {
                ItemName = "Version",
                Value = "V 1.0",
                AnchorPosition = new Vector2(0, 11),
                Alignment = TextAlignment.EnumLineAlignment.Right,
                EnumColor = PersonnalColors.EnumColorName.White,
                FontFileName = "TimesNewRoman24",
                WidthLimit = 0.99f
            });
            #endregion

            #region MenuSelection
            MenuData.MenuSelection = new MenuSelection();
            MenuData.MenuSelection.SelectionItems = new List<string>
            {
                "New game",
                "Instructions",
                "Credits",
                "Quit"
            };
            MenuData.MenuSelection.AnchorItems = new List<Vector2>
            {
                new Vector2(0, 0),
                new Vector2(0, 0),
                new Vector2(0, 0),
                new Vector2(0, 0),
            };
            MenuData.MenuSelection.AnchorPosition = new Vector2(0, 7);
            MenuData.MenuSelection.Alignment = TextAlignment.EnumLineAlignment.Center;
            MenuData.MenuSelection.EnumColor = PersonnalColors.EnumColorName.White;
            MenuData.MenuSelection.FontFileName = "Pacifico";
            MenuData.MenuSelection.ItemSelected = 0;
            #endregion

            #region Credits
            MenuData.Credits = new List<CreditsProperties>();
            MenuData.Credits.Add(new CreditsProperties
            {
                AnchorPosition = new List<Vector2>
                {
                    new Vector2(0, 300),
                    new Vector2(0, 300),
                    new Vector2(0, 300),           
                },
                Assets = "Background picture",
                Name = "Alexander Ovechkin",
                Source = "http://"
            });
            MenuData.Credits.Add(new CreditsProperties
            {
                AnchorPosition = new List<Vector2>
                {
                    new Vector2(0, 300),
                    new Vector2(0, 300),
                    new Vector2(0, 300),
                },
                Assets = "Sound effect",
                Name = "Teemu Selanne",
                Source = "http://"
            });
            #endregion

            #region Instructions
            MenuData.Instructions = new List<InstructionsProperties>();
            MenuData.Instructions.Add(new InstructionsProperties
            {
                AnchorPosition = new List<Vector2>
                {
                    new Vector2(0, 300),
                    new Vector2(0, 300)
                },
                Action = "Direction",
                Control = "WASD arrow keys"
            });
            MenuData.Instructions.Add(new InstructionsProperties
            {
                AnchorPosition = new List<Vector2>
                {
                    new Vector2(0, 300),
                    new Vector2(0, 300)
                },
                Action = "Jump",
                Control = "Space key"
            });
            #endregion

            return MenuData;
        }
        #endregion
    }
}