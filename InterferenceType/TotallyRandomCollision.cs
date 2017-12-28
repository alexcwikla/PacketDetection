using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
    class TotallyRandomCollision : ICollision
    {
        public void DoCollision(Frame nFrame, int interference_level)
        {
            for (int i = 0; i < nFrame.GetInformationPart().Count; i++)
            {
                if (interference_level > Functions.GenerateRandomNumber(0, 10000))
                {
                    nFrame[i] = Functions.GenerateRandomByte();
                }
            }
            for (int i = 0; i < nFrame.GetControlPart().GetList().Count; i++)
            {
                if (interference_level > Functions.GenerateRandomNumber(0, 10000))
                {
                    nFrame.GetControlPart()[i] = Functions.GenerateRandomByte();
                }
            }
        }
    }
}
