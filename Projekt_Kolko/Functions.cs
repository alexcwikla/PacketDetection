using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
    public static class Functions
    {
        static Random rnd = new Random();
        static public ulong GetPartInDec(List<byte> nList)
        {
            string result = string.Join("", nList);
            ulong InDec = Convert.ToUInt64(result, 2);
            return InDec;
        }

        static public byte GenerateRandomByte()
        {
            return (byte)rnd.Next(0, 2);
        }
        static public List<byte> ConvertDecToByteList(int number)
        {
            String binary = Convert.ToString(number, 2);
            List<byte> byteList = new List<byte>();
            int toList;
            for (int i = 0; i < binary.Length; i++)
            {
                if (Int32.TryParse(Convert.ToString(binary[i]), out toList))
                    byteList.Add(Convert.ToByte((byte)toList));
                else
                    Console.WriteLine("String could not be parsed.");

            }
            return byteList;
        }

        public static void AddElements(List<byte> array, int number)
        {
            foreach (var item in Functions.ConvertDecToByteList(number))
            {
                array.Add(item);
            }
        }
    }
}
