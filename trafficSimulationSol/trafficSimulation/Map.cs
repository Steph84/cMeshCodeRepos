using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

[DataContract]
public partial class Map
{
    [DataMember]
    public List<Tile> ListTiles;

    private Texture2D TileSetMap { get; set; }
    private Vector2 SpriteOrigin { get; set; }
    private SpriteEffects SpriteDirection { get; set; }
    
    public void InitializeMap()
    {
        #region Load json file for map
        string myDirectory = "";
        // if exe has the json file in the same location
        myDirectory = Environment.CurrentDirectory;
        if (!File.Exists(myDirectory + "/map.json"))
        {
            // if the exe is in the release or debug directories
            myDirectory = Directory.GetParent(myDirectory).Parent.Parent.Parent.FullName;
        }
        MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(File.ReadAllText(myDirectory + "/map.json")));
        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Map));
        TrafficSimulator.MyMap = (Map)ser.ReadObject(stream);
        #endregion

        TrafficSimulator.MyMap.SpriteOrigin = new Vector2();
        TrafficSimulator.MyMap.SpriteDirection = SpriteEffects.None;
        TrafficSimulator.MyMap.TileSetMap = TrafficSimulator.GlobalContent.Load<Texture2D>("roadTileSet");

        CompleteInitMap();
    }

    private void CompleteInitMap()
    {
        int tempId = 1;
        int tempRow = 0;
        int tempColumn = 0;
        foreach (Tile t in TrafficSimulator.MyMap.ListTiles)
        {
            t.Id = tempId;
            t.SquareDestination = new Rectangle(tempColumn * Constantes.SquareSize, tempRow * Constantes.SquareSize, Constantes.SquareSize, Constantes.SquareSize);
            t.SquareCoordinate = new Point(tempColumn, tempRow);
            t.Row = tempRow;
            t.Column = tempColumn;

            t.SourceQuad = new Rectangle(t.Flag * Constantes.SquareSize, 0, Constantes.SquareSize, Constantes.SquareSize);

            if (tempColumn < Constantes.ColumnNumber - 1)
            {
                tempColumn++;
            }
            else
            {
                tempColumn = 0;
                tempRow++;
            }
            tempId++;
        }
    }

    public void MapDraw(GameTime pGameTime)
    {
        foreach (Tile t in TrafficSimulator.MyMap.ListTiles)
        {
            TrafficSimulator.spriteBatch.Draw(TileSetMap, t.SquareDestination, t.SourceQuad, Color.White, 0, SpriteOrigin, SpriteDirection, 0);
        }
    }
}

[DataContract]
public partial class Tile
{
    [DataMember]
    public int Flag { get; set; }

    public int Id;
    public int? Row { get; set; }
    public int? Column { get; set; }
    public Rectangle SquareDestination { get; set; }
    public Point SquareCoordinate { get; set; }
    public Rectangle SourceQuad { get; set; }
}

public static class Constantes
{
    public const int RowNumber = 20;
    public const int ColumnNumber = 32; // 36 - 4 (hud)
    public const int SquareNumbers = RowNumber * ColumnNumber;
    public const int SquareSize = 32;
}