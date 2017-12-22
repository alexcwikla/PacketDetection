using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
    class ControlElements 
    {
        List<byte> control_holder = new List<byte>();


        public List<byte> GetList()
        {
            return control_holder;
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
