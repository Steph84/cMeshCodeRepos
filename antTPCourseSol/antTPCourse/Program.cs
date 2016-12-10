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

            Console.WriteLine("Hello !");

            FourmiReine myQueen = new FourmiReine(1, 10);
            Thread queenThread = new Thread ( new ThreadStart ( myQueen.queenRun ) ); // thread object
            queenThread.Start(); // start the thread
            while (!queenThread.IsAlive);


            myQueen.ShowName();
            


            Console.Read();

        }
    }
}
