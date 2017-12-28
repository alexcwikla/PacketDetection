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
        private ControlElements control_part = new ControlElements();
        private IControl control_type;


        #region Private zone


        private void Show(List<byte> array)
        {
            foreach (var item in array)
            {
                Console.Write(item);
            }
            Console.WriteLine();
        }


        private Frame()
        {
            // Ochrona przed tworzeniem obiektu bez buildera
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
                fra.GetControlPart().SetControlPart(number);
                return this;
            }
            /// <summary>
            /// Ustawia czesc kontrolna ramki, bazujac na typie kontroli. Przypisuje dany typ kontroli do ramki
            /// </summary> 
            /// <param name="number">Liczba okreslajaca kontrolna czesc ramki</param>
            public Builder SetControlType(IControl control_type)
            {
                fra.SetControlType(control_type);
                fra.SetControlPartByType();
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
                frame_holder.Add(Functions.GenerateRandomByte());
            }
        }

        #region Show Methods
        public void ShowInformationPart()
        {
            Console.Write("Information part : ");
            Show(frame_holder);
        }
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
        #endregion

        #region Get Methods
        public List<byte> GetInformationPart()
        {
            return frame_holder;
        }
        
        public ulong GetInformationPartInDec()
        {
            return Functions.GetPartInDec(this.GetInformationPart());
        }
        public ControlElements GetControlPart()
        {
            return control_part;
        }
        #endregion

        #region Set Methods
        public void SetInformationPart(int number)
        {
            frame_holder.RemoveAll(item => true == true);
            Functions.AddElements(control_part.GetList(), number);
        }
        public void SetInformationPart(List<byte> nList)
        {
            frame_holder.Clear();
            foreach (var item in nList)
            {
                frame_holder.Add(item);
            }
        }

        public void SetControlPartByType()
        {
            try
            {
                control_part.SetControlPart(control_type.CalculateControlPart(this));
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Nie zdefiniowany typ obiektu - SetControlPartByType");
            }
        }
        public void SetControlType(IControl ICon)
        {
            try
            {
                control_type = ICon;
            }
            catch(System.NullReferenceException)
            {
                Console.WriteLine("Nie zdefiniowany typ obiektu - SetControlType");
            }
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
