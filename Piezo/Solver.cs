using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piezo
{
    public delegate double Function(double x);
    class Solver
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

        //масив функцій куранта
        private Function[] fiX;

        //масив похідних від функцій Куранта
        private Function[] fiDerivativeX;

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
            FormFiX();
            FormFiDerivativeX();

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

        private void FormX()
        {
            xi = new double[n];
            xi[0] = 0;
            for (int i = 1; i < n; i++)
            {
                xi[i] = xi[i - 1] + h;

            }
        }
        private void FormFiX()
        {
            fiX = new Function[n + 1];

            for (int i = 0; i <= n; i++)
            {
                Courant courant = new Courant(l, i, n);
                fiX[i] = new Function(courant.fi);

            }
        }
        private void FormFiDerivativeX()
        {
            fiDerivativeX = new Function[n];

            for (int i = 0; i < n; i++)
            {
                Courant courant = new Courant(l, i, n);
                fiDerivativeX[i] = new Function(courant.fiDerivative);
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


        private double[,] FormMatrix(double f)
        {
            double[,] a = new double[n, 3];


            a[0, 0] = 0;

            a[0, 1] = IntegrateGauss((x) => (f * fiDerivativeX[0](x) * fiDerivativeX[0](x)), xi[0], xi[1]);
            a[0, 2] = IntegrateGauss((x) => (f * fiDerivativeX[0](x) * fiDerivativeX[1](x)), xi[0], xi[1]);

            for (int i = 1; i < n - 1; i++)
            {
                a[i, 0] = IntegrateGauss((x) => (f * fiDerivativeX[i - 1](x) * fiDerivativeX[i](x)), xi[i - 1], xi[i]);
                a[i, 1] = IntegrateGauss((x) => (f * fiDerivativeX[i](x) * fiDerivativeX[i](x)), xi[i - 1], xi[i]);
                a[i, 1] += IntegrateGauss((x) => (f * fiDerivativeX[i](x) * fiDerivativeX[i](x)), xi[i], xi[i + 1]);
                a[i, 2] = IntegrateGauss((x) => (f * fiDerivativeX[i](x) * fiDerivativeX[i + 1](x)), xi[i], xi[i + 1]);

            }

            a[n - 1, 0] = IntegrateGauss((x) => (f * fiDerivativeX[n - 2](x) * fiDerivativeX[n - 1](x)), xi[n - 2], xi[n - 1]);
            a[n - 1, 1] = IntegrateGauss((x) => (f * fiDerivativeX[n - 1](x) * fiDerivativeX[n - 1](x)), xi[n - 2], xi[n - 1]);
            a[n - 1, 2] = 0;

            return a;
        }


        public void Solve()
        {
            double b = IntegrateGauss((x) => (ro * fiDerivativeX[0](x) * fiDerivativeX[0](x)), xi[0], xi[1]);
            Console.WriteLine(b);
            u = new double[n];
            p = new double[n];
            double[,] C = FormMatrix(c);
            double[,] E = FormMatrix(e);
            double[,] G = FormMatrix(g);
            double[,] mE = FormMatrix(-e);


            Console.WriteLine("-----Matrix C----");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write($"{C[i, j]}  ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();



            Console.WriteLine("-----Matrix E----");

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write($"{E[i, j]}  ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();



            Console.WriteLine("-----Matrix -E ----");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write($"{mE[i, j]}  ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();



            Console.WriteLine("-----Matrix G----");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write($"{G[i, j]}  ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();



            double[,] m = new double[2 * n, 2 * n];

            for (int i = 0; i < 2 * n; i++)
            {
                for (int k = 0; k < 2 * n; k++)
                {
                    m[i, k] = 0;

                }

            }

            //формуємо матрицю
            for (int i = 0; i <= 2 * n - 1; i += 2)
            {
                if (i >= 2)
                {
                    m[i, i - 2] = C[i / 2, 0];
                    m[i, i - 1] = E[i / 2, 0];

                    m[i + 1, i - 2] = mE[i / 2, 0];
                    m[i + 1, i - 1] = G[i / 2, 0];
                }

                m[i, i] = C[i / 2, 1];
                m[i, i + 1] = E[i / 2, 1];

                m[i + 1, i] = mE[i / 2, 1];
                m[i + 1, i + 1] = G[i / 2, 1];


                if (i != 2 * n - 2)
                {
                    m[i, i + 2] = C[i / 2, 2];
                    m[i, i + 3] = E[i / 2, 2];

                    m[i + 1, i + 2] = mE[i / 2, 2];
                    m[i + 1, i + 3] = G[i / 2, 2];

                }

            }

            Console.WriteLine("-----Matrix m----");
            for (int i = 0; i < 2 * n; i++)
            {
                for (int k = 0; k < 2 * n; k++)
                {

                    Console.Write($"{(m[i, k])}  ");
                }
                Console.WriteLine();

            }

            //формуємо вектор
            double[] f = new double[2 * n];

            double[] ll = new double[n];
            double[] r = new double[n];
            for (int i = 0; i < n; i++)
            {
                ll[i] = r[i] = 0;

            }
            ll[n - 1] = sigma;
            r[n - 1] = D;




            for (int i = 0; i < 2 * n; i += 2)
            {
                f[i] = ll[i / 2];
                f[i + 1] = r[i / 2];

            }
            Console.WriteLine("-------Vector f ----- ");

            for (int i = 0; i < 2 * n; i++)
            {
                Console.WriteLine(f[i]);
            }
            Console.WriteLine(" ");


            Console.WriteLine("------ Matrix matrix-----");
            Matrix[,] matrix = new Matrix[n, 3];
            for (int i = 0; i < n; i++)
            {
                if (i != 0)
                {
                    matrix[i, 0] = new Matrix(m[2 * i, 2 * i - 2], m[2 * i, 2 * i - 1], m[2 * i + 1, 2 * i - 2], m[2 * i + 1, 2 * i - 1]);
                    matrix[i, 0].PrintMatrix();
                    Console.WriteLine(" ");
                }

                matrix[i, 1] = new Matrix(m[2 * i, 2 * i], m[2 * i, 2 * i + 1], m[2 * i + 1, 2 * i], m[2 * i + 1, 2 * i + 1]);
                matrix[i, 1].PrintMatrix();
                Console.WriteLine(" ");
                if (i != n - 1)
                {
                    matrix[i, 2] = new Matrix(m[2 * i, 2 * i + 2], m[2 * i, 2 * i + 3], m[2 * i + 1, 2 * i + 2], m[2 * i + 1, 2 * i + 3]);
                    matrix[i, 2].PrintMatrix();
                    Console.WriteLine(" ");
                }


            }
            Console.WriteLine(" ");


            Vector[] vector = new Vector[n];

            for (int i = 0; i < n; i++)
            {
                vector[i] = new Vector(f[2 * i], f[2 * i + 1]);
            }

            Vector[] res = BlockTridiagonalGauss(matrix, vector);


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


        public static Vector[] BlockTridiagonalGauss(Matrix[,] A, Vector[] F)
        {

            int N = F.Length;

            Vector[] res = new Vector[N];

            //Прямий хід
            Matrix restMatrix = new Matrix();
            Vector restVector = new Vector();
            for (int i = 0; i < N - 1; i++)
            {
                A[i, 1] = A[i, 1] + restMatrix;
                F[i] = F[i] + restVector;

                Matrix A11 = (Matrix)A[i, 1].Clone();
                Matrix A12 = (Matrix)A[i, 2].Clone();
                Matrix A21 = (Matrix)A[i + 1, 0].Clone();

                Vector V1 = (Vector)F[i].Clone();
                Vector V2 = (Vector)F[i + 1].Clone();

                A[i, 1] = new Matrix(1, 0, 0, 1);
                A[i + 1, 0] = new Matrix();
                A[i, 2] = A11.InvertedMatrix() * A12;
                restMatrix = (-A21 * (A11.InvertedMatrix())) * A12;

                F[i] = A11.InvertedMatrix() * V1;
                restVector = (-A21 * (A11.InvertedMatrix())) * V1;


            }
            A[N - 1, 1] = A[N - 1, 1] + restMatrix;
            F[N - 1] = F[N - 1] + restVector;

            //Обернений хід
            res[N - 1] = (A[N - 1, 1].InvertedMatrix()) * F[N - 1];
            for (int i = N - 2; i >= 0; --i)
            {
                res[i] = F[i] - A[i, 2] * res[i + 1];

            }

            return res;



        }

    }
}
    