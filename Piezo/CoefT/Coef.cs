using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piezo.CoefT
{
    public abstract class Coef
    {
        protected int n;

        protected double l;

        protected double ro;
        protected double c;
        protected double e;
        protected double g;


        public Coef()
        {
        }

        public Coef(double ro, double c, double e, double g, double l, int n)
        {
            this.ro = ro;
            this.c = c;
            this.e = e;
            this.g = g;
            this.l = l;
            this.n = n;
        }

        public abstract double[,] FormM();
        public abstract double[,] FormA();
        public abstract double[,] FormE();
        public abstract double[,] FormC();
        public abstract double[,] FormG();

    }
}
