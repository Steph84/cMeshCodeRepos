using blockMapGenerator.UtilFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockMapGenerator.MapGenFolder
{
    public class MapGenerator
    {
        private Texture2D BitMapData { get; set; }
        private ContentManager Content { get; set; }
        private SpriteBatch SpriteBatch { get; set; }
        private string BitMapName { get; set; }
        private Tuple<int, int> MapSizeInTile { get; set; }
        public MapTexture[,] MapTextureGrid { get; private set; }
        public TileObject MapGrid { get; set; } 

        public MapGenerator(ContentManager pContent, SpriteBatch pSpriteBatch, string pBitMapName)
        {
            Content = pContent;
            SpriteBatch = pSpriteBatch;
            BitMapName = pBitMapName;

            ExtractDataFromBitMap();
            ExtractTileSet();
        }

        private void ExtractTileSet()
        {

        }

        private void ExtractDataFromBitMap()
        {
            // BitMap load
            BitMapData = Content.Load<Texture2D>(BitMapName);
            MapSizeInTile = new Tuple<int, int>(BitMapData.Width, BitMapData.Height);

            // extraction color of the BitMap
            Color[] rawData = new Color[MapSizeInTile.Item1 * MapSizeInTile.Item2];
            BitMapData.GetData<Color>(rawData);
            
            // creation of the texture grid
            MapTextureGrid = new MapTexture[MapSizeInTile.Item2, MapSizeInTile.Item1];
            for (int row = 0; row < MapSizeInTile.Item2; row++)
            {
                for (int column = 0; column < MapSizeInTile.Item1; column++)
                {
                    Color temp = rawData[row * MapSizeInTile.Item1 + column];

                    // if black
                    if(temp.R == 0 && temp.G == 0 && temp.B == 0)
                    {
                        MapTextureGrid[row, column] = MapTexture.Wall;
                    } // if white
                    else if (temp.R == 255 && temp.G == 255 && temp.B == 255)
                    {
                        MapTextureGrid[row, column] = MapTexture.Floor;
                    }
                }
            }
        }

        public enum MapTexture
        {
            Void = 0,
            Wall = 1,
            Floor = 2,
            Grass = 3,
            Dirt = 4,
            Water = 5
        }
        




            
        // constructor that call all the methods

        // method to extrat tileSet

        // method to read B&W map

        // method write map
    }
}
