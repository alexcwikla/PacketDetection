using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
    public class ControlElements 
    {
        private List<byte> control_holder = new List<byte>();
        private List<byte> CRC_divider;

        public List<byte> GetList()
        {
            return control_holder;
        }
        public ulong GetControlPartInDec()
        {
            return Functions.GetPartInDec(control_holder);
        }

        public void AddByte(Byte b)
        {
            control_holder.Add(b);
        }

        public void SetCRCDivider(List<byte> nList)
        {
            CRC_divider = new List<byte>();
            foreach (var item in nList)
            {
                CRC_divider.Add(item);
            }
        }

        public void SetControlPart(int number)
        {
            control_holder.RemoveAll(item => true == true); //kasuje wszystkie elementy, zawsze
            Functions.AddElements(control_holder, number);
        }

        public void SetControlPart(List<byte> nList)
        {
            control_holder.Clear();
            foreach (var item in nList)
            {
                control_holder.Add(item);
            }
        }
        public List<byte> GetCRCDivider()
        {
            return CRC_divider;
        }


        public byte this[int number]
        {
            get
            {
                return control_holder[number];
                //throw new ArgumentOutOfRangeException();
            }
            set
            {
                try
                {
                    control_holder[number] = value;
                }
                catch (Exception)
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
