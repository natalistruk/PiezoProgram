using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piezo
{
    class Courant
    {
        private int i;
        private int n;
        private double L;

        public Courant(double length, int number, int total) { L = length; i = number; n = total; }

        public double fi(double x)
        {
            double h = L / n;

            double f;

            if (x < 0 || x > L)
                f = 0;
            else
            {

                if ((i - 1) * h < x && x <= i * h)
                {
                    f = (x - (i - 1) * h) / h;
                }
                else
                    if (i * h < x && x <= (i + 1) * h)
                {
                    f = ((i + 1) * h - x) / h;
                }
                else
                    f = 0;
            }

            return f;
        }


        public double fiDerivative(double x)
        {
            double h = L / n;

            double f;

            if (x < 0 || x > L)
                f = 0;
            else
            {

                if ((i - 1) * h < x && x <= i * h)
                {
                    f = n / L;
                }
                else
                    if (i * h < x && x <= (i + 1) * h)
                {
                    f = -n / L;
                }
                else
                    f = 0;
            }

            return f;
        }
    }
}
