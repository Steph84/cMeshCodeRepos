using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace antTPCourse
{

    class FourmiSoldat : Fourmi
    {
        protected int masterId;
        protected int queenId;
        
        internal FourmiSoldat(int pId, int pMasId, int pQueId, int pX, int pY)
        {
            name = "Warrior";
            id = pId;
            coordX = pX;
            coordY = pY;
            masterId = pMasId;
            queenId = pQueId;
            //Console.WriteLine("Warrior egg number " + id + " has been laid ! Its Master is number " + masterId);
        }

        internal void antRun()
        {
            int antRandSeed = Environment.TickCount + this.id;
            Random dirChoice1 = new Random(antRandSeed);
            Random dirChoice2 = new Random(antRandSeed);

            while (true)
            {
                int tempDir1 = dirChoice1.Next(1, 5);
                Console.WriteLine("fuck it " + tempDir1);
                int tempDir2;
                do
                {
                    tempDir2 = dirChoice2.Next(1, 5);
                    Console.WriteLine("so what " + tempDir2);
                } while (tempDir2 == tempDir1);
                

                switch (tempDir1)
                {
                    case 1: // go to north
                        coordX = coordX - 1;
                        break;
                    case 2: // go to east
                        coordY = coordY + 1;
                        break;
                    case 3: // go to south
                        coordX = coordX + 1;
                        break;
                    case 4: // go to west
                        coordY = coordY - 1;
                        break;
                }
                
                switch (tempDir2)
                {
                    case 1: // go to north
                        coordX = coordX - 1;
                        break;
                    case 2: // go to east
                        coordY = coordY + 1;
                        break;
                    case 3: // go to south
                        coordX = coordX + 1;
                        break;
                    case 4: // go to west
                        coordY = coordY - 1;
                        break;
                }
                
                Thread.Sleep(10000);
                //Console.WriteLine("the ant " + this.id + " is at the coordinate : " + coordX + ", " + coordY);
                //Console.WriteLine("Ant Warrior " + this.id + " of the master " + this.masterId + " from the queen " + this.queenId + " alive ...");
            }
        }


    }
}
