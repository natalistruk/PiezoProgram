using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piezo.CoefT
{
    public class ConstantMode : Coef
    {
        private Solver solverInstance;

        public ConstantMode(double ro, double c, double e, double g, double l, int n, Solver solverInstance) : base(ro, c, e, g, l, n)
        {
            this.solverInstance = solverInstance;
        }

        private double[,] FormElementMatrix()
        {
            double[,] m = new double[3, 3];

            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    m[i, j] = ro * IntegrateGauss(((x) => (solverInstance.Fi[i](x) * solverInstance.Fi[j](x))), solverInstance.X[0], solverInstance.X[1]);
                }
            }

            return m;


        }

        private double[,] FormElementDerivativeMatrix(double f)
        {
            double[,] m = new double[3, 3];

            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    m[i, j] = f * IntegrateGauss(((x) => (solverInstance.FiDerivative[i](x) * solverInstance.FiDerivative[j](x))), solverInstance.X[0], solverInstance.X[1]);
                }
            }

            return m;

        }


        public override double[,] FormM()
        {
            double[,] elementMatrix = FormElementMatrix();

            double[,] m = new double[2 * n + 1, 5];

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < 5; ++j)
                {
                    if (i == 0)
                        m[2 * i, j] = 0;

                    m[2 * i + 1, j] = 0;
                    m[2 * i + 2, j] = 0;
                }

                m[2 * i, 2] += elementMatrix[0, 0];
                m[2 * i, 3] += elementMatrix[0, 1];
                m[2 * i, 4] += elementMatrix[0, 2];

                m[2 * i + 1, 1] += elementMatrix[1, 0];
                m[2 * i + 1, 2] += elementMatrix[1, 1];
                m[2 * i + 1, 3] += elementMatrix[1, 2];

                m[2 * i + 2, 0] += elementMatrix[2, 0];
                m[2 * i + 2, 1] += elementMatrix[2, 1];
                m[2 * i + 2, 2] += elementMatrix[2, 2];

            }

            return m;
        }

        public override double[,] FormA()
        {
            double[,] elementMatrix = FormElementDerivativeMatrix(g);

            double[,] m = new double[2 * n + 1, 5];

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < 5; ++j)
                {
                    if (i == 0)
                        m[2 * i, j] = 0;

                    m[2 * i + 1, j] = 0;
                    m[2 * i + 2, j] = 0;
                }

                m[2 * i, 2] += elementMatrix[0, 0];
                m[2 * i, 3] += elementMatrix[0, 1];
                m[2 * i, 4] += elementMatrix[0, 2];

                m[2 * i + 1, 1] += elementMatrix[1, 0];
                m[2 * i + 1, 2] += elementMatrix[1, 1];
                m[2 * i + 1, 3] += elementMatrix[1, 2];

                m[2 * i + 2, 0] += elementMatrix[2, 0];
                m[2 * i + 2, 1] += elementMatrix[2, 1];
                m[2 * i + 2, 2] += elementMatrix[2, 2];

            }

            return m;
        }

        public override double[,] FormE()
        {
            double[,] elementMatrix = FormElementDerivativeMatrix(e);

            double[,] m = new double[2 * n + 1, 5];

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < 5; ++j)
                {
                    if (i == 0)
                        m[2 * i, j] = 0;
                    m[2 * i + 1, j] = 0;
                    m[2 * i + 2, j] = 0;
                }

                m[2 * i, 2] += elementMatrix[0, 0];
                m[2 * i, 3] += elementMatrix[0, 1];
                m[2 * i, 4] += elementMatrix[0, 2];

                m[2 * i + 1, 1] += elementMatrix[1, 0];
                m[2 * i + 1, 2] += elementMatrix[1, 1];
                m[2 * i + 1, 3] += elementMatrix[1, 2];

                m[2 * i + 2, 0] += elementMatrix[2, 0];
                m[2 * i + 2, 1] += elementMatrix[2, 1];
                m[2 * i + 2, 2] += elementMatrix[2, 2];

            }

            return m;
        }

        public override double[,] FormC()
        {
            double[,] elementMatrix = FormElementDerivativeMatrix(c);

            double[,] m = new double[2 * n + 1, 5];

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < 5; ++j)
                {
                    if (i == 0)
                        m[2 * i, j] = 0;
                    m[2 * i + 1, j] = 0;
                    m[2 * i + 2, j] = 0;
                }

                m[2 * i, 2] += elementMatrix[0, 0];
                m[2 * i, 3] += elementMatrix[0, 1];
                m[2 * i, 4] += elementMatrix[0, 2];

                m[2 * i + 1, 1] += elementMatrix[1, 0];
                m[2 * i + 1, 2] += elementMatrix[1, 1];
                m[2 * i + 1, 3] += elementMatrix[1, 2];

                m[2 * i + 2, 0] += elementMatrix[2, 0];
                m[2 * i + 2, 1] += elementMatrix[2, 1];
                m[2 * i + 2, 2] += elementMatrix[2, 2];

            }

            return m;
        }

        public override double[,] FormG()
        {
            double[,] elementMatrix = FormElementDerivativeMatrix(g);

            double[,] m = new double[2 * n + 1, 5];

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < 5; ++j)
                {
                    if (i == 0)
                        m[2 * i, j] = 0;
                    m[2 * i + 1, j] = 0;
                    m[2 * i + 2, j] = 0;
                }

                m[2 * i, 2] += elementMatrix[0, 0];
                m[2 * i, 3] += elementMatrix[0, 1];
                m[2 * i, 4] += elementMatrix[0, 2];

                m[2 * i + 1, 1] += elementMatrix[1, 0];
                m[2 * i + 1, 2] += elementMatrix[1, 1];
                m[2 * i + 1, 3] += elementMatrix[1, 2];

                m[2 * i + 2, 0] += elementMatrix[2, 0];
                m[2 * i + 2, 1] += elementMatrix[2, 1];
                m[2 * i + 2, 2] += elementMatrix[2, 2];

            }

            return m;
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
    }
}
