using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace antTPCourse
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is an ant hill");
            Console.WriteLine("===================");
            
            // creation of the first queen
            FourmiReine myQueen = new FourmiReine(1, 25, 0, 0);
            Thread queenThread = new Thread ( new ThreadStart ( myQueen.queenRun ) ); // thread object
            if (!queenThread.IsAlive)
            {
                queenThread.Start(); // start the thread
            }

            /*
            // creation of the second queen
            FourmiReine myOtherQueen = new FourmiReine(2, 5, 99, 99);
            Thread otherQueenThread = new Thread(new ThreadStart(myOtherQueen.queenRun)); // thread object
            if (!otherQueenThread.IsAlive)
            {
                otherQueenThread.Start(); // start the thread
            }
            */

            //Console.Read(); // to maintain the console opened

        }
    }
}
