using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antTPCourse
{
    class Terrain
    {
        internal int mapWidth;
        internal int mapHeight;
        internal int tileSize;

        internal Terrain(int pWidth, int pHeight, int pTileSize)
        {
            mapWidth = pWidth;
            mapHeight = pHeight;
            tileSize = pTileSize;
        }

    }
}
