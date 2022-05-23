using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piezo
{
    class Parabola
    {
        //номер параболи
        private int i;

        //к-сть скінченних елементів
        private int n;

        //довжина відрізка
        private double l;

        public Parabola() { i = 0; n = 10; l = 1; }
        public Parabola(double l, int i, int n)
        {
            this.l = l;
            this.i = i;
            this.n = n;
        }

        public double fi(double x)
        {
            double h = l / n;

            double f;

            if (x < 0 || x > l)
                f = 0;
            else
            {
                int k = i / 2;
                if (i % 2 == 0)
                {
                    if (x >= (k - 1) * h && x <= k * h)
                        f = 2 * (1.0 / h) * (1.0 / h) * (x - (k - 1) * h) * (x - (k - 1) * h - h / 2);
                    else
                        if (x >= k * h && x <= (k + 1) * h)
                    {
                        f = 2 * (1.0 / h) * (1.0 / h) * (x - (k + 1) * h) * (x - k * h - h / 2);
                    }
                    else
                        f = 0;

                }
                else
                {
                    if (x >= k * h && x <= (k + 1) * h)
                        f = -4 * (1.0 / h) * (1.0 / h) * (x - k * h) * (x - (k + 1) * h);
                    else
                        f = 0;

                }
            }

            return f;
        }

        public double fiDerivative(double x)
        {
            double h = l / n;

            double f;

            if (x < 0 || x > l)
                f = 0;
            else
            {
                int k = i / 2;
                if (i % 2 == 0)
                {
                    if (x >= (k - 1) * h && x <= k * h)
                        f = 2 * (1.0 / h) * (1.0 / h) * ((x - (k - 1) * h) + (x - (k - 1) * h - h / 2));
                    else
                        if (x >= k * h && x <= (k + 1) * h)
                    {
                        f = 2 * (1.0 / h) * (1.0 / h) * ((x - (k + 1) * h) + (x - k * h - h / 2));
                    }
                    else
                        f = 0;

                }
                else
                {
                    if (x >= k * h && x <= (k + 1) * h)
                        f = -4 * (1.0 / h) * (1.0 / h) * ((x - k * h) + (x - (k + 1) * h));
                    else
                        f = 0;

                }
            }

            return f;
        }
    }
}
