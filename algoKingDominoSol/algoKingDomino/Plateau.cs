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

    public enum EnumCardinal
    {
        North = 1,
        East = 2,
        South = 4,
        West = 8
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

            neoSquare.SidePlaced = new Side() { CrownNb = 0, Nature = EnumNature.Empty };
            if (i == 40)
            {
                neoSquare.SidePlaced.Nature = EnumNature.Castle;
            }

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

    // Method to get a list of playable actions in a plateau
    public void ComputePossibleCases(List<Case> pPlayerPlateau)
    {
        // TODO make a custom object
        List<Case> result = new List<Case>();

        // check the empty cases
        foreach (Case elt in pPlayerPlateau.Where(x => x.SidePlaced.Nature == EnumNature.Empty))
        {
            // foreach case, check if there is at least one terrain case around
            List<Case> casesAround = CheckTerrainAround(elt, pPlayerPlateau);

            // if so, the actual case is a possible match
            if (casesAround.Count != 0)
            {
                //result.Add()
                int x = 2;
            }
        }
    }

    // Method to check around specific case into one plateau if there is a terrain
    public List<Case> CheckTerrainAround(Case pCase, List<Case> pPlayerPlateau)
    {
        List<Case> result = new List<Case>();
        foreach (EnumCardinal card in Enum.GetValues(typeof(EnumCardinal)))
        {
            Point coord = new Point();
            Case caseCorresp = null;

            if (card == EnumCardinal.North)
            {
                coord = new Point(pCase.SquareCoordinate.X, pCase.SquareCoordinate.Y - 1);
                caseCorresp = pPlayerPlateau.Where(x => x.SquareCoordinate == coord).FirstOrDefault();
            }
            else if (card == EnumCardinal.East)
            {
                coord = new Point(pCase.SquareCoordinate.X + 1, pCase.SquareCoordinate.Y);
                caseCorresp = pPlayerPlateau.Where(x => x.SquareCoordinate == coord).FirstOrDefault();
            }
            else if (card == EnumCardinal.South)
            {
                coord = new Point(pCase.SquareCoordinate.X, pCase.SquareCoordinate.Y + 1);
                caseCorresp = pPlayerPlateau.Where(x => x.SquareCoordinate == coord).FirstOrDefault();
            }
            else if (card == EnumCardinal.West)
            {
                coord = new Point(pCase.SquareCoordinate.X - 1, pCase.SquareCoordinate.Y);
                caseCorresp = pPlayerPlateau.Where(x => x.SquareCoordinate == coord).FirstOrDefault();
            }

            if (caseCorresp != null)
            {
                if ((int)caseCorresp.SidePlaced.Nature < 10)
                {
                    result.Add(caseCorresp);
                }
            }
        }
        return result;
    }
}