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
            
            FourmiReine myQueen = new FourmiReine(1, 25);
            Thread queenThread = new Thread ( new ThreadStart ( myQueen.queenRun ) ); // thread object
            queenThread.Start(); // start the thread
            while (!queenThread.IsAlive);
            
            Console.Read();

        }
    }
}
