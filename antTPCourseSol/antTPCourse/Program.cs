using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antTPCourse
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Hello !");

            FourmiReine myQueen = new FourmiReine(1);

            Console.WriteLine(myQueen.name + myQueen.id);


            Console.Read();

        }
    }
}
