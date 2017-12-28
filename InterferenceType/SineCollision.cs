using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
    class SineCollision
    {
        /*
        * Sinus w postaci a*sin(b*x + c)
        * Nalezy zdefiniowac wspolczynniki a, b i c
        */
        double arg_a = 1;
        double arg_b = 2;
        double arg_c = 0;
        /*
        * Domyslny wzor sin(2x) dziedzina <-3,14 - 3,14>
        */
        private double Function(double x)
        {
            return arg_a * Math.Sin(arg_b * x);
        }
        SineCollision(double _arg_a, double _arg_b, double _arg_c )
        {
            this.arg_a = _arg_a;
            this.arg_b = _arg_b;
            this.arg_c = _arg_c;
        }
        public void DoCollision(Frame nFrame, int interference_level)
        {

        }
        public void DoCollision(Package nPackage, int interference_level)
        {
            nPackage.
        }
    }
}
