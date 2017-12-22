using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
    class Program
    {
        static bool Parity_bit(byte[] array)
        {
            byte sum = 0;
            for (int i = 0; i < array.Length; i++)
                if (array[i] == 1)
                    sum++;

            if (sum % 2 == 0)
                return true;
            else
                return false;
        }

        static bool Checksum(byte[] array, int number_of_byte_sum)
        {
            byte sum = 0;
            for (int i = 0; i < array.Length - number_of_byte_sum; i++)
                if (array[i] == 1)
                    sum++;

            int last_element = array.Length - number_of_byte_sum;
            if (array[last_element] == sum)
                return true;
            else
                return false;

        }

        static void Main(string[] args)
        { 
            // Przykladowo wygenerowane ramki i pakiety
            Pakiet pak = new Pakiet(); // Nowy nie zdefiniowany pakiet

            Console.WriteLine("PAKIET -----------------------------------------");

            pak.GenerateFrameList(15, 30, true); // Wygenerowanie 15 ramek o rozmiarze 30, o losowej zawartosci
            pak.ShowFrames(); // wypisanie wszystkich ramek ( czesc informacyjna i kontrolna )
            pak.SetControlPart(5); // ustawienie czesci kontrolnej na [1][0][1]
            Console.WriteLine(pak.GetControlPart()[1]); // wypisanie wartosci znajdujacej sie pod indexem 1 czesci kontrolnej

            Console.WriteLine("------------------------------------------------");


            Console.WriteLine("Ramka -----------------------------------------");
            // Stworzenie ramki o losowej zawartosci o rozmiarze 20. Czesc kontrolna ustawiona na [1][0][1].
            Frame fra = new Frame.Builder().RandomFrame(20).SetControl(5).Create();

            fra.ShowInformationPart(); // wypisuje czesc informacyjna ramki
            fra.ShowControlPart(); // wypisuje czesc kontrolna ramki
            Console.WriteLine("-----------------------------------------------");

            Console.ReadKey();
        }

       
    }
}
