using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
    public class RandomCollision : ICollision
    {
        public void DoCollision(Frame nFrame, int interference_level)
        {
            for (int i = 0; i < nFrame.GetInformationPart().Count; i++)
            {
                if (interference_level > Functions.GenerateRandomNumber(0, 10000))
                {
                    if (nFrame[i] == 0)
                        nFrame[i] = 1;
                    else
                        nFrame[i] = 0;
                }
            }
            for (int i = 0; i < nFrame.GetControlPart().GetList().Count; i++)
            {
                if (interference_level > Functions.GenerateRandomNumber(0, 10000))
                {
                    if (nFrame.GetControlPart()[i] == 0)
                        nFrame.GetControlPart()[i] = 1;
                    else
                        nFrame.GetControlPart()[i] = 0;
                }
            }
        }
    }
}
