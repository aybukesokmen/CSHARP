using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    internal class Teacher:Person,IPerson,IElectironicEngineer
    {
        public string Branch { get; set; }

        public string Eats(string text)
        {
            return text;
        }

        public string PCBDesign(int _pcb)
        {
            
            string pcb = _pcb.ToString();
            int a=int.Parse(pcb);//string ifadeyi parse edebilim başka bir türe
            double b = Convert.ToDouble(a);//içerideki değişeni hagi değiskene cevirceksek ona evirir.
            return pcb;
        }

        public int TC()
        {
            throw new NotImplementedException();
        }
    }
}
