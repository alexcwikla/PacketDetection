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
            //Package pak = new Package(); // Nowy nie zdefiniowany Package

            //Console.WriteLine("Package -----------------------------------------");

            //pak.GenerateFrameList(4, 15, new CRCControl()); // Wygenerowanie 4 ramek o rozmiarze 15, kontrola typu CRC, o losowej zawartosci
            //pak.ShowFrames(); // wypisanie wszystkich ramek ( czesc informacyjna i kontrolna )
            //pak.ShowControlPart();
            ////pak.SetControlPart(5); // ustawienie czesci kontrolnej na [1][0][1]
            //Console.WriteLine(pak.GetControlPart()[1]); // wypisanie wartosci znajdujacej sie pod indexem 1 czesci kontrolnej

            //Console.WriteLine("------------------------------------------------");


            //Console.WriteLine("Ramka -----------------------------------------");
            // Stworzenie ramki o losowej zawartosci o rozmiarze 50. Typ kontroli Checksum
            Frame fra = new Frame.Builder().RandomFrame(50).SetControlType(new CheckSumControl()).Create();
            ICollision col = new TotallyRandomCollision();
            //fra.SetControlType(new CRCControl());
            //fra.SetControlPartByType();
            Console.WriteLine(fra.IsChanged());
            //Console.WriteLine(fra.GetInformationPart().Count);

            fra.ShowInformationPart(); // wypisuje czesc informacyjna ramki
            fra.ShowControlPart(); // wypisuje czesc kontrolna ramki

            col.DoCollision(fra, 1000);
            Console.WriteLine(fra.IsChanged());
            fra.ShowInformationPart(); // wypisuje czesc informacyjna ramki
            fra.ShowControlPart();

            //fra.ShowControlCRC();
            Console.WriteLine("-----------------------------------------------");

            Console.ReadKey();
        }

       
    }
}
