using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

public class TileSetData
{
    #region DTO
    public class Tuile
    {
        public int Id { get; set; } // 1 à 48
        public Side LeftSide { get; set; }
        public Side RigthSide { get; set; }
    }

    public class Side
    {
        public int CrownNb { get; set; }
        public EnumNature Nature { get; set; }
    }

    public enum EnumNature
    {
        Castle = 0,
        Wheat = 1,
        Forest = 2,
        Water = 3,
        Grass = 4,
        Swamp = 5,
        Mine = 6,
        Empty = 10,
        Forbidden = 11
    }
    #endregion

    #region Method to initialize the Data
    public List<Tuile> LoadHardData()
    {
        List<Tuile> result = new List<Tuile>()
        {
            new Tuile()
            {
                Id = 1,
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat }
            },
            new Tuile()
            {
                Id = 2,
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat }
            },
            new Tuile()
            {
                Id = 3,
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest }
            },
            new Tuile()
            {
                Id = 4,
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest }
            },
            new Tuile()
            {
                Id = 5,
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest }
            },
            new Tuile()
            {
                Id = 6,
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest }
            },
            new Tuile()
            {
                Id = 7,
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Water },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Water }
            },
            new Tuile()
            {
                Id = 8,
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Water },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Water }
            },
            new Tuile()
            {
                Id = 9,
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Water },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Water }
            },
            new Tuile()
            {
                Id = 10,
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Grass },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Grass }
            },
            new Tuile()
            {
                Id = 11,
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Grass },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Grass }
            },
            new Tuile()
            {
                Id = 12,
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Swamp },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Swamp }
            },
            new Tuile()
            {
                Id = 13,
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest }
            },
            new Tuile()
            {
                Id = 14,
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Water }
            },
            new Tuile()
            {
                Id = 15,
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Grass }
            },
            new Tuile()
            {
                Id = 16,
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Swamp }
            },
            new Tuile()
            {
                Id = 17,
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Water }
            },
            new Tuile()
            {
                Id = 18,
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Grass }
            },
            new Tuile()
            {
                Id = 19,
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest }
            },
            new Tuile()
            {
                Id = 20,
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Water }
            },
            new Tuile()
            {
                Id = 21,
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Grass }
            },
            new Tuile()
            {
                Id = 22,
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Swamp }
            },
            new Tuile()
            {
                Id = 23,
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Mine }
            },
            new Tuile()
            {
                Id = 24,
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Forest },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat }
            },
            new Tuile()
            {
                Id = 25,
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Forest },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat }
            },
            new Tuile()
            {
                Id = 26,
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Forest },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat }
            },
            new Tuile()
            {
                Id = 27,
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Forest },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat }
            },
            new Tuile()
            {
                Id = 28,
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Forest },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Water }
            },
            new Tuile()
            {
                Id = 29,
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Forest },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Grass }
            },
            new Tuile()
            {
                Id = 30,
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Water },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat }
            },
            new Tuile()
            {
                Id = 31,
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Water },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat }
            },
            new Tuile()
            {
                Id = 32,
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Water },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest }
            },
            new Tuile()
            {
                Id = 33,
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Water },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest }
            },
            new Tuile()
            {
                Id = 34,
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Water },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest }
            },
            new Tuile()
            {
                Id = 35,
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Water },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest }
            },
            new Tuile()
            {
                Id = 36,
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 1, Nature = EnumNature.Grass }
            },
            new Tuile()
            {
                Id = 37,
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Water },
                RigthSide = new Side() { CrownNb = 1, Nature = EnumNature.Grass }
            },
            new Tuile()
            {
                Id = 38,
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 1, Nature = EnumNature.Swamp }
            },
            new Tuile()
            {
                Id = 39,
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Grass },
                RigthSide = new Side() { CrownNb = 1, Nature = EnumNature.Swamp }
            },
            new Tuile()
            {
                Id = 40,
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Mine },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat }
            },
            new Tuile()
            {
                Id = 41,
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 2, Nature = EnumNature.Grass }
            },
            new Tuile()
            {
                Id = 42,
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Water },
                RigthSide = new Side() { CrownNb = 2, Nature = EnumNature.Grass }
            },
            new Tuile()
            {
                Id = 43,
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 2, Nature = EnumNature.Swamp }
            },
            new Tuile()
            {
                Id = 44,
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Grass },
                RigthSide = new Side() { CrownNb = 2, Nature = EnumNature.Swamp }
            },
            new Tuile()
            {
                Id = 45,
                LeftSide = new Side() { CrownNb = 2, Nature = EnumNature.Mine },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat }
            },
            new Tuile()
            {
                Id = 46,
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Swamp },
                RigthSide = new Side() { CrownNb = 2, Nature = EnumNature.Mine }
            },
            new Tuile()
            {
                Id = 47,
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Swamp },
                RigthSide = new Side() { CrownNb = 2, Nature = EnumNature.Mine }
            },
            new Tuile()
            {
                Id = 48,
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 3, Nature = EnumNature.Mine }
            }
        };

        return result;
    }
    #endregion

    public List<Tuile> TilesShuffle(List<Tuile> pTuilesDeDepart)
    {
        List<Tuile> result = new List<Tuile>();

        //result = pTuilesDeDepart.OrderBy(x => Random.value).ToList();
        var rnd = new Random();
        result = pTuilesDeDepart.OrderBy(item => rnd.Next()).ToList();

        return result;
    }
}