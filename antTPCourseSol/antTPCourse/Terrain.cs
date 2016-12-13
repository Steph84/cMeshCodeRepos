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
        internal int nbColumns;
        internal int nbLines;


        internal Terrain(int pWidth, int pHeight, int pTileSize)
        {
            mapWidth = pWidth;
            mapHeight = pHeight;
            tileSize = pTileSize;

            nbColumns = mapWidth/tileSize;
            nbLines = mapHeight/tileSize;

            int i, j, tempId ;

            Tile[,] floor = new Tile[nbColumns, nbLines]; // creation of the array of Tile objects

            // initialization of each Tile
            for (i = 0; i < nbColumns; i++)
            {
                for(j = 0; j < nbLines; j++)
                {
                    tempId = Convert.ToInt32(string.Format("{0}{1}", i, j)); // concatenate 2 numbers for the id .NET-ish
                    floor[i, j] = new Tile(tempId, i, j, "NOTHING", 0, 0);
                }
            }


        }

    }
}
