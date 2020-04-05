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
        public Rectangle Position { get; set; }
    }

    public class Side
    {
        public int CrownNb { get; set; }
        public EnumNature Nature { get; set; }
    }

    public enum EnumNature
    {
        Wheat = 1,
        Forest = 2,
        Water = 3,
        Grass = 4,
        Swamp = 5,
        Mine = 6
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
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat }
            },
            new Tuile()
            {
                Id = 2,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat }
            },
            new Tuile()
            {
                Id = 3,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest }
            },
            new Tuile()
            {
                Id = 4,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest }
            },
            new Tuile()
            {
                Id = 5,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest }
            },
            new Tuile()
            {
                Id = 6,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest }
            },
            new Tuile()
            {
                Id = 7,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Water },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Water }
            },
            new Tuile()
            {
                Id = 8,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Water },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Water }
            },
            new Tuile()
            {
                Id = 9,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Water },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Water }
            },
            new Tuile()
            {
                Id = 10,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Grass },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Grass }
            },
            new Tuile()
            {
                Id = 11,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Grass },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Grass }
            },
            new Tuile()
            {
                Id = 12,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Swamp },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Swamp }
            },
            new Tuile()
            {
                Id = 13,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest }
            },
            new Tuile()
            {
                Id = 14,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Water }
            },
            new Tuile()
            {
                Id = 15,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Grass }
            },
            new Tuile()
            {
                Id = 16,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Swamp }
            },
            new Tuile()
            {
                Id = 17,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Water }
            },
            new Tuile()
            {
                Id = 18,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Grass }
            },
            new Tuile()
            {
                Id = 19,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest }
            },
            new Tuile()
            {
                Id = 20,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Water }
            },
            new Tuile()
            {
                Id = 21,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Grass }
            },
            new Tuile()
            {
                Id = 22,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Swamp }
            },
            new Tuile()
            {
                Id = 23,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Mine }
            },
            new Tuile()
            {
                Id = 24,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Forest },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat }
            },
            new Tuile()
            {
                Id = 25,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Forest },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat }
            },
            new Tuile()
            {
                Id = 26,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Forest },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat }
            },
            new Tuile()
            {
                Id = 27,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Forest },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat }
            },
            new Tuile()
            {
                Id = 28,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Forest },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Water }
            },
            new Tuile()
            {
                Id = 29,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Forest },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Grass }
            },
            new Tuile()
            {
                Id = 30,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Water },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat }
            },
            new Tuile()
            {
                Id = 31,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Water },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat }
            },
            new Tuile()
            {
                Id = 32,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Water },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest }
            },
            new Tuile()
            {
                Id = 33,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Water },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest }
            },
            new Tuile()
            {
                Id = 34,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Water },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest }
            },
            new Tuile()
            {
                Id = 35,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Water },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Forest }
            },
            new Tuile()
            {
                Id = 36,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 1, Nature = EnumNature.Grass }
            },
            new Tuile()
            {
                Id = 37,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Water },
                RigthSide = new Side() { CrownNb = 1, Nature = EnumNature.Grass }
            },
            new Tuile()
            {
                Id = 38,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 1, Nature = EnumNature.Swamp }
            },
            new Tuile()
            {
                Id = 39,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Grass },
                RigthSide = new Side() { CrownNb = 1, Nature = EnumNature.Swamp }
            },
            new Tuile()
            {
                Id = 40,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 1, Nature = EnumNature.Mine },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat }
            },
            new Tuile()
            {
                Id = 41,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 2, Nature = EnumNature.Grass }
            },
            new Tuile()
            {
                Id = 42,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Water },
                RigthSide = new Side() { CrownNb = 2, Nature = EnumNature.Grass }
            },
            new Tuile()
            {
                Id = 43,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat },
                RigthSide = new Side() { CrownNb = 2, Nature = EnumNature.Swamp }
            },
            new Tuile()
            {
                Id = 44,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Grass },
                RigthSide = new Side() { CrownNb = 2, Nature = EnumNature.Swamp }
            },
            new Tuile()
            {
                Id = 45,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 2, Nature = EnumNature.Mine },
                RigthSide = new Side() { CrownNb = 0, Nature = EnumNature.Wheat }
            },
            new Tuile()
            {
                Id = 46,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Swamp },
                RigthSide = new Side() { CrownNb = 2, Nature = EnumNature.Mine }
            },
            new Tuile()
            {
                Id = 47,
                Position = new Rectangle(),
                LeftSide = new Side() { CrownNb = 0, Nature = EnumNature.Swamp },
                RigthSide = new Side() { CrownNb = 2, Nature = EnumNature.Mine }
            },
            new Tuile()
            {
                Id = 48,
                Position = new Rectangle(),
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