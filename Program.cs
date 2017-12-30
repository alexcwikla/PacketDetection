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

            //Frame fra = new Frame.Builder().RandomFrame(50).SetControlType(new CheckSumControl()).Create();

            TransmissionType newTranssmision = new TransmissionType(100, new ParityBitControl(), new RandomCollision(),1000,100,100);
            newTranssmision.Normal();
            //}
            

            Console.ReadKey();
        }

       
    }
}
