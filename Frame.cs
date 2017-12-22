using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{

    public class Frame : IFrame
    {
        private List<byte> frame_holder = new List<byte>();
        private ControlElements control_holder = new ControlElements();

        #region Private zone
        static Random rnd = new Random();
        static private byte GenerateRandomByte()
        {
            return (byte)rnd.Next(0, 2);
        }
        static private ulong GetPartInDec(List<byte> nList)
        {
            string result = string.Join("", nList);
            ulong InDec = Convert.ToUInt64(result, 2);
            return InDec;
        }
        private void Show(List<byte> array)
        {
            foreach (var item in array)
            {
                Console.Write(item);
            }
            Console.WriteLine();
        }
        private void AddElements(List<byte> array, int number)
        {
            String binary = Convert.ToString(number, 2);
            int toList;
            for (int i = 0; i < binary.Length; i++)
            {
                if (Int32.TryParse(Convert.ToString(binary[i]), out toList)) ;
                else
                    Console.WriteLine("String could not be parsed.");
                array.Add(Convert.ToByte((byte)toList));
            }
        }
        private Frame()
        {

        }
        #endregion

        #region Builder zone
        public class Builder
        {
            Frame fra = new Frame();
            /// <summary>
            /// Tworzy ramke z losowo wygenerowanymi bitami.
            /// </summary>
            /// <param name="size">Okresla dlugosc ramki</param>
            public Builder RandomFrame(int size)
            {
                fra.frame_holder.RemoveAll(item => true == true);
                fra.CreateRandomInformationPart(size);
                return this;
            }
            /// <summary>
            /// Tworzy ramke przechowywyjaca podana liczbe jako argument w postaci binarnej. Rozmiar ramki zalezny od podanej liczby
            /// </summary>
            /// <param name="number">Liczba okreslajaca informacyjna czesc ramki</param>
            public Builder SetFrame(int number)
            {
                fra.SetInformationPart(number);
                return this;
            }
            /// <summary>
            /// Wpisuje podana liczbe w postaci binarnej jako czesc kontrolna ramki
            /// </summary>
            /// <param name="number">Liczba okreslajaca kontrolna czesc ramki</param>
            public Builder SetControl(int number)
            {
                fra.SetControlPart(number);
                return this;
            }

            /// <summary>
            /// Zwraca ramke wygenerowana przez poprzednie metody
            /// </summary>
            public Frame Create()
            {
                return fra;
            }
        }
        #endregion


        ~Frame()
        {
            Console.WriteLine("Kasuje");
        }
        public void CreateRandomInformationPart(int size)
        {
            
            for (int i = 0; i < size; i++)
            {
                frame_holder.Add(GenerateRandomByte());
            }
        }

        #region Show Methods
        public void ShowInformationPart()
        {
            Console.Write("Frame : ");
            Show(frame_holder);
        }
        public void ShowControlPart()
        {
            Console.Write("Kontrola : ");
            Show(control_holder.GetList());
        }
        #endregion

        #region Get Methods
        public List<byte> GetInformationPart()
        {
            return frame_holder;
        }
        public List<byte> GetControlPart()
        {
            return control_holder.GetList();
        }
        public ulong GetControlPartInDec()
        {
            return GetPartInDec(this.GetControlPart());
        }
        public ulong GetInformationPartInDec()
        {
            return GetPartInDec(this.GetInformationPart());
        }
        #endregion

        #region Set Methods
        public void SetInformationPart(int number)
        {
            frame_holder.RemoveAll(item => true == true);
            AddElements(control_holder.GetList(), number);
        }
        public void SetControlPart(int number)
        {
            control_holder.GetList().RemoveAll(item => true == true); //kasuje wszystkie elementy, zawsze
            AddElements(control_holder.GetList(), number);
        }
        #endregion

        public byte this[int number]
        {
            get
            {
                try
                {
                    return frame_holder[number];
                }
                catch (Exception)
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            set
            {
                /* jakby ktos chciał ustawic inna liczbe :))) */
                if ( value > 1 || value < 0)
                {
                    value = 1;
                } 
                try
                {
                    frame_holder[number] = value;
                }
                catch (Exception)
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

  

    }
}
