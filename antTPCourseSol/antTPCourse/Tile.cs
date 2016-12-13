using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antTPCourse
{
    class Tile
    {
        internal int id;
        internal int coordX;
        internal int coordY;
        internal string stuff; // nothing, food, branch
        internal int stuffQty;
        internal int pheroRate;

        internal Tile(int pId, int pX, int pY, string pStuff, int pStuffQty, int pPhero)
        {
            id = pId;
            coordX = pX;
            coordY = pY;
            stuff = pStuff;
            stuffQty = pStuffQty;
            pheroRate = pPhero;
        }

        //manage the evaporation of the pheromon
        internal void Evaporation()
        {
            if (pheroRate > 0)
            {
                pheroRate = pheroRate - 5;
            }
            else
            {
                pheroRate = 0;
            }
        }
    }
}
