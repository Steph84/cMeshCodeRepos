using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antTPCourse
{
    class FourmiReine : Fourmi
    {
        public string name;

        public FourmiReine()
        {
        }

        public FourmiReine(int pId)
        {
            name = "Queen";
            id = pId;
        }

    }
}
