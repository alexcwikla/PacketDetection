﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{

    public static class Functions
    {


        //
        public const int FLEXIBLE = 0;

        //Generuje losowe liczby. Static zapewnia wygenerowanie innych liczb za kazdym razem.
        static Random rnd = new Random();

        /// <summary>
        /// Zamienia liste przechowywujaca typ byte na liczbe.
        /// </summary>
        /// <param name="nList"></param>
        /// <returns>Zwraca liczbowa reprezentacje listy</returns>
        static public ulong GetPartInDec(List<byte> nList)
        {
            string result = string.Join("", nList);
            ulong InDec = Convert.ToUInt64(result, 2);
            return InDec;
        }

        /// <summary>
        /// Generuje losowa wartosc typu byte(1 lub 0).
        /// </summary>
        /// <returns></returns>
        static public byte GenerateRandomByte()
        {
            return (byte)rnd.Next(0, 2);
        }

        /// <summary>
        /// Generuje losowa liczbe z przedzialu
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        static public int GenerateRandomNumber(int min, int max)
        {
            return rnd.Next(min, max+1);
        }

        /// <summary>
        /// Zamienia liczbe w systemie dziesietnym na liczbe w systemie binarnym w postaci Listy.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        static public List<byte> ConvertDecToByteList(int number)
        {
            String binary = Convert.ToString(number, 2);
            List<byte> byteList = new List<byte>();
            for (int i = 0; i < binary.Length; i++)
            {
                if (Int32.TryParse(Convert.ToString(binary[i]), out int toList))
                    byteList.Add(Convert.ToByte((byte)toList));
                else
                    Console.WriteLine("String could not be parsed.");

            }
            return byteList;
        }


        public static void AddElements(List<byte> nlst, int number)
        {
            foreach (var item in Functions.ConvertDecToByteList(number))
            {
                nlst.Add(item);
            }
        }
    }
}
