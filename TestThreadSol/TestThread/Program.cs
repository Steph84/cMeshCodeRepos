using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;

namespace TestThread
{
    public class Program
    {
        private static int azerty;

        static void Main(string[] args)
        {
            // start a thread launching a method A
            Thread thread1 = new Thread(new ThreadStart(A));
            Thread thread2 = new Thread(new ThreadStart(B));
            thread1.Start();
            thread2.Start();
            
            // when the thread has finished, write azerty
            thread1.Join();
            thread2.Join();

            Console.WriteLine("Final {0}", azerty);
            Console.ReadKey();
        }

        // "main" of the thread
        static void A()
        {
            int i = 0;
            while(i < 100)
            {
                Thread.Sleep(50);
                Console.WriteLine("ThreadA {0}", i);
                i++;
                azerty += 10;
            }
        }

        static void B()
        {
            int i = 0;
            while (i < 100)
            {
                Thread.Sleep(20);
                Console.WriteLine("ThreadB {0}", azerty);
                i++;
            }
        }
    }
}
