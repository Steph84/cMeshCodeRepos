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

        internal List<FourmiChef> masterList = new List<FourmiChef>();
        internal List<FourmiSoldat> warriorList = new List<FourmiSoldat>();

        //private List<Oeufs> eggsList = new List<Oeufs>();
        private List<Thread> antThreads = new List<Thread>();


        public FourmiReine(int pId, int pNumEggs)
        {
            name = "Queen";
            id = pId;
            coordX = 0;
            coordY = 0;
            numEggs = pNumEggs;
            //ShowName();
        }
        
        internal void queenRun()
        {
            Console.WriteLine("I am alive as an ant queen !");
            Ponte(numEggs, coordX, coordY);
            sendHatch();
            while (true)
            {

                Thread.Sleep(1000);
                Console.WriteLine("Ant Queen alive ...");
            }
        }

        internal void Ponte(int pEggs, int pX, int pY)
        {
            Random antChoice = new Random();
            int masterNum;
            int i = 0;

            masterNum = pEggs / 20 + 1;

            Console.WriteLine("I'm laying " + pEggs + " warrior Eggs. And " + masterNum + " master Egg(s) to manage them");

            for (i = 0; i < masterNum; i++)
            {
                FourmiChef myMaster = new FourmiChef((i + 1), pX, pY);
                masterList.Add(myMaster);
            }

            for (i = 0; i < pEggs; i++)
            {
                int thisMasId = antChoice.Next(1, masterNum + 1);
                FourmiSoldat monSoldat = new FourmiSoldat((i + 1), thisMasId, pX, pY);
                warriorList.Add(monSoldat);
            }
        }

        private void sendHatch()
        {
            Console.WriteLine("Come into life my minions !");

            foreach(FourmiChef thisMaster in masterList)
            {
                Thread masterThread = new Thread(new ThreadStart(thisMaster.antRun));
                if (!masterThread.IsAlive)
                {
                    masterThread.Start();
                }
            }

            foreach (FourmiSoldat thisWarrior in warriorList)
            {
                Thread warriorThread = new Thread(new ThreadStart(thisWarrior.antRun));
                if (!warriorThread.IsAlive)
                {
                    warriorThread.Start();
                }
            }
        }
         
    }
}