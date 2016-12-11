using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace antTPCourse
{
    class FourmiChef : Fourmi
    {
        protected int queenId;

        internal FourmiChef(int pId, int pQueId, int pX, int pY)
        {
            name = "Master";
            id = pId;
            coordX = pX;
            coordY = pY;
            queenId = pQueId;
            //Console.WriteLine("Master egg number " + id + " has been laid !");
        }

        internal void antRun()
        {
            while (true)
            {

                Thread.Sleep(2000);
                Console.WriteLine("Ant Master " + this.id + " from the queen " +  queenId + " alive ...");
                
            }
        }


        }
}
