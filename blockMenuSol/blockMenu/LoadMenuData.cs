using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
            //public SpriteFont Font { get; }
            public string Value { get; set; }
            public Vector2 AnchorPosition { get; set; }
            //public Color Color { get; set; }
            public LineAlignment Alignment { get; }
        }

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