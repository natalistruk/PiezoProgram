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
            xi = new double[n];
            xi[0] = 0;
            for (int i = 1; i < n; i++)
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


        private double[,] FormM()
        {
            double[,] a = new double[2 * n + 1, 5];

            Function func;

            a[0, 0] = 0;
            a[0, 1] = 0;

            a[0, 2] = IntegrateGauss((x) => (ro * fi[0](x) * fi[0](x)), xi[0], xi[1]);
            a[0, 3] = IntegrateGauss((x) => (ro * fi[0](x) * fi[1](x)), xi[0], xi[1]);
            a[0, 4] = IntegrateGauss((x) => (ro * fi[0](x) * fi[2](x)), xi[0], xi[1]);



            a[1, 0] = 0;
            a[1, 1] = IntegrateGauss((x) => (ro * fi[0](x) * fi[1](x)), xi[0], xi[1]);

            a[1, 2] = IntegrateGauss((x) => (ro * fi[1](x) * fi[1](x)), xi[0], xi[1]);
            a[1, 3] = IntegrateGauss((x) => (ro * fi[1](x) * fi[2](x)), xi[0], xi[1]);
            a[1, 4] = 0;
            for (int i = 2; i < 2 * n - 1; i++)
            {
                if (i % 2 == 0)
                {
                    a[i, 0] = IntegrateGauss((x) => (ro * fi[i](x) * fi[i - 2](x)), xi[i / 2 - 1], xi[i / 2]);
                    a[i, 1] = IntegrateGauss((x) => (ro * fi[i](x) * fi[i - 1](x)), xi[i / 2 - 1], xi[i / 2]);

                    a[i, 2] = IntegrateGauss((x) => (ro * fi[i](x) * fi[i](x)), xi[i / 2 - 1], xi[i / 2]);
                    a[i, 2] += IntegrateGauss((x) => (ro * fi[i](x) * fi[i](x)), xi[i / 2], xi[i / 2 + 1]);

                    a[i, 3] = IntegrateGauss((x) => (ro * fi[i](x) * fi[i + 1](x)), xi[i / 2], xi[i / 2 + 1]);
                    a[i, 4] = IntegrateGauss((x) => (ro * fi[i](x) * fi[i + 2](x)), xi[i / 2], xi[i / 2 + 1]);
                }
                else
                {
                    a[i, 0] = 0;
                    a[i, 1] = IntegrateGauss((x) => (ro * fi[i](x) * fi[i - 1](x)), xi[i / 2], xi[(i + 1) / 2]);

                    a[i, 2] = IntegrateGauss((x) => (ro * fi[i](x) * fi[i](x)), xi[i / 2], xi[(i + 1) / 2]);
                    a[i, 3] = IntegrateGauss((x) => (ro * fi[1](x) * fi[i + 1](x)), xi[i / 2], xi[(i + 1) / 2]);
                    a[i, 4] = 0;
                }

            }

            a[2 * n - 1, 0] = 0;
            a[2 * n - 1, 1] = IntegrateGauss((x) => (ro * fi[2 * n - 1](x) * fi[2 * n - 2](x)), xi[n - 1], xi[n]);
            a[2 * n - 1, 2] = IntegrateGauss((x) => (ro * fi[2 * n - 1](x) * fi[2 * n - 1](x)), xi[n - 1], xi[n]);
            a[2 * n - 1, 3] = IntegrateGauss((x) => (ro * fi[2 * n - 1](x) * fi[2 * n](x)), xi[n - 1], xi[n]);
            a[2 * n - 1, 4] = 0;



            a[2 * n, 0] = IntegrateGauss((x) => (ro * fi[2 * n](x) * fi[2 * n - 2](x)), xi[n - 1], xi[n]);
            a[2 * n, 1] = IntegrateGauss((x) => (ro * fi[2 * n](x) * fi[2 * n - 1](x)), xi[n - 1], xi[n]);
            a[2 * n, 2] = IntegrateGauss((x) => (ro * fi[2 * n](x) * fi[2 * n](x)), xi[n - 1], xi[n]);
            a[2 * n, 3] = 0;
            a[2 * n, 4] = 0;



            return a;

        }
        private double[,] FormOther(Function f)
        {
            double[,] a = new double[2 * n + 1, 5];


            a[0, 0] = 0;
            a[0, 1] = 0;

            a[0, 2] = IntegrateGauss((x) => (f(x) * fiDerivative[0](x) * fiDerivative[0](x)), xi[0], xi[1]);
            a[0, 3] = IntegrateGauss((x) => (f(x) * fiDerivative[0](x) * fiDerivative[1](x)), xi[0], xi[1]);
            a[0, 4] = IntegrateGauss((x) => (f(x) * fiDerivative[0](x) * fiDerivative[2](x)), xi[0], xi[1]);



            a[1, 0] = 0;
            a[1, 1] = IntegrateGauss((x) => (f(x) * fiDerivative[0](x) * fiDerivative[1](x)), xi[0], xi[1]);

            a[1, 2] = IntegrateGauss((x) => (f(x) * fiDerivative[1](x) * fiDerivative[1](x)), xi[0], xi[1]);
            a[1, 3] = IntegrateGauss((x) => (f(x) * fiDerivative[1](x) * fiDerivative[2](x)), xi[0], xi[1]);
            a[1, 4] = 0;
            for (int i = 2; i < 2 * n - 1; i++)
            {
                if (i % 2 == 0)
                {
                    a[i, 0] = IntegrateGauss((x) => (f(x) * fiDerivative[i](x) * fiDerivative[i - 2](x)), xi[i / 2 - 1], xi[i / 2]);
                    a[i, 1] = IntegrateGauss((x) => (f(x) * fiDerivative[i](x) * fiDerivative[i - 1](x)), xi[i / 2 - 1], xi[i / 2]);

                    a[i, 2] = IntegrateGauss((x) => (f(x) * fiDerivative[i](x) * fiDerivative[i](x)), xi[i / 2 - 1], xi[i / 2]);
                    a[i, 2] += IntegrateGauss((x) => (f(x) * fiDerivative[i](x) * fiDerivative[i](x)), xi[i / 2], xi[i / 2 + 1]);

                    a[i, 3] = IntegrateGauss((x) => (f(x) * fiDerivative[i](x) * fiDerivative[i + 1](x)), xi[i / 2], xi[i / 2 + 1]);
                    a[i, 4] = IntegrateGauss((x) => (f(x) * fiDerivative[i](x) * fiDerivative[i + 2](x)), xi[i / 2], xi[i / 2 + 1]);
                }
                else
                {
                    a[i, 0] = 0;
                    a[i, 1] = IntegrateGauss((x) => (f(x) * fiDerivative[i](x) * fiDerivative[i - 1](x)), xi[i / 2], xi[(i + 1) / 2]);

                    a[i, 2] = IntegrateGauss((x) => (f(x) * fiDerivative[i](x) * fiDerivative[i](x)), xi[i / 2], xi[(i + 1) / 2]);
                    a[i, 3] = IntegrateGauss((x) => (f(x) * fiDerivative[i](x) * fiDerivative[i + 1](x)), xi[i / 2], xi[(i + 1) / 2]);
                    a[i, 4] = 0;
                }

            }

            a[2 * n - 1, 0] = 0;
            a[2 * n - 1, 1] = IntegrateGauss((x) => (f(x) * fiDerivative[2 * n - 1](x) * fiDerivative[2 * n - 2](x)), xi[n - 1], xi[n]);
            a[2 * n - 1, 2] = IntegrateGauss((x) => (f(x) * fiDerivative[2 * n - 1](x) * fiDerivative[2 * n - 1](x)), xi[n - 1], xi[n]);
            a[2 * n - 1, 3] = IntegrateGauss((x) => (f(x) * fiDerivative[2 * n - 1](x) * fiDerivative[2 * n](x)), xi[n - 1], xi[n]);
            a[2 * n - 1, 4] = 0;



            a[2 * n, 0] = IntegrateGauss((x) => (f(x) * fiDerivative[2 * n](x) * fiDerivative[2 * n - 2](x)), xi[n - 1], xi[n]);
            a[2 * n, 1] = IntegrateGauss((x) => (f(x) * fiDerivative[2 * n](x) * fiDerivative[2 * n - 1](x)), xi[n - 1], xi[n]);
            a[2 * n, 2] = IntegrateGauss((x) => (f(x) * fiDerivative[2 * n](x) * fiDerivative[2 * n](x)), xi[n - 1], xi[n]);
            a[2 * n, 3] = 0;
            a[2 * n, 4] = 0;



            return a;

        }




        public void Solve()
        {
            double b = IntegrateGauss((x) => (ro * fiDerivative[0](x) * fiDerivative[0](x)), xi[0], xi[1]);
            Console.WriteLine(b);
            u = new double[2*n+1];
            p = new double[2*n+1];

            //Початкові умови
            for (int i = 0; i <= 2 * n; i++)
            {
                u[i] = 0;
                p[i] = 0;
            }

            double[,] M = coef.FormM();
            double[,] A = coef.FormA();
            double[,] C = coef.FormC();
            double[,] E = coef.FormE();
            double[,] S = coef.FormS();


            



            
            //формуємо вектор
            double[] f = new double[2 * n+1];

            double[] ll = new double[2*n];
            double[] r = new double[2*n];
            for (int i = 0; i < n; i++)
            {
                ll[i] = r[i] = 0;

            }
            ll[2*n - 1] = sigma;
            r[2*n - 1] = D;




            for (int i = 0; i < 4 * n+1; i += 2)
            {
                f[i] = ll[i / 2];
                f[i + 1] = r[i / 2];

            }
            


            Matrix[,] matrix = new Matrix[2 * n + 1, 5];
            


            Vector[] vector = new Vector[2*n+1];

            for (int i = 0; i < 2*n; i++)
            {
                vector[i] = new Vector(f[2 * i], f[2 * i + 1]);
            }

            Vector[] res = FivediagonalMatrixMethod(matrix, vector);


            for (int i = 0; i < n; i++)
            {
                u[i] = res[i].A1;
                p[i] = res[i].A2;

            }
            Console.WriteLine("-------res------ ");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(res[i].A1);
                Console.WriteLine(res[i].A2);
                Console.WriteLine(" ");
            }
            Console.WriteLine(" ");




        }


        public static Vector[] FivediagonalMatrixMethod(Matrix[,] A, Vector[] F)
        {
            int N = F.Length - 1;

            Matrix[] a = new Matrix[N + 1];
            Matrix[] b = new Matrix[N + 1];
            Matrix[] c = new Matrix[N + 1];
            Matrix[] d = new Matrix[N + 1];
            Matrix[] e = new Matrix[N + 1];

            //формуємо вектори
            for (int i = 0; i <= N; ++i)
            {
                a[i] = A[i, 0];
                b[i] = -A[i, 1];
                c[i] = A[i, 2];
                d[i] = -A[i, 3];
                e[i] = A[i, 4];

            }

            //рахуємо коефіцієнти прогонки
            Matrix[] p = new Matrix[N + 1];
            Matrix[] q = new Matrix[N];
            Vector[] r = new Vector[N + 2];

            Matrix[] D = new Matrix[N + 1];

            p[1] = (c[0].InvertedMatrix()) * d[0];
            q[1] = (c[0].InvertedMatrix()) * e[0];
            r[1] = (c[0].InvertedMatrix()) * F[0];
            D[1] = c[1] - b[1] * p[1];

            p[2] = (D[1].InvertedMatrix()) * (d[1] - q[1] * b[1]);
            r[2] = (D[1].InvertedMatrix()) * (F[1] + b[1] * r[1]);
            q[2] = (D[1].InvertedMatrix()) * e[1];
            //D[2] = c[2] - a[2] * q[1] + p[2] * (a[2] * p[1] - b[2]); 


            for (int i = 2; i <= N; ++i)
            {
                D[i] = c[i] - a[i] * q[i - 1] + p[i] * (a[i] * p[i - 1] - b[i]);
                if (i <= N - 1)
                    p[i + 1] = (D[i].InvertedMatrix()) * (d[i] + q[i] * (a[i] * p[i - 1] - b[i]));

                r[i + 1] = (D[i].InvertedMatrix()) * (F[i] - a[i] * r[i - 1] - (a[i] * p[i - 1] - b[i]) * r[i]);

                if (i <= N - 2)
                    q[i + 1] = (D[i].InvertedMatrix()) * e[i];
            }

            //шукаємо розвязки
            Vector[] u = new Vector[N + 1];
            u[N] = r[N + 1];
            u[N - 1] = p[N] * u[N] + r[N];

            for (int i = N - 2; i >= 0; --i)
            {
                u[i] = p[i + 1] * u[i + 1] - q[i + 1] * u[i + 2] + r[i + 1];
            }



            return u;

        }



    }

}

    