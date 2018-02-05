using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace basicsTopDown.MapFolder
{
    public class Map
    {
        private ContentManager Content { get; set; }
        private SpriteBatch SpriteBatch { get; set; }

        private string BitMapName { get; set; }
        private Texture2D BitMapData { get; set; }
        private string TileSetName { get; set; }
        private Texture2D TileSetData { get; set; }
        private MapTexture[,] MapTextureGrid { get; set; }
        private int TileWidth { get; set; }
        private int TileHeight { get; set; }
        private double GameSizeCoefficient { get; set; }
        private bool IsSingleTileSet { get; set; }

        public TileObject[,] MapGrid { get; set; }
        public Rectangle MapSizeInTile { get; set; }

        public Map(ContentManager pContent, SpriteBatch pSpriteBatch, string pBitMapName, string pTileSetName, int pTileWidth, int pTileHeight, double pGameSizeCoefficient)
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
            for (int row = 0; row < MapSizeInTile.Height; row++)
            {
                for (int column = 0; column < MapSizeInTile.Width; column++)
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
                        if (column == MapSizeInTile.Width - 1)
                            DictTextureAround[2] = true;
                        else
                            if (MapTextureGrid[row, column + 1] == MapTexture.Wall)
                            DictTextureAround[2] = true;

                        // tile bottom
                        if (row == MapSizeInTile.Height - 1)
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
            // basic tile 32x32 so have to correct the GameSizeCoef for the tiles
            double GameSizeCoefficientFixed = GameSizeCoefficient / (TileWidth/32);
            
            int tileWidthShowing = (int)Math.Round(TileWidth * GameSizeCoefficientFixed, MidpointRounding.AwayFromZero);
            int tileHeightShowing = (int)Math.Round(TileHeight * GameSizeCoefficientFixed, MidpointRounding.AwayFromZero);

            int tileId = 0;
            MapGrid = new TileObject[MapSizeInTile.Height, MapSizeInTile.Width];
            for (int row = 0; row < MapSizeInTile.Height; row++)
            {
                for (int column = 0; column < MapSizeInTile.Width; column++)
                {
                    MapGrid[row, column] =
                        new TileObject(Content, SpriteBatch,
                                       new Rectangle(column * tileWidthShowing, row * tileHeightShowing, tileWidthShowing, tileHeightShowing),
                                       null,
                                       MapTextureGrid[row, column]);

                    // manual update of the sprite texture
                    MapGrid[row, column].SpriteData = TileSetData;

                    tileId += 1;
                    MapGrid[row, column].Id = tileId;
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
            MapSizeInTile = new Rectangle(0, 0, BitMapData.Width, BitMapData.Height);

            // extraction color of the BitMap
            Color[] rawData = new Color[MapSizeInTile.Width * MapSizeInTile.Height];
            BitMapData.GetData<Color>(rawData);

            // creation of the texture grid
            MapTextureGrid = new MapTexture[MapSizeInTile.Height, MapSizeInTile.Width];
            for (int row = 0; row < MapSizeInTile.Height; row++)
            {
                for (int column = 0; column < MapSizeInTile.Width; column++)
                {
                    Color temp = rawData[row * MapSizeInTile.Width + column];

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
            for (int row = 0; row < MapSizeInTile.Height; row++)
            {
                for (int column = 0; column < MapSizeInTile.Width; column++)
                {
                    TileObject localTile = MapGrid[row, column];
                    if (localTile.Flag >= 0)
                    {
                        SpriteBatch.Draw(localTile.SpriteData, localTile.Position, new Rectangle(localTile.Flag * TileWidth, 0, TileWidth, TileHeight), Color.White);
                    }
                }
            }
        }
    }
}
