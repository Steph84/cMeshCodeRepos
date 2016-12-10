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

        internal FourmiSoldat(int pId, int pMasId, int pX, int pY)
        {
            name = "Warrior";
            id = pId;
            coordX = pX;
            coordY = pY;
            masterId = pMasId;
            Console.WriteLine("Warrior egg number " + id + " has been laid ! Its Master is number " + masterId);
        }

        private ThreadStart antRun()
        {
            Console.WriteLine("thread ant warrior !");
        }


    }
}
