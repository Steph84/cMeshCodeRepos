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
        List<List<Character>> ListGames = new List<List<Character>>();

        int iter = 0;
        while (iter < 24)
        {
            List<TournByCharacter> sortedData = FinalData.OrderBy(x => x.GamePlayed).ToList();
            ListGames.Add(new List<Character>());
            List<CoupleCharGP> tempListForNext = new List<CoupleCharGP>();

            foreach (Character subCharac in Enum.GetValues(typeof(Character)))
            {
                tempListForNext.Add(new CoupleCharGP()
                {
                    Opponent = subCharac,
                    OppGP = 0
                });
            }

            // choose first one
            TournByCharacter firstChar = sortedData[0];
            ListGames.Last().Add(firstChar.Character);

            firstChar.GamePlayed++;

            tempListForNext = tempListForNext.Where(x => x.Opponent != firstChar.Character).ToList();
            foreach (CoupleCharGP item in tempListForNext)
            {
                item.OppGP += firstChar.ListCouple.Where(x => x.Opponent == item.Opponent).First().OppGP;
            }

            // choose second character
            Character tempSecondChar = firstChar.ListCouple.OrderBy(x => x.OppGP).First().Opponent;
            ListGames.Last().Add(tempSecondChar);
            TournByCharacter secondChar = FinalData.Where(x => x.Character == tempSecondChar).First();

            secondChar.GamePlayed++;
            firstChar.ListCouple.Where(x => x.Opponent == secondChar.Character).First().OppGP++;
            secondChar.ListCouple.Where(x => x.Opponent == firstChar.Character).First().OppGP++;

            tempListForNext = tempListForNext.Where(x => x.Opponent != secondChar.Character).ToList();
            foreach (CoupleCharGP item in tempListForNext)
            {
                item.OppGP += secondChar.ListCouple.Where(x => x.Opponent == item.Opponent).First().OppGP;
            }

            // choose the third character
            Character tempThirdChar = tempListForNext.OrderBy(x => x.OppGP).First().Opponent;
            ListGames.Last().Add(tempThirdChar);
            TournByCharacter thirdChar = FinalData.Where(x => x.Character == tempThirdChar).First();

            thirdChar.GamePlayed++;
            firstChar.ListCouple.Where(x => x.Opponent == thirdChar.Character).First().OppGP++;
            thirdChar.ListCouple.Where(x => x.Opponent == firstChar.Character).First().OppGP++;
            secondChar.ListCouple.Where(x => x.Opponent == thirdChar.Character).First().OppGP++;
            thirdChar.ListCouple.Where(x => x.Opponent == secondChar.Character).First().OppGP++;

            tempListForNext = tempListForNext.Where(x => x.Opponent != thirdChar.Character).ToList();
            foreach (CoupleCharGP item in tempListForNext)
            {
                item.OppGP += thirdChar.ListCouple.Where(x => x.Opponent == item.Opponent).First().OppGP;
            }

            // choose the fourth characater
            Character tempFourthChar = tempListForNext.OrderBy(x => x.OppGP).First().Opponent;
            ListGames.Last().Add(tempFourthChar);
            TournByCharacter fourthChar = FinalData.Where(x => x.Character == tempFourthChar).First();

            fourthChar.GamePlayed++;
            firstChar.ListCouple.Where(x => x.Opponent == fourthChar.Character).First().OppGP++;
            fourthChar.ListCouple.Where(x => x.Opponent == firstChar.Character).First().OppGP++;
            secondChar.ListCouple.Where(x => x.Opponent == fourthChar.Character).First().OppGP++;
            fourthChar.ListCouple.Where(x => x.Opponent == secondChar.Character).First().OppGP++;
            thirdChar.ListCouple.Where(x => x.Opponent == fourthChar.Character).First().OppGP++;
            fourthChar.ListCouple.Where(x => x.Opponent == thirdChar.Character).First().OppGP++;
            
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