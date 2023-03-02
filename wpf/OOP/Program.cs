using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Teacher _teacher = new Teacher();
            _teacher.email = "aybuke";
            StaticClass.sayi=12;//statik değişkenlere claslardan yeni bir nesne üretmeden erişebilir ve kullanbiliriz.
            Console.WriteLine(_teacher.Email);
            Console.ReadLine();
        }
    }
}
