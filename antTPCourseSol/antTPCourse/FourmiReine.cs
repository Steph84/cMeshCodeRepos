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
        }

        internal void Ponte(int pEggs, int pX, int pY)
        {
            int i = 0;
            for (i = 0; i < pEggs; i++)
            {
                FourmiSoldat monSoldat = new FourmiSoldat(( i + 1 ), pX, pY);
                eggsList.Add(monSoldat);
                Console.WriteLine("Warrior egg number " + (i + 1) + " laid !");
            }
            
            
        }

        internal void queenRun()
        {
            Console.WriteLine("I am alive as an ant queen !");
            Ponte(numEggs, coordX, coordY);
            while (true)
            {

                Thread.Sleep(500);
                Console.WriteLine("Ant Queen alive ...");
            }
        }
        
         
    }
}
