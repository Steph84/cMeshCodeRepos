using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Program;

class BuildTournament
{
    public BuildTournament()
    {
        List<TournByCharacter> FinalData = new List<TournByCharacter>();
        foreach (Character charac in Enum.GetValues(typeof(Character)))
        {
            TournByCharacter tempTournByCharacter = new TournByCharacter()
            {
                Character = charac,
                DictGames = new Dictionary<Character, int>()
            };

            foreach (Character subCharac in Enum.GetValues(typeof(Character)))
            {
                if (subCharac == charac)
                {
                    continue;
                }
                tempTournByCharacter.DictGames.Add(subCharac, 0);
            }

            FinalData.Add(tempTournByCharacter);
        }

        int x = 2;
    }

    class TournData
    {
        List<TournByCharacter> ListTournByCharacter { get; set; }
    }

    class TournByCharacter
    {
        public Character Character { get; set; }
        public Dictionary<Character, int> DictGames { get; set; }
    }
}
