using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
    public class CheckSumControl : IControl
    {
        public List<byte> CalculateControlPart(Frame nFrame)
        {
            List<byte> CheckSum = new List<byte>();
            int results = nFrame.GetInformationPart().Sum(x => Convert.ToInt32(x));
            
            return Functions.ConvertDecToByteList(results);
        }

        public List<byte> CalculateControlPart(Package nPackage)
        {
            int sum = 0;
            foreach (var item in nPackage.GetFrames())
            {
                sum += (int)item.GetControlPart().GetControlPartInDec();
            }
            
            return Functions.ConvertDecToByteList(sum);
        }
        public byte CollisionDetection(Frame nFrame)
        {
            ulong results = (ulong)nFrame.GetInformationPart().Sum(x => Convert.ToInt32(x));
            if( results == nFrame.GetControlPart().GetControlPartInDec())
            {
                return 1;
            }
            return 2;
        }

    }
}
