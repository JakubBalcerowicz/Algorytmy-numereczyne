using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Matrix
{
    class Fraction 
    { 
        private static long GCD(long a, long b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);
            for (; ; )
            {
                long remainder = a % b;
                if (remainder == 0) return b;
                a = b;
                b = remainder;
            };
        }
        
        public long Numerator, Denominator;
        public Fraction(string txt)
        {
            int size;
            string[] pieces = txt.Split(',');
            Numerator = long.Parse(pieces[1]);
            size = pieces[1].Length;
            Denominator = (long)Math.Pow(10, size);
            Simplify();
        }
        public Fraction(long numer, long denom)
        {
            Numerator = numer;
            Denominator = denom;
            Simplify();
        }
        public static Fraction operator *(Fraction a, Fraction b)
        {
            Fraction result1 = new Fraction(a.Numerator, b.Denominator);
            Fraction result2 = new Fraction(b.Numerator, a.Denominator);
            return new Fraction(
                result1.Numerator * result2.Numerator,
                result1.Denominator * result2.Denominator);
        }

        public static Fraction operator -(Fraction a)
        {
            return new Fraction(-a.Numerator, a.Denominator);
        }
        public static Fraction operator +(Fraction a, Fraction b)
        {
            long gcd_ab = GCD(a.Denominator, b.Denominator);

            long numer =
                a.Numerator * (b.Denominator / gcd_ab) +
                b.Numerator * (a.Denominator / gcd_ab);
            long denom =
                a.Denominator * (b.Denominator / gcd_ab);
            return new Fraction(numer, denom);
        }
        public static Fraction operator -(Fraction a, Fraction b)
        {
            return a + -b;
        }
        public static Fraction operator /(Fraction a, Fraction b)
        {
            return a * new Fraction(b.Denominator, b.Numerator);
        }
        private void Simplify()
        {
            if (Denominator < 0)
            {
                Numerator = -Numerator;
                Denominator = -Denominator;
            }

        long gcd_ab = GCD(Numerator, Denominator);
            Numerator = Numerator / gcd_ab;
            Denominator = Denominator / gcd_ab;
        }
        public override string ToString()
        {
            return Numerator.ToString() + "/" + Denominator.ToString();
        }
    }
    class Program
    {
        public static int LineCount()
        {
            var lineCount = 0;
            using (var reader = File.OpenText(@"matrixdouble.txt"))
            {
                while (reader.ReadLine() != null)
                {
                    lineCount++;
                }
            }
            return lineCount;
        }
        public static void DoubleArray(double SIZE)
        {
            double[] numbers = new double[16];
            var fileNumbers = File.ReadLines("matrixdouble.txt").Select(double.Parse);
            int index = 0;
            foreach (var number in fileNumbers)
            {
                numbers[index] = number;
                index++;
            }
            double[,] result = new double[4, 4];
            MyMatrix<double> MyMatrix = new MyMatrix<double>();
            int k = 0;
            for (int i = 0; i < SIZE; i++)
                for (int j = 0; j < SIZE; j++)
                {
                    {
                        double value = numbers[k];
                        MyMatrix.AddMatrix(value, i, j);
                        MyMatrix.AddWektor(value, i, j);
                        result[i, j] = value; 
                    }
                    k++;
                }
            MyMatrix.Multiply();
            MyMatrix.Drukuj();
           
        }
        public static void FloatArray(double SIZE)
        {
            float[] numbers = new float[100];
            var fileNumbers = File.ReadLines("matrixfloat.txt").Select(float.Parse);
            int index = 0;
            foreach (var number in fileNumbers)
            {
                numbers[index] = number;
                index++;
            }

            MyMatrix<float> kolekcja = new MyMatrix<float>();
            int k = 0;
            for (int i = 0; i < SIZE; i++)
                for (int j = 0; j < SIZE; j++)
                {
                    {
                        float value = (float)numbers[k];
                        kolekcja.AddMatrix(value, i, j);
                    }
                    k++;
                }
            kolekcja.Drukuj();
        }

        public static void MyArray(double SIZE)
        {
            string[] numbers = new string[100];
            
            MyMatrix<Fraction> kolekcja = new MyMatrix<Fraction>();
            int k = 0;
            for (int i = 0; i < SIZE; i++)
                for (int j = 0; j < SIZE; j++)
                {
                    {
                        Fraction value = new Fraction(numbers[k]);
                        kolekcja.AddMatrix(value, i, j);
                    }
                    k++;
                }
           // Console.WriteLine(kolekcja.Suma());
        }
        static void Main(string[] args)
        {

            double SIZE = Math.Sqrt(LineCount());
            //MyArray(SIZE);
            // Console.WriteLine(SIZE);
             DoubleArray(SIZE);
            // FloatArray(SIZE);
        }
        class MyMatrix<T> 
        {

                T[,] array = new T[4, 4];
                T[,] wektor = new T[4, 4];
           // Multiply();

            // Drukuj();
            public void AddMatrix(T o, int i, int j)
            {
                array[i, j] = o;
            }
            public void AddWektor(T o, int i, int j)
            {
                wektor[i, j] = o;
            }   
            public  T[,]  Multiply()
            {
                if (array.GetLength(1) != wektor.GetLength(0))
                {
                    Console.WriteLine("Illegal matrix dimensions!");
                    
                }

                T[,] result = new T[array.GetLength(0), wektor.GetLength(1)];

                for (int i = 0; i < result.GetLength(0); i++)
                {
                    for (int j = 0; j < array.GetLength(0); j++)
                    {
                        for (int k = 0; k < array.GetLength(1); k++)
                        {
                             result[i, j] +=(dynamic)array[i, k] * (dynamic)wektor[k, j];
                            
                        }
                        Console.WriteLine("dupa {0}", result[i, j]);
                    }
                }
                
                return result;
                
            }
            
            public void Drukuj()
            {
                int rowLength = array.GetLength(0);
                int colLength = array.GetLength(1);

                for (int i = 0; i < rowLength; i++)
                {
                    for (int j = 0; j < colLength; j++)
                    {
                        Console.Write(string.Format("{0} ", array[i, j]));
                    }
                    Console.Write(Environment.NewLine + Environment.NewLine);
                }
            }
            


        }
    }
}
