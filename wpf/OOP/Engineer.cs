using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    internal class Engineer : Person, IPerson, IElectironicEngineer, IComputerEngineer
    {
        public string Eats(string text)
        {
            throw new NotImplementedException();
        }

        public string PCBDesign(int _pcb)
        {
            throw new NotImplementedException();
        }

        public string SoftwareLesson(string lesson)
        {
            throw new NotImplementedException();
        }

        public int TC()
        {
            throw new NotImplementedException();
        }
    }
}
