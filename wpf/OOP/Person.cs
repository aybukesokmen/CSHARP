using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    public class Person
    {
        //internal sadece bulunduğum projeden erişmemimi sağlar 
        //private nerde tanımlandı ise orda erişebilirim 
        //protected bulunudu pakette erişebilirm 
        //public heryerden erişiriz
        public string Tc { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string email { get; set; }
        //OOP prensipleri
        //encapsulatıon,Inheritance,abtract,polimorfizm
        public string Email {

            get {
                if (email !="aybuke" )
                {
                    return "email";
                }
                else
                {
                    return email;
                }
            }
        }
        //public string Eats(string text) {
        //    text = "aybuke sokmen yemek yer";
        //    return text;
        //}

    }
}
