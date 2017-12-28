using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
    interface ICollision
    {
        void DoCollision(Frame nFrame, int interference_level);
    }
}
