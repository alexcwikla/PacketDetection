using System;
using System.Collections.Generic;




/*
 *                  Pakiet
 *                  [p][p][p][p][p][p]  - jakies dodatkowe miejsce na info
 *                  [1][1][0][1][0][1]
 *                  [1][1][0][1][0][1]
 *                  [1][1][0][1][0][1]
 *                  [1][1][0][1][0][1]
 *                  
 *                  
 *                  Ramki
 *                  [1, 1, 1, 1]
 *                  [1, 1, 1, 1]
 *                  [0, 0, 0, 0]
 *                  [1, 1, 1, 1]
 *                  [0, 0, 0, 0]
 *                  [1, 1, 1, 1]
 *                  
 *                  lub
 *                  
 *                  Pakiet
 *                  [a][b][c][d][e][f]  - wskazniki na ramki lub listy obiektow
 *                  
 *                  Ramki
 *                  a = [1, 1, 1, 1]
 *                  b = [1, 1, 1, 1]
 *                  c = [0, 0, 0, 0]
 *                  d = [1, 1, 1, 1]
 *                  e = [0, 0, 0, 0]
 *                  f = [1, 1, 1, 1]
 *                  
 *                  
 *                  lub 
 *                  
 *                  Pakiet
 *                  [1][1][0][1][0][1]
 *                  [1][1][0][1][0][1]
 *                  [1][1][0][1][0][1]
 *                  [1][1][0][1][0][1]
 * */



/*
 *        //Tablica postrzepiona - nie obawiamy sie o wymiar poszczegolnych pakietow
    void utworz_pakiet()
    {
        byte[][] pakiet = new byte[][]
        {
        new byte[] { 1, 1, 1, 1, 0, 0, 0, 0 },
        new byte[] { 1, 1, 1, 1, 0, 0, 0, 0 },
        new byte[] { 1, 1, 1, 1, 0, 0, 0, 0 },
        new byte[] { 1, 1, 1, 1, 0, 0, 0, 0 },
        new byte[] { 1, 1, 1, 1, 0, 0, 0, 0 }
        };
 * 
 **/



namespace Projekt_Kolko
{


    public class Pakiet : IPakiet
    {
        private List<Frame> FrameList = new List<Frame>();
        private ControlElements control_holder = new ControlElements();

        #region Private_zone<3
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
        #endregion

        /// <summary>
        /// Dodaje ramke na koniec listy
        /// </summary>
        public void AddFrame(Frame item)
        {
            try
            {
                FrameList.Add(item);
            }
            catch(Exception)
            {
                Console.WriteLine("Blad przy dodawaniu Ramki do Pakietu!");
            }
        }
        /// <summary>
        /// Usuwa WSZYSTKIE ramki.
        /// </summary>
        public void DeleteFrames()
        {
            FrameList.Clear();
        }

        /// <summary>
        /// Tworzy liste ramek(bez czesci kontrolnej). Sposob tworzenia zalezny od argumentow
        /// </summary>
        /// <param name="numberOfFrames">Liczba ramek do stworzenia</param>
        /// <param name="size">Dlugosc kazdej ramki ( bez czesci kontrolnej )</param>
        /// <param name="random">Okresla czy zawartosc ramek powinna byc losowa "TO DO" FALSE JESZCZE NIE DZIALA!!!!</param>
        public void GenerateFrameList(int numberOfFrames, int size, bool random = true)
        {
            DeleteFrames();
            for (int i = 0; i < numberOfFrames; i++)
            {
                AddFrame(new Frame.Builder().RandomFrame(size).SetControl(7).Create());
            }
        }

        /// <summary>
        /// Ustawia czesc kontrolna zamieniajac liczbę z systemu 10 na 2. Dlugosc listy zawierajacej 
        /// kontrolna czesc zalezna jest od zajmowanych bitow przez argument.
        /// </summary>
        /// <param name="number">Liczba w systemie dziesietnym</param>
        public void SetControlPart(int number)
        {
            control_holder.GetList().RemoveAll(item => true == true); //kasuje wszystkie elementy, zawsze
            AddElements(control_holder.GetList(), number);
        }



        #region Get Methods
        /// <summary>
        /// Zwraca liste zawierajaca bity kontroli
        /// </summary>
        /// <returns></returns>
        public List<byte> GetControlPart()
        {
            return control_holder.GetList();
        }
        /// <summary>
        /// Zwraca liste ramek
        /// </summary>
        /// <returns></returns>
        public List<Frame> GetFrames()
        {
            return FrameList;
        }
        #endregion

        #region Show Function
        /// <summary>
        /// Wyświetla czesc kontrolna w postaci binarnej
        /// </summary>
        /// <returns></returns>
        public void ShowControlPart()
        {
            Console.Write("Kontrola : ");
            Show(control_holder.GetList());
        }
        /// <summary>
        /// Wyświetla wszystkie ramki w postaci binarnej
        /// </summary>
        /// <returns></returns>
        public void ShowFrames()
        {
            foreach (var item in FrameList)
            {
                item.ShowInformationPart();
                item.ShowControlPart();
            }
        }
        #endregion


        public Frame this[int number]
        {
            get
            {
                try
                {
                    return FrameList[number];
                }
                catch (Exception)
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

    }




    
}
