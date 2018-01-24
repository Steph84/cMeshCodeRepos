using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace basicsTopDown.MapGenFolder
{
    public class MapGenerator
    {
        private ContentManager Content { get; set; }
        private SpriteBatch SpriteBatch { get; set; }
        private string BitMapName { get; set; }
        private Texture2D BitMapData { get; set; }
        private string TileSetName { get; set; }
        private Texture2D TileSetData { get; set; }
        private Tuple<int, int> MapSizeInTile { get; set; }
        public MapTexture[,] MapTextureGrid { get; private set; }
        public TileObject[,] MapGrid { get; set; }
        private int TileWidth { get; set; }
        private int TileHeight { get; set; }
        private double GameSizeCoefficient { get; set; }
        private bool IsSingleTileSet { get; set; }

        public MapGenerator(ContentManager pContent, SpriteBatch pSpriteBatch, string pBitMapName, string pTileSetName, int pTileWidth, int pTileHeight, double pGameSizeCoefficient)
        {
            Content = pContent;
            SpriteBatch = pSpriteBatch;
            BitMapName = pBitMapName;
            TileSetName = pTileSetName;
            TileWidth = pTileWidth;
            TileHeight = pTileHeight;
            GameSizeCoefficient = pGameSizeCoefficient;

            ExtractDataFromBitMap();
            ExtractTileSet();
            InitilizeMapGrid();
            GenerateMapGrid();
        }

        private void GenerateMapGrid()
        {
            for (int row = 0; row < MapSizeInTile.Item2; row++)
            {
                for (int column = 0; column < MapSizeInTile.Item1; column++)
                {
                    if (MapTextureGrid[row, column] == MapTexture.Wall)
                    {

                        Dictionary<int, bool> DictTextureAround =
                        new Dictionary<int, bool>() { { 1, false }, { 2, false }, { 4, false }, { 8, false } };

                        // tile top
                        if (row == 0)
                            DictTextureAround[1] = true;
                        else
                            if (MapTextureGrid[row - 1, column] == MapTexture.Wall)
                            DictTextureAround[1] = true;

                        // tile right
                        if (column == MapSizeInTile.Item1 - 1)
                            DictTextureAround[2] = true;
                        else
                            if (MapTextureGrid[row, column + 1] == MapTexture.Wall)
                            DictTextureAround[2] = true;

                        // tile bottom
                        if (row == MapSizeInTile.Item2 - 1)
                            DictTextureAround[4] = true;
                        else
                            if (MapTextureGrid[row + 1, column] == MapTexture.Wall)
                            DictTextureAround[4] = true;

                        // tile left
                        if (column == 0)
                            DictTextureAround[8] = true;
                        else
                            if (MapTextureGrid[row, column - 1] == MapTexture.Wall)
                            DictTextureAround[8] = true;

                        int tempFlag = 0;
                        foreach (KeyValuePair<int, bool> entry in DictTextureAround)
                        {
                            if (entry.Value)
                                tempFlag += entry.Key;
                        }

                        MapGrid[row, column].Flag = tempFlag;
                    }
                }
            }
        }

        private void InitilizeMapGrid()
        {
            // basic tile 32x32 but tileSet 96x96 so GameSizeCoefficient / 3
            GameSizeCoefficient = GameSizeCoefficient / 3.0d;

            // tiles size as shown on the screen
            int tileWidthShowing = (int)Math.Round(TileWidth * GameSizeCoefficient, MidpointRounding.AwayFromZero);
            int tileHeightShowing = (int)Math.Round(TileHeight * GameSizeCoefficient, MidpointRounding.AwayFromZero);

            MapGrid = new TileObject[MapSizeInTile.Item2, MapSizeInTile.Item1];
            for (int row = 0; row < MapSizeInTile.Item2; row++)
            {
                for (int column = 0; column < MapSizeInTile.Item1; column++)
                {
                    MapGrid[row, column] =
                        new TileObject(new Rectangle(column * tileWidthShowing, row * tileHeightShowing,
                                                     tileWidthShowing, tileHeightShowing),
                                       MapTextureGrid[row, column]);
                }
            }
        }

        private void ExtractTileSet()
        {
            TileSetData = Content.Load<Texture2D>(TileSetName);
            if (TileSetData.Width / TileWidth > 16)
            {
                throw new Exception("The TileSet has to many tiles");
            }
            else if (TileSetData.Width / TileWidth < 16)
            {
                throw new Exception("The TileSet has not enough tiles");
            }

            IsSingleTileSet = true;
            if (TileSetData.Height / TileHeight > 1)
            {
                IsSingleTileSet = false;
            }
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
                    if (temp.R == 0 && temp.G == 0 && temp.B == 0)
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

        public void MapDraw(GameTime pGameTime)
        {
            for (int row = 0; row < MapSizeInTile.Item2; row++)
            {
                for (int column = 0; column < MapSizeInTile.Item1; column++)
                {
                    TileObject localTile = MapGrid[row, column];
                    if (localTile.Flag >= 0)
                    {
                        SpriteBatch.Draw(TileSetData, localTile.Position, new Rectangle(localTile.Flag * TileWidth, 0, TileWidth, TileHeight), Color.White);
                    }
                }
            }
        }
    }
}
