using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
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
            public LineProperties MaintTitle { get; }
            public LineProperties SubTitle { get; }
            public List<LineProperties> Selection { get; }
            public List<LineProperties> Credits { get; }
            public LineProperties Version { get; }
        }
        
        public class LineProperties
        {
            public SpriteFont Font { get; }
            public string Value { get; }
            public Vector2 AnchorPosition { get; set; }
            public Color Color { get; set; }
            public LineAlignment Alignment { get; }
        }

        public enum LineAlignment
        {
            Left = 1,
            Center = 2,
            Right = 3
        }

        // TODO enum for colors




        public int[,] LoadLevelData(string filename)
        {
            using (var streamReader = new StreamReader(filename))
            {
                var serializer = new JsonSerializer();
                return (int[,])serializer.Deserialize(streamReader, typeof(int[,]));
            }
        }
    }
}
