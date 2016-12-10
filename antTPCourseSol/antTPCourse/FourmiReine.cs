using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace antTPCourse
{
    class FourmiReine : Fourmi
    {

        private int numEggs;
        private List<Oeufs> eggsList = new List<Oeufs>();   


        public FourmiReine(int pId, int pNumEggs)
        {
            name = "Queen";
            id = pId;
            coordX = 0;
            coordY = 0;
            numEggs = pNumEggs;
            ShowName();
        }

        internal void Ponte(int pEggs, int pX, int pY)
        {
            Random antChoice = new Random();
            int masterNum;
            int i = 0;

            masterNum = pEggs / 20 + 1;

            for (i = 0; i < masterNum; i++)
            {
                FourmiChef myMaster = new FourmiChef(( i + 1 ), pX, pY);
            }

            for (i = 0; i < pEggs; i++)
            {
                int thisMasId = antChoice.Next(1, masterNum + 1);
                FourmiSoldat monSoldat = new FourmiSoldat(( i + 1 ), thisMasId, pX, pY);
                eggsList.Add(monSoldat);
            }
            
        }

        internal void queenRun()
        {
            Console.WriteLine("I am alive as an ant queen !");
            Ponte(numEggs, coordX, coordY);
            sendHatch();
            while (true)
            {

                Thread.Sleep(500);
                Console.WriteLine("Ant Queen alive ...");
            }
        }

        private void sendHatch()
        {
            foreach(Oeufs thisEgg in eggsList)
            {
                thisEgg.Eclore();
            }
        }
        
         
    }
}
