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
            while (true)
            {

                Thread.Sleep(1000);
                //Console.WriteLine("Ant Warrior " + this.id + " of the master " + this.masterId + " from the queen " + this.queenId + " alive ...");
            }
        }


    }
}
