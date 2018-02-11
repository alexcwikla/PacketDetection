using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
    /*
     * Okresla w jaki sposob bity w pakietach maja zostac przeklamane
     */
    public interface ICollision
    {
        void DoCollision(Frame nFrame, int interference_level);
        void DoCollision(Package nPackage, int interference_level);
    }
}
