using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antTPCourse
{
    class FourmiReine : Fourmi
    {

        private int numEggs;
        private List<Oeufs> eggsList = new List<Oeufs>();   


        public FourmiReine(int pId)
        {
            name = "Queen";
            id = pId;
            coordX = 0;
            coordY = 0;
        }

        internal void Ponte(int pEggs, int pX, int pY)
        {
            int i = 0;
            for (i = 0; i < (pEggs + 1); i++)
            {
                FourmiSoldat monSoldat = new FourmiSoldat(( i + 1 ), pX, pY);
                eggsList.Add(monSoldat);
            }
            
            Console.WriteLine("Ca fonctionne");
            
        }
    }
}
