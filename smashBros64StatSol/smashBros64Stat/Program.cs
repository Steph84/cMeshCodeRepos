using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smashBros64Stat
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Compute N64 Smash Bros data");

            using (var reader = new StreamReader("data.csv"))
            {
                List<string> listA = new List<string>();
                List<string> listB = new List<string>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    listA.Add(values[0]);
                    listB.Add(values[1]);
                }
            }


            Console.ReadKey();
        }
    }
}
