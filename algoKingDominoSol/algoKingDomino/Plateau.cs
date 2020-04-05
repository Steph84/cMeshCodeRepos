using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static TileSetData;

public class Plateau
{
    public List<Case> YellowPlayer { get; set; }
    public List<Case> GreenPlayer { get; set; }
    public List<Case> BluePlayer { get; set; }
    public List<Case> PinkPlayer { get; set; }
    int RowNumber = 9;
    int ColumnNumber = 9;
    int SquareNumbers = 81;
    int SquareSize = 32;

    public class Case
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public Rectangle SquareDestination { get; set; }
        public Point SquareCoordinate { get; set; }
        public Side SidePlaced { get; set; }
    }

    public Plateau()
    {
        YellowPlayer = InitializePlateau();
        GreenPlayer = InitializePlateau();
        BluePlayer = InitializePlateau();
        PinkPlayer = InitializePlateau();
    }

    public List<Case> InitializePlateau()
    {
        List<Case> result = new List<Case>();

        int tempRow = 0;
        int tempColumn = 0;

        for (int i = 0; i < SquareNumbers; i++)
        {
            Case neoSquare = new Case();
            neoSquare.SquareDestination = new Rectangle(tempColumn * SquareSize, tempRow * SquareSize, SquareSize, SquareSize);

            if (i == 37)
            {
                neoSquare.SidePlaced = new Side() { CrownNb = 0, Nature = EnumNature.Castle };
            }
            else { neoSquare.SidePlaced = null; }

            neoSquare.SquareCoordinate = new Point(tempColumn, tempRow);
            neoSquare.Row = tempRow;
            neoSquare.Column = tempColumn;

            result.Add(neoSquare);

            if (tempColumn < ColumnNumber - 1)
            {
                tempColumn++;
            }
            else
            {
                tempColumn = 0;
                tempRow++;
            }
        }

        return result;
    }
}