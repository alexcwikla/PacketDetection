using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
    public class CheckSumControl : IControl
    {
        public List<byte> CalculateControlPart(Frame nFrame)
        {
            List<byte> CheckSum = new List<byte>();
            int results = nFrame.GetInformationPart().Sum(x => Convert.ToInt32(x));
            
            return Functions.ConvertDecToByteList(results);
        }

        public List<byte> CalculateControlPart(Package nPackage)
        {
            int sum = 0;
            foreach (var item in nPackage.GetFrames())
            {
                sum += (int)item.GetControlPart().GetControlPartInDec();
            }
            
            return Functions.ConvertDecToByteList(sum);
        }
        public byte CollisionDetection(Frame nFrame)
        {
            ulong results = (ulong)nFrame.GetInformationPart().Sum(x => Convert.ToInt32(x));
            if( results == nFrame.GetControlPart().GetControlPartInDec())
            {
                return 1;
            }
            return 2;
        }

        public byte CollisionDetection(Frame nFrame)
        {
            ulong results = (ulong)nFrame.GetInformationPart().Sum(x => Convert.ToInt32(x));        // Obliczamy sume z czesci informacyjnej
            if (results == nFrame.GetControlPart().GetControlPartInDec())                           // Porownujemy z czescia kontrolna
            {
                if (nFrame.IsChanged() == false)
                {
                    Console.WriteLine("Wyglada na to ze jest ok");              // Blad nie wystapil
                    return 0;
                }
                else
                {
                    Console.WriteLine("Blad istnieje nie zostal wykryty");      // Blad istnieje i nie zostal wykryty
                    return 2;
                }
            }
            else
            {
                if (nFrame.IsChanged() == false)
                {
                    Console.WriteLine("Blad!");                                 // Blad wystapil
                    return 1;
                }
                else
                {
                    Console.WriteLine("False positive!");                       // Bledne wykrycie
                    return 3;
                }
            }
        }

        public byte CollisionDetection(Package nPackage)
        {
            ulong results = 0;                                                      // Tworzymy zmienna pomocnicza
            foreach (var item in nPackage.GetFrames())                              // Dodajemy wszysktie czesci kontrolne ramek
            {
                results += (ulong)item.GetControlPart().GetControlPartInDec();
            }
            if (results == nPackage.GetControlPart().GetControlPartInDec())         // Porownujemy sume z czescia kontrolna pakietu
            {
                if (nPackage.IsChanged() == false)
                {
                    Console.WriteLine("Wyglada na to ze jest ok");              // Blad nie wystapil
                    return 0;
                }
                else
                {
                    Console.WriteLine("Blad istnieje nie zostal wykryty");      // Blad istnieje i nie zostal wykryty
                    return 2;
                }
            }
            else
            {
                if (nPackage.IsChanged() == false)
                {
                    Console.WriteLine("Blad!");                                 // Blad wystapil
                    return 1;
                }
                else
                {
                    Console.WriteLine("False positive!");                       // Bledne wykrycie
                    return 3;
                }
            }
        }
    }
}
