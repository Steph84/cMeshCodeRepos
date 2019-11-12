using System.Collections.Generic;
using static Program;

class ExtractionTools
{

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
