using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Map
{
    public enum MapTexture
    {
        Void = 0,
        ocean = 1,
        river = 2,
        sand = 3,
        grass = 4,
        forest = 5,
        mountain = 6
    }

    private string BitMapName { get; set; }
    public Rectangle MapSizeInTile { get; set; }

    private int TileWidth { get; set; }
    private int TileHeight { get; set; }

    private Texture2D BitMapData { get; set; }
    public List<MapTexture> ListMapTexture { get; set; }

    //public TileObject[,] MapGrid { get; set; }

    public Rectangle TileSizeShowing { get; set; }
    private bool IsSingleTileSet { get; set; }
    private Texture2D TileSetData { get; set; }

    public Map(string pBitMapName, int pTileWidth=32, int pTileHeight=32)
    {
        BitMapName = pBitMapName;
        TileWidth = pTileWidth;
        TileHeight = pTileHeight;

        ExtractDataFromBitMap();
        //ExtractTileSet();
        //InitializeMapGrid();
    }
    #region Extraction of the map data from the BitMap
    private void ExtractDataFromBitMap()
    {
        // BitMap load
        BitMapData = Main.GlobalContent.Load<Texture2D>(BitMapName);
        MapSizeInTile = new Rectangle(0, 0, BitMapData.Width, BitMapData.Height);

        // extraction color of the BitMap
        Color[] rawData = new Color[MapSizeInTile.Width * MapSizeInTile.Height];
        BitMapData.GetData(rawData);

        // creation of the texture grid
        ListMapTexture = new List<MapTexture>();
        for (int row = 0; row < MapSizeInTile.Height; row++)
        {
            for (int column = 0; column < MapSizeInTile.Width; column++)
            {
                Color tempColorData = rawData[row * MapSizeInTile.Width + column];
                Vector4 tempRGB = new Vector4(tempColorData.R, tempColorData.G, tempColorData.B, 255);

                PersonnalColors.EnumColorName tempColor = PersonnalColors.GetColorNameFromRGB(tempRGB);

                switch (tempColor)
                {
                    case PersonnalColors.EnumColorName.White:
                        break;
                    case PersonnalColors.EnumColorName.Red:
                        break;
                    case PersonnalColors.EnumColorName.Cinnabar:
                        break;
                    case PersonnalColors.EnumColorName.Purple:
                        ListMapTexture.Add(MapTexture.mountain);
                        break;
                    case PersonnalColors.EnumColorName.Violet:
                        break;
                    case PersonnalColors.EnumColorName.Blue:
                        break;
                    case PersonnalColors.EnumColorName.Teal:
                        ListMapTexture.Add(MapTexture.ocean);
                        break;
                    case PersonnalColors.EnumColorName.Green:
                        ListMapTexture.Add(MapTexture.forest);
                        break;
                    case PersonnalColors.EnumColorName.Chartreuse:
                        ListMapTexture.Add(MapTexture.grass);
                        break;
                    case PersonnalColors.EnumColorName.Yellow:
                        ListMapTexture.Add(MapTexture.sand);
                        break;
                    case PersonnalColors.EnumColorName.Amber:
                        break;
                    case PersonnalColors.EnumColorName.Orange:
                        break;
                    case PersonnalColors.EnumColorName.Vermilion:
                        ListMapTexture.Add(MapTexture.river);
                        break;
                    default:
                        break;
                }
            }
        }
    }
    #endregion

    public void MapUpdate(GameTime pGameTime)
    {

    }

    public void MapDraw(GameTime pGameTime)
    {

    }
}
