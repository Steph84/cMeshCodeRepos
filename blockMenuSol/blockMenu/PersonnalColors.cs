using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace blockMenu
{
    public class PersonnalColors
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ColorName
        {
            Red = 1,
            Cinnabar = 2,
            Purple = 3,
            Violet = 4,
            Blue = 5,
            Teal = 6,
            Green = 7,
            Chartreuse = 8,
            Yellow = 9,
            Amber = 10,
            Orange = 11,
            Vermilion = 12,
        };
    }
}
