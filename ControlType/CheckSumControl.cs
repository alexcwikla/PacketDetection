using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
    public class CheckSumControl : IControl
    {
        

        public List<byte> CalculateControlPart(Frame nFrame, int sizeOfControlPart = Functions.FLEXIBLE)
        {
            int results = nFrame.GetInformationPart().Sum(x => Convert.ToInt32(x));
            List<byte> CheckSum = Functions.ConvertDecToByteList(results);

            if (sizeOfControlPart != Functions.FLEXIBLE)
                SetSizeOfList(ref CheckSum, sizeOfControlPart);

            return CheckSum;
        }

        public List<byte> CalculateControlPart(Package nPackage, int sizeOfControlPart = Functions.FLEXIBLE)
        {
            int sum = 0;
            foreach (var item in nPackage.GetFrames())
            {
                sum += (int)item.GetControlPart().GetControlPartInDec();
            }
            List<byte> CheckSum = Functions.ConvertDecToByteList(sum);
            //Console.WriteLine(CheckSum);
            if (sizeOfControlPart != Functions.FLEXIBLE)
                SetSizeOfList(ref CheckSum, sizeOfControlPart);

            return CheckSum;
        }

        public void SetSizeOfList(ref List<byte> lst, int sizeOfControlPart)
        {
            //Console.WriteLine("jestem uta");
            if (sizeOfControlPart < lst.Count)
                for (int i = lst.Count; i != sizeOfControlPart; i--)
                {
                    lst.RemoveAt(0);
                }
            else
                for (int i = lst.Count; i != sizeOfControlPart; i++)
                {
                    lst.Insert(0, 0);
                }
           
        }



        public byte CollisionDetection(Frame nFrame)
        {
            ulong results = (ulong)nFrame.GetInformationPart().Sum(x => Convert.ToInt32(x));        // Obliczamy sume z czesci informacyjnej
            return DeterminateResults(results, nFrame.IsChanged(), nFrame.GetControlPart().GetControlPartInDec());
        }

        public byte CollisionDetection(Package nPackage)
        {
            ulong results = 0;                                                      // Tworzymy zmienna pomocnicza
            foreach (var item in nPackage.GetFrames())                              // Dodajemy wszysktie czesci kontrolne ramek
            {
                results += (ulong)item.GetControlPart().GetControlPartInDec();
            }       // Porownujemy sume z czescia kontrolna pakietu
            return DeterminateResults(results, nPackage.IsChanged(), nPackage.GetControlPart().GetControlPartInDec());
        }

        private byte DeterminateResults(ulong sum, bool changed, ulong object_sum)
        {
            if (sum == object_sum)                     // Sprawdzanie bledu
            {
                if (changed == false)
                {
                    //Console.WriteLine("Wyglada na to ze jest ok");              // NIe wykryto bledu i blad nie wystapil
                    return 0;
                }
                else
                {
                   // Console.WriteLine("Blad istnieje nie zostal wykryty");      // Blad istnieje i nie zostal wykryty
                    return 2;
                }
            }
            else
            {
                if (changed == true)
                {
                    //Console.WriteLine("Wykryto blad");                                 // Blad wystapil
                    return 1;
                }
                else
                {
                    //Console.WriteLine(changed + " zmiana ");
                    //Console.WriteLine("Bledne wykrycie - COS JEST NIE TAK ---------------------------------------------------");                       // Bledne wykrycie
                    return 3;
                }
            }
        }
    }
}