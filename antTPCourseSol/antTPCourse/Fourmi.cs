using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace antTPCourse
{
    abstract class Fourmi
    {
        protected string name;
        protected int id;
        protected int hp;
        protected int attack;
        protected int defence;
        protected int coordX;
        protected int coordY;

        public void ShowName()
        {
            Console.WriteLine("My name is " + name + " number " + id);
        }

    }
}
