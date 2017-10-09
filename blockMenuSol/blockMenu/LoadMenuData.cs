using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.IO;


namespace blockMenu
{
    public class LoadMenuData
    {
        public class MenuDto
        {
            public LineProperties MaintTitle { get; set; }
            public LineProperties SubTitle { get; set; }
            public List<LineProperties> Selection { get; set; }
            public List<LineProperties> Credits { get; set; }
            public LineProperties Version { get; set; }
        }
        
        public class LineProperties
        {
            //public SpriteFont Font { get; set; }
            public string Value { get; set; }
            public Vector2 AnchorPosition { get; set; }
            //public Color Color { get; set; }
            public LineAlignment Alignment { get; set; }
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum LineAlignment
        {
            Left = 1,
            Center = 2,
            Right = 3
        };

        // TODO enum for colors
        
        public MenuDto LoadJson()
        {
            MenuDto temp = new MenuDto();

            string filename = "../../../../json1.json";

            using (StreamReader streamReader = new StreamReader(filename))
            {
                string json = streamReader.ReadToEnd();
                temp = JsonConvert.DeserializeObject<MenuDto>(json);
            }
            

            return temp;

        }
    }
}

//public void LoadJson()
//{
//    using (StreamReader r = new StreamReader("file.json"))
//    {
//        string json = r.ReadToEnd();
//        List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);
//    }
//}

//public class Item
//{
//    public int millis;
//    public string stamp;
//    public DateTime datetime;
//    public string light;
//    public float temp;
//    public float vcc;
//}