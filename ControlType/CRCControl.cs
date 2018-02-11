using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
    public class CRCControl : IControl
    {
        public List<byte> CalculateControlPart(Frame nFrame, int sizeOfControlPart = Functions.FLEXIBLE)
        {

            int control_size = CalculateControlPartSize(nFrame.GetInformationPart().Count, sizeOfControlPart);

            List<byte> information_part = new List<byte>(nFrame.GetInformationPart());
            List<byte> CRC_divider = GenerateCRC(control_size);

            for (int i = 0; i < control_size; i++)
                information_part.Add(0);

            nFrame.GetControlPart().SetCRCDivider(CRC_divider);
            return CalculateCRCControlPart(control_size, CRC_divider, information_part);
        }
        public List<byte> CalculateControlPart(Package nPackage, int sizeOfControlPart = Functions.FLEXIBLE)
        {
            List<byte> NewPacket = new List<byte>();
            foreach (Frame frame in nPackage.GetFrames())
            {
                foreach (var nlist in frame.GetInformationPart())
                {
                    NewPacket.Add(nlist);
                }
                foreach (var nlist in frame.GetControlPart().GetList())
                {
                    NewPacket.Add(nlist);
                }
            }
            int control_size = CalculateControlPartSize(NewPacket.Count, sizeOfControlPart);
            List<byte> CRC_divider = GenerateCRC(control_size);

            for (int i = 0; i < control_size; i++)
                NewPacket.Add(0);

            nPackage.GetControlPart().SetCRCDivider(CRC_divider);

            return CalculateCRCControlPart(control_size, CRC_divider, NewPacket);
        }
        private List<byte> CalculateCRCControlPart(int size, List<byte> CRC_divider, List<byte> information_part)
        {
            for (int i = 0; i < information_part.Count - size; i++)
            {
                if(information_part[i] == 1)
                {
                    for (int j = 0; j < CRC_divider.Count; j++)
                    {
                        if(information_part[i+j] == CRC_divider[j]) // OPERACJA XOR
                        {
                            information_part[i + j] = 0;
                        }
                        else
                        {
                            information_part[i + j] = 1;
                        }
                    }
                }
            }
            List<byte> control_part = new List<byte>();
            for (int i = 0; i < size; i++)
                control_part.Add(information_part[information_part.Count - size + i]);

            return control_part;
        }
        private int CalculateControlPartSize(int size_of_information_part,int sizeOfControlPart)
        {
            if (sizeOfControlPart != Functions.FLEXIBLE)
                return sizeOfControlPart;
            int control_size = 1;
            for (int i = 4; size_of_information_part >= i; i = i * 2)
            {
                control_size++;
            }
            return control_size;
        }
        private List<byte> GenerateCRC(int size)
        {
            List<byte> CRC_divider = new List<byte>();
            for (int i = 0; i < size + 1; i++)
            {
                if (i == 0 || i == size) // Na poczatku i na koncu musi byc 1, reszta moze byc losowa
                {
                    CRC_divider.Add(1);
                }
                else
                {
                    CRC_divider.Add(Functions.GenerateRandomByte());
                }
            }
            return CRC_divider;
        }

        public byte CollisionDetection(Frame nFrame)
        {
            int control_size = nFrame.GetControlPart().GetCount(); // Oblicamy control_size 
                                                                   //CalculateControlPartSize(nFrame.GetInformationPart().Count); 

            List<byte> information_part = new List<byte>(nFrame.GetInformationPart());      // Kopia do nowej listy
            information_part.AddRange(nFrame.GetControlPart().GetList());                   // Dodajemy czesc kontrolna
            List<byte> CRC_divider = nFrame.GetControlPart().GetCRCDivider();               // Odczytujemy CRC_Divider`a

            CalculateCRCControlPart(control_size, CRC_divider, information_part);           // Powinno wszystko wyzerowac
            return DeterminateResults(information_part.Sum(x => Convert.ToInt32(x)), nFrame.IsChanged());

        }


        public byte CollisionDetection(Package nPackage)
        {
            List<byte> NewPacket = new List<byte>();                                    // Nowy pakiet
            foreach (Frame frame in nPackage.GetFrames())                               // Kopiowanie
            {
                foreach (var nlist in frame.GetInformationPart())
                {
                    NewPacket.Add(nlist);
                }
                foreach (var nlist in frame.GetControlPart().GetList())
                {
                    NewPacket.Add(nlist);
                }
            }
            int control_size = nPackage.GetControlPart().GetCount();
                //CalculateControlPartSize(NewPacket.Count);               // Oblicamy control_size 
            List<byte> CRC_divider = nPackage.GetControlPart().GetCRCDivider();         // Odczytujemy CRC_Divider`a

            NewPacket.AddRange(nPackage.GetControlPart().GetList());                    // Dodanie sumy kontrolnej

            CalculateCRCControlPart(control_size, CRC_divider, NewPacket);              // Powinno wszystko wyzerowac

            return DeterminateResults(NewPacket.Sum(x => Convert.ToInt32(x)), nPackage.IsChanged());

        }
        private byte DeterminateResults(int sum, bool changed)
        {
            if(sum == 0)                   // Sprawdzanie bledu
            {
                if (changed == false)
                {
                    //Console.WriteLine("Wyglada na to ze jest ok");              // NIe wykryto bledu i blad nie wystapil
                    return 0;
                }
                else
                {
                    //Console.WriteLine("Blad istnieje nie zostal wykryty");      // Blad istnieje i nie zostal wykryty
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
                   // Console.WriteLine(changed + " zmiana ");
                    //Console.WriteLine("Bledne wykrycie - COS JEST NIE TAK ---------------------------------------------------");    
                    return 3;
                }
            }
        }

    }
}
