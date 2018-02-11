using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
    class Program
    {
        
        static void Main(string[] args)
        {
            // Przykladowo wygenerowane ramki i Packagey


            BitsCollision BC = new BitsCollision.Builder().SetBasedOnPackage().SetRandomCollision().Create();

            SineCollision sin = new SineCollision(1, 2, 0, -60, 60);

            TransmissionType newTranssmision = new TransmissionType(1000, new CheckSumControl(), BC, 1000, 100, 100,8);
            newTranssmision.UserStop();
            






            Console.ReadKey();
        }

       
    }
}

