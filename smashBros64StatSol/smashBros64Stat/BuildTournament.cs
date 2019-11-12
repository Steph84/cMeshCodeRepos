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
        List<TournByCharacter> FinalData = InitData();
        List<Tuple<Character, Character, Character, Character>> ListGames = new List<Tuple<Character, Character, Character, Character>>();

        int iter = 0;
        while (iter < 12)
        {
            List<TournByCharacter> sortedList = FinalData.OrderBy(x => x.GamePlayed).ToList();

            List<TournByCharacter> listCharToUpdate = new List<TournByCharacter>();
            listCharToUpdate.Add(sortedList[0]);

            foreach (TournByCharacter charcToLook in sortedList.Skip(1))
            {
                List<CoupleCharGP> oppToLook = charcToLook.ListCouple.OrderBy(y => y.OppGP).Take(6).ToList();
                CoupleCharGP hypoOppToKeep = oppToLook.Where(z => z.Opponent == sortedList[0].Character).FirstOrDefault();
                if (hypoOppToKeep != null)
                {
                    listCharToUpdate.Add(charcToLook);
                }

                if (listCharToUpdate.Count == 4)
                {
                    break;
                }
            }

            List<Character> listCharInvolved = listCharToUpdate.Select(x => x.Character).ToList();
            foreach (TournByCharacter item in listCharToUpdate)
            {
                item.GamePlayed++;
                foreach (Character thisChar in listCharInvolved.Where(y => y != item.Character))
                {
                    item.ListCouple.Where(z => z.Opponent == thisChar).Single().OppGP++;
                }
            }

            ListGames.Add(new Tuple<Character, Character, Character, Character>
                (
                    listCharToUpdate[0].Character,
                    listCharToUpdate[1].Character,
                    listCharToUpdate[2].Character,
                    listCharToUpdate[3].Character
                ));

            iter++;
        }

        int machin = 2;
    }

    private List<TournByCharacter> InitData()
    {
        List<TournByCharacter> tempFinalData = new List<TournByCharacter>();
        foreach (Character charac in Enum.GetValues(typeof(Character)))
        {
            TournByCharacter tempTournByCharacter = new TournByCharacter()
            {
                Character = charac,
                GamePlayed = 0,
                ListCouple = new List<CoupleCharGP>()
            };

            foreach (Character subCharac in Enum.GetValues(typeof(Character)))
            {
                if (subCharac == charac)
                {
                    continue;
                }
                tempTournByCharacter.ListCouple.Add(new CoupleCharGP()
                {
                    Opponent = subCharac,
                    OppGP = 0
                });
            }

            tempFinalData.Add(tempTournByCharacter);
        }

        return tempFinalData;
    }

    class TournByCharacter
    {
        public Character Character { get; set; }
        public int GamePlayed { get; set; }
        public List<CoupleCharGP> ListCouple { get; set; }
    }

    class CoupleCharGP
    {
        public Character Opponent { get; set; }
        public int OppGP { get; set; }
    }
}