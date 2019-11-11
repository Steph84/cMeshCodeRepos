using System.Collections.Generic;

class ExtractionTools
{
    public enum Stage
    {
        Peach, // Château de Peach
        Jungle, // Jungle du Congo
        Hyrule, // Château d'Hyrule
        Zebes, // Planète Zebes
        Yoshi, // Ile Yoshi
        Reves, // Pays des Rêves
        SecteurZ,
        Safrania,
        Champignon, //Royaume Champignon
        None
    }

    public enum Character
    {
        Luigi = 2,
        Mario,
        DonkeyKong,
        Link,
        SamusAran,
        CFalcon,
        Ness,
        Yoshi,
        Kirby,
        Fox,
        Pikachu,
        Rondoudou
    }

    public class DataByStage
    {
        public Stage Stage { get; set; }
        public List<DataByCharacter> ListDataByCharacter { get; set; }
    }

    public class DataByCharacter
    {
        public Character Character { get; set; }
        public int Win { get; set; }
        public int Lose { get; set; }
        public int Tie { get; set; }
        public int KO { get; set; }
        public int TKO { get; set; }
    }
}
