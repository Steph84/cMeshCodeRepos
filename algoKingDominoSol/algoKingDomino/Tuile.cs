using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machin
{
    public class Tuile
    {
        public int Id { get; set; } // 1 à 48
        public Side LeftSide { get; set; }
        public Side RigthSide { get; set; }
        public Rectangle Position { get; set; }
    }

    public class Side
    {
        public int CrownNb { get; set; }
        public EnumNature Nature { get; set; }
    }

    public enum EnumNature
    {
        Wheat = 1,
        Forest = 2,
        Water = 3,
        Grass = 4,
        Swamp = 5,
        Mine = 6
    }
}