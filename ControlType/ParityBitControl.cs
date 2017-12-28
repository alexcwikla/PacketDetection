using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
    public class ParityBitControl : IControl
    {
        public List<byte> CalculateControlPart(Frame nFrame)
        {
            ulong results = (ulong)nFrame.GetInformationPart().Sum(x => Convert.ToInt32(x));
            return DeterminateParityBit(results);
        }
        public List<byte> CalculateControlPart(Package nPakiet)
        {
            ulong sum = 0;
            List<byte> nLists_information_part = new List<byte>();
            foreach (var frame in nPakiet.GetFrames())
            {
                sum += (ulong)frame.GetInformationPart().Sum(x => Convert.ToInt32(x));
            }
            return DeterminateParityBit(sum);
        }
        private List<byte> DeterminateParityBit(ulong sum)
        {
            List<byte> parityBit = new List<byte>();
            if (sum % 2 == 0)
                parityBit.Add(0);
            else
                parityBit.Add(1);
            return parityBit;
        }

        public byte CollisionDetection(Frame nFrame)
        {
            ulong count = (ulong)nFrame.GetInformationPart().Count(b => b == 1) + nFrame.GetControlPart().GetControlPartInDec(); //Zlicza jedynki z infoParty i controlPart
            Console.WriteLine("Count - " + count);  //Do wykasowania linijka

            if (count % 2 == 0)                     // Sprawdzanie bledu
            {
                Console.WriteLine("Wyglada na to ze jest ok");
                return 0;                           // Blad nie wystapil
            }                                       // TODO: Nie wykryto a blad wystapil 

            else
            {
                Console.WriteLine("Blad!");
                return 1;                           // Blad wystapil
            }                                    // TODO: Bledne wykrycie
        }

        public byte CollisionDetection(Package nPakiet)
        {
            ulong count = nPakiet.GetControlPart().GetControlPartInDec();

            Console.WriteLine("Count przed dodaniem ramek: " + count);          // Do usuniecia!

            foreach (var frame in nPakiet.GetFrames())                          //Zlicza jedynki z infoParty calego pakietu
            {
                count += (ulong)frame.GetInformationPart().Count(b => b == 1);  //Zlicza jedynki z infoParty  z pojedynczej ramki
            }

            Console.WriteLine("Count - " + count);  //Do wykasowania linijka

            if (count % 2 == 0)                     // Sprawdzanie bledu
            {
                Console.WriteLine("Wyglada na to ze jest ok");
                return 0;                           // Blad nie wystapil
            }                                       // TODO: Nie wykryto a blad wystapil 

            else
            {
                Console.WriteLine("Blad!");
                return 1;                           // Blad wystapil
            }
        }


    }
}
