using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockMenu
{
    public class TextAlignment
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum EnumLineAlignment
        {
            Left = 1,
            Center = 2,
            Right = 3
        };


    }
}
