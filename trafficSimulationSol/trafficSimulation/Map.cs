using Microsoft.Xna.Framework;
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
}

[DataContract]
public partial class Tile
{
    [DataMember]
    public int FlagTile { get; set; }

    public int? Id;
    public int? Row { get; set; }
    public int? Column { get; set; }
    public Rectangle? SquareDestination { get; set; }
    public Point? SquareCoordinate { get; set; }
}

public static class Constantes
{
    public const int RowNumber = 20;
    public const int ColumnNumber = 36;
    public const int SquareNumbers = 720;
    public const int SquareSize = 32;
}