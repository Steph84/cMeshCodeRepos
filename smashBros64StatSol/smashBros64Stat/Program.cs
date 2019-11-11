using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static ExtractionTools;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Compute N64 Smash Bros data");

        Dictionary<string, Stage> DictDataToStage = new Dictionary<string, Stage>()
        {
            {"pea", Stage.Peach},
            {"jun", Stage.Jungle},
            {"hyr", Stage.Hyrule},
            {"zeb", Stage.Zebes },
            {"yos", Stage.Yoshi},
            {"rev", Stage.Reves},
            {"sec", Stage.SecteurZ},
            {"saf", Stage.Safrania},
            {"cha", Stage.Champignon}
        };

        #region Initialize FullData
        List<DataByStage> FullData = new List<DataByStage>();
        foreach (Stage stage in Enum.GetValues(typeof(Stage)))
        {
            DataByStage tempDataByStage = new DataByStage()
            {
                Stage = stage,
                ListDataByCharacter = new List<DataByCharacter>()
            };

            foreach (Character charac in Enum.GetValues(typeof(Character)))
            {
                DataByCharacter tempDataByCharacter = new DataByCharacter()
                {
                    Character = charac,
                    Win = 0,
                    Lose = 0,
                    Tie = 0,
                    KO = 0,
                    TKO = 0
                };
                tempDataByStage.ListDataByCharacter.Add(tempDataByCharacter);
            }
            
            FullData.Add(tempDataByStage);
        }
        #endregion

        using (var reader = new StreamReader("../../data.csv"))
        {
            int line_count = 0;
            while (!reader.EndOfStream)
            {
                line_count++;
                Stage currentStage = Stage.None;
                
                var line = reader.ReadLine();
                var values = line.Split(';');

                // first line or multiple of 14
                if (line_count == 1 || line_count % 14 == 0)
                {
                    currentStage = DictDataToStage[values[0]];
                }
                else
                {
                    DataByStage dataToUpdate = FullData.Where(x => x.Stage == currentStage).First();

                }


                
                
            }
        }


        Console.ReadKey();
    }
}
