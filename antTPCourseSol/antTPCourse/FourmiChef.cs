using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antTPCourse
{
    class FourmiChef : Fourmi
    {
        internal FourmiChef(int pId, int pX, int pY)
        {
            name = "Master";
            id = pId;
            coordX = pX;
            coordY = pY;
            Console.WriteLine("Master egg number " + id + " has been laid !");
        }

        internal void antRun()
        {
            Console.WriteLine("I am alive as an ant master !");
        }


        }
}
