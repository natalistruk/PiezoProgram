using System;
using Piezo.CoefT;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piezo
{
    public delegate double Function(double x);
    public class Solver
    {
        private int n;
        private double l;
        private double ro;
        private double c;
        private double e;
        private double g;

        private double sigma;
        private double D;


        private double[] u;
        private double[] p;

        private double h;

        private double[] xi;

        //масив парабол
        private Function[] fi;

        //масив похідних від парабол
        private Function[] fiDerivative;
        private Coef coef;

        public Solver(double _ro, double _c, double _e, double _g, double _sigma, double _D, double _l, int _n)
        {
            ro = _ro;
            c = _c;
            e = _e;
            g = _g;
            sigma = _sigma;
            D = _D;
            l = _l;
            n = _n;
            h = l / n;
            FormX();
            FormFi();
            FormFiDerivative();

            coef = new ConstantMode(ro, c, e, g, l, n, this);

        }
        public int N
        {
            get { return n; }
        }
        public double L
        {
            get { return l; }
        }

        public double[] U
        {
            get { return u; }
        }
        public double[] P
        {
            get { return p; }
        }

        public double[] X
        {
            get { return xi; }
        }
        public Function[] Fi
        {
            get { return fi; }
        }

        public Function[] FiDerivative
        {
            get { return fiDerivative; }
        }


        private void FormX()
        {
            xi = new double[n+1];
            xi[0] = 0;
            for (int i = 1; i <= n; i++)
            {
                xi[i] = xi[i - 1] + h;

            }
        }
        private void FormFi()
        {
            fi = new Function[2 * n + 1];

            Parabola[] parabolas = new Parabola[2 * n + 1];

            for (int i = 0; i <= 2 * n; i++)
            {
                parabolas[i] = new Parabola(l, i, n);
                fi[i] = new Function(parabolas[i].fi);

            }
        }
        private void FormFiDerivative()
        {
            fiDerivative = new Function[2 * n + 1];

            Parabola[] parabolas = new Parabola[2 * n + 1];

            for (int i = 0; i <= 2 * n; i++)
            {
                parabolas[i] = new Parabola(l, i, n);
                fiDerivative[i] = new Function(parabolas[i].fiDerivative);
            }
        }


        //квадратурна формула Гаусса
        public static double IntegrateGauss(Function f, double a, double b)
        {
            double[] x = new double[8];
            x[0] = -0.96028986;
            x[7] = -x[0];
            x[1] = -0.79666648;
            x[6] = -x[1];
            x[2] = -0.52553242;
            x[5] = -x[2];
            x[3] = -0.18343464;
            x[4] = -x[3];

            double[] A = new double[8];
            A[0] = A[7] = 0.10122854;
            A[1] = A[6] = 0.22238103;
            A[2] = A[5] = 0.31370664;
            A[3] = A[4] = 0.36268378;

            double[] t = new double[8];
            for (int i = 0; i < 8; i++)
            {
                t[i] = (a + b) / 2 + ((b - a) / 2) * x[i];
            }

            double integral = 0;
            for (int i = 0; i < 8; i++)
            {
                integral += (b - a) * (A[i] * f(t[i])) / 2;

            }

            return integral;
        }






        public void Solve()
        {

            u = new double[2 * n + 1];
            p = new double[2 * n + 1];

            double[,] C = coef.FormC();
            double[,] E = coef.FormE();
            double[,] G = coef.FormG();


            //формуємо вектор
            double[] f = new double[4 * n + 2];

            double[] l = new double[2 * n + 1];
            double[] r = new double[2 * n + 1];

            l[2 * n] = sigma;
            r[2 * n] = D;




            for (int i = 0; i < 4 * n + 1; i += 2)
            {
                f[i] = l[i / 2];
                f[i + 1] = r[i / 2];

            }

            //формуємо загальну блочно-п'ятидіагональну матрицю

            Matrix[,] matrix = new Matrix[2 * n + 1, 5];

            for (int i = 0; i <= 2 * n; i++)
            {
                for (int k = 0; k < 5; k++)
                {
                    matrix[i, k] = new Matrix(C[i, k], E[i, k],
                            -E[i, k], G[i, k]);

                }
            }
            //застосовуємо крайові умови на лівому кінці стержня
            matrix[0, 2].A11 = Math.Pow(10.0, 50.0);
            matrix[0, 2].A22 = Math.Pow(10.0, 50.0);

            // формуємо загальний блочний вектор

            Vector[] vector = new Vector[2 * n + 1];

            for (int i = 0; i <= 2 * n; i++)
            {
                vector[i] = new Vector(f[2 * i], f[2 * i + 1]);
            }

            //Розв'язуємо СЛАР з блочно-п'ятидіагональною матрицею
            Vector[] res = FiveDiagonalLowerUpperMethod(matrix, vector);


            for (int i = 0; i <= 2 * n; i++)
            {
                u[i] = res[i].A1;
                p[i] = res[i].A2;

            }

            Console.WriteLine("-------res------ ");
            for (int i = 0; i <= 2 * n; i++)
            {
                Console.WriteLine(res[i].A1);
                Console.WriteLine(res[i].A2);
                Console.WriteLine(" ");
            }
            Console.WriteLine(" ");

        }



        public static Vector[] FiveDiagonalLowerUpperMethod(Matrix[,] A, Vector[] F)
        {
            int n = F.Length;

            Matrix[] alpha = new Matrix[n];
            Matrix[] beta = new Matrix[n];
            Matrix[] gama = new Matrix[n];
            Matrix[] sigma = new Matrix[n];
            Matrix[] etha = new Matrix[n];

            //формуємо вектори
            for (int i = 0; i < n; ++i)
            {
                if (i >= 2)
                    alpha[i] = A[1, 0];
                if (i >= 1)
                {
                    beta[i] = A[i, 1];
                    if (i >= 2)
                        beta[i] = beta[i] - A[i, 0] * sigma[i - 2];
                }
                gama[i] = A[i, 2];

                if (i >= 1)
                    gama[i] = gama[i] - beta[i] * sigma[i - 1];
                if (i >= 2)
                    gama[i] = gama[i] - A[i, 0] * etha[i - 2];
                if (i <= n - 2)
                {
                    Matrix mult = A[i, 3];
                    if (i >= 1)
                        mult = mult - beta[i] * etha[i - 1];
                    sigma[i] = (gama[i].InvertedMatrix()) * mult;

                }
                if (i <= n - 3)
                {
                    etha[i] = (gama[i].InvertedMatrix()) * A[i, 4];
                }

            }


            Vector[] v = new Vector[n];
            for (int i = 0; i < n; i++)
            {
                Vector mult = F[i];
                if (i >= 1)
                    mult = mult - beta[i] * v[i - 1];

                if (i >= 2)
                    mult = mult - A[i, 0] * v[i - 2];

                v[i] = (gama[i].InvertedMatrix()) * mult;
            }
            Vector[] w = new Vector[n];

            for (int i = n - 1; i >= 0; --i)
            {
                w[i] = v[i];

                if (i < n - 1)
                    w[i] = w[i] - sigma[i] * w[i + 1];

                if (i < n - 2)
                    w[i] = w[i] - etha[i] * w[i + 2];
            }

            return w;
        }
    

        public double U_L2_Norm()
        {

            double norm = 0;

            for (int i = 0; i < n; i++)
            {
                norm += IntegrateGauss((x) => ((u[2 * i ] * fi[2 * i ](x) + u[2*i+1] * fi[2 * i + 1](x) + u[2*i + 2] * fi[2*i + 2](x)) *
                    (u[2 * i] * fi[2 * i](x) + u[2 * i + 1] * fi[2 * i + 1](x) + u[2 * i + 2] * fi[2 * i + 2](x))), xi[i], xi[i + 1]);

            }

            return Math.Sqrt(norm);

        }

        public double P_L2_Norm()
        {


            double norm = 0;

            for (int i = 0; i < n; i++)
            {
                norm += IntegrateGauss((x) => ((p[2 * i] * fi[2 * i](x) + p[2 * i + 1] * fi[2 * i + 1](x) + p[2 * i + 2] * fi[2 * i + 2](x)) *
                    (p[2 * i] * fi[2 * i](x) + p[2 * i + 1] * fi[2 * i + 1](x) + p[2 * i + 2] * fi[2 * i + 2](x))), xi[i], xi[i + 1]);

            }

            return Math.Sqrt(norm);

        }

      

    }
}
    