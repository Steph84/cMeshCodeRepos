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
        {   // initialization of both random seeds
            int antRandSeed1 = Environment.TickCount + this.id;
            int antRandSeed2 = Environment.TickCount - this.id;
            // creation of random objects for an ant
            Random dirChoice1 = new Random(antRandSeed1);
            Random dirChoice2 = new Random(antRandSeed2);

            while (true)
            {
                int tempDir1 = dirChoice1.Next(1, 5); // first part of the direction
                int tempDir2;
                do
                {
                    tempDir2 = dirChoice2.Next(1, 5); // second part of the direction to allow diagonal move
                // values have to be unopposed (stagnation)
                } while (tempDir2 == tempDir1 + 2 || tempDir2 == tempDir1 - 2);
                
                // correction to avoid 2 steps move
                if(tempDir2 == tempDir1)
                {
                    tempDir2 = -1;
                }
                
                // manage the coordonate chqnges
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

                Console.WriteLine("the ant " + this.id + " is at the coordinate : " + coordX + ", " + coordY);
                Thread.Sleep(1000);
                //Console.WriteLine("Ant Warrior " + this.id + " of the master " + this.masterId + " from the queen " + this.queenId + " alive ...");
            }
        }


    }
}
