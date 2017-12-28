using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
    public class ParityBitControl : IControl
    {
        public List<byte> CalculateControlPart(Frame nFrame)
        {
            ulong results = (ulong)nFrame.GetInformationPart().Sum(x => Convert.ToInt32(x));
            return DeterminateParityBit(results);
        }
        public List<byte> CalculateControlPart(Package nPakiet)
        {
            ulong sum = 0;
            List<byte> nLists_information_part = new List<byte>();
            foreach (var frame in nPakiet.GetFrames())
            {
                sum += (ulong)frame.GetInformationPart().Sum(x => Convert.ToInt32(x));
            }
            return DeterminateParityBit(sum);
        }
        private List<byte> DeterminateParityBit(ulong sum)
        {
            List<byte> parityBit = new List<byte>();
            if (sum % 2 == 0)
                parityBit.Add(0);
            else
                parityBit.Add(1);
            return parityBit;
        }


    }
}
