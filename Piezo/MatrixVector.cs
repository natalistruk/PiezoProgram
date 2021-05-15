using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piezo
{
    class Matrix : ICloneable
    {
        private double a11 = 0;
        private double a12 = 0;
        private double a21 = 0;
        private double a22 = 0;

        public Matrix() { }
        public Matrix(double a1, double a2, double a3, double a4)
        {
            a11 = a1;
            a12 = a2;
            a21 = a3;
            a22 = a4;
        }
        public double A11
        {
            get { return a11; }
            set { a11 = value; }
        }
        public double A12
        {
            get { return a12; }
            set { a12 = value; }
        }
        public double A21
        {
            get { return a21; }
            set { a21 = value; }
        }
        public double A22
        {
            get { return a22; }
            set { a22 = value; }
        }
        public void PrintMatrix()
        {
            Console.WriteLine(a11 + " " + a12);
            Console.WriteLine(a21 + " " + a22);
        }
        public Matrix InvertedMatrix()
        {
            double det = a11 * a22 - a12 * a21;
            if (det == 0)
            {
                return null;
            }
            else
            {
                double a = a22 / det;
                double b = -a12 / det;
                double c = -a21 / det;
                double d = a11 / det;
                return new Matrix(a, b, c, d);
            }
        }

        public static Matrix operator *(Matrix B, Matrix C)
        {
            Matrix D = new Matrix();

            D.A11 = B.A11 * C.A11 + B.A12 * C.A21;
            D.A12 = B.A11 * C.A12 + B.A12 * C.A22;
            D.A21 = B.A21 * C.A11 + B.A22 * C.A21;
            D.A22 = B.A21 * C.A12 + B.A22 * C.A22;

            return D;
        }
        public static Vector operator *(Matrix B, Vector C)
        {
            Vector D = new Vector();

            D.A1 = B.A11 * C.A1 + B.A12 * C.A2;
            D.A2 = B.A21 * C.A1 + B.A22 * C.A2;

            return D;
        }
        public static Vector operator *(Vector B, Matrix A)
        {
            Vector newVector = new Vector();

            newVector.A1 = A.A11 * B.A1 + A.A21 * B.A2;
            newVector.A2 = A.A12 * B.A1 + A.A22 * B.A2;

            return newVector;
        }

        public static Matrix operator +(Matrix B, Matrix C)
        {
            Matrix D = new Matrix();

            D.A11 = B.A11 + C.A11;
            D.A12 = B.A12 + C.A12;
            D.A21 = B.A21 + C.A21;
            D.A22 = B.A22 + C.A22;

            return D;
        }

        public static Matrix operator -(Matrix B, Matrix C)
        {
            Matrix D = new Matrix();

            D.A11 = B.A11 - C.A11;
            D.A12 = B.A12 - C.A12;
            D.A21 = B.A21 - C.A21;
            D.A22 = B.A22 - C.A22;


            return D;
        }

        public static Matrix operator -(Matrix B)
        {
            Matrix C = new Matrix();

            C.A11 = -B.A11;
            C.A12 = -B.A12;
            C.A21 = -B.A21;
            C.A22 = -B.A22;

            return C;
        }
        public object Clone()
        {
            return new Matrix(this.a11, this.a12, this.a21, this.a22);
        }


    }
    public class Vector : ICloneable
    {
        private double a1 = 0;
        private double a2 = 0;

        public Vector() { }
        public Vector(double a, double b)
        {
            a1 = a;
            a2 = b;
        }
        public double A1
        {
            get { return a1; }
            set { a1 = value; }
        }
        public double A2
        {
            get { return a2; }
            set { a2 = value; }
        }
        public void PrintVector()
        {
            Console.WriteLine(a1 + " " + a2);
        }
        
        public static Vector operator +(Vector B, Vector C)
        {
            Vector D = new Vector();

            D.A1 = C.A1 + B.A1;
            D.A2 = C.A2 + B.A2;


            return D;
        }

        public static Vector operator -(Vector B, Vector C)
        {
            Vector D = new Vector();

            D.A1 = B.A1 - C.A1;
            D.A2 = B.A2 - C.A2;


            return D;
        }

        public static Vector operator -(Vector B)
        {
            Vector D = new Vector();

            D.A1 = -B.A1;
            D.A2 = -B.A2;


            return D;
        }

        public object Clone()
        {
            return new Vector(this.a1, this.a2);
        }


    }
}
