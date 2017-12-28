using System;
using System.Collections.Generic;




/*
 *                  Package
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
 *                  Package
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
 *                  Package
 *                  [1][1][0][1][0][1]
 *                  [1][1][0][1][0][1]
 *                  [1][1][0][1][0][1]
 *                  [1][1][0][1][0][1]
 * */



/*
 *        //Tablica postrzepiona - nie obawiamy sie o wymiar poszczegolnych Packageow
    void utworz_Package()
    {
        byte[][] Package = new byte[][]
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


    public class Package : IPackage
    {
        private List<Frame> FrameList = new List<Frame>();
        private ControlElements control_part = new ControlElements();
        private IControl control_type;

        #region Private_zone<3
        private void Show(List<byte> nlist)
        {
            foreach (var item in nlist)
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
                if (Int32.TryParse(Convert.ToString(binary[i]), out toList))
                    array.Add(Convert.ToByte((byte)toList));
                else
                    Console.WriteLine("String could not be parsed.");
                
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
        /// <param name="controlType">Typ kontroli ( domyslnie ustawiony na ParityBitControl )</param>
        /// <param name="random">Okresla czy zawartosc ramek powinna byc losowa "TO DO" FALSE JESZCZE NIE DZIALA!!!!</param>
        public void GenerateFrameList(int numberOfFrames, int size, IControl _controlType = null, bool random = true)
        {
            if(_controlType == null)
            {
                _controlType = new ParityBitControl();
            }
            DeleteFrames();
            for (int i = 0; i < numberOfFrames; i++)
            {
                AddFrame(new Frame.Builder().RandomFrame(size).SetControlType(_controlType).Create());
            }
            this.SetControlType(_controlType);
            this.SetControlPartByType();



        }

        #region SET METHODS
        /// <summary>
        /// Ustawia czesc kontrolna zamieniajac liczbę z systemu 10 na 2. Dlugosc listy zawierajacej 
        /// kontrolna czesc zalezna jest od zajmowanych bitow przez argument.
        /// </summary>
        /// <param name="number">Liczba w systemie dziesietnym</param>
        public void SetControlPartByType()
        {
            try
            {
                control_part.SetControlPart(control_type.CalculateControlPart(this));
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Nie zdefiniowany typ obiektu");
            }
        }
        public void SetControlType(IControl ICon)
        {
            try
            {
                control_type = ICon;
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Nie zdefiniowany typ obiektu");
            }
        }
        #endregion

        #region Get Methods
        /// <summary>
        /// Zwraca liste zawierajaca bity kontroli
        /// </summary>
        /// <returns></returns>
        public ControlElements GetControlPart()
        {
            return control_part;
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
            Console.Write("Control part : ");
            Show(control_part.GetList());
            try
            {
                if (control_type.GetType() == typeof(CRCControl))
                {
                    Console.Write("CRC divider: ");
                    Show(control_part.GetCRCDivider());
                }
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Odwolanie do nieistniejacego obiektu");
            }
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
