using System;
using System.IO;
using System.Linq;
using System.Numerics;
/*---------------------------
 * Jakub Balcerowicz
 * Indeks: 238153
 * Grupa 1 Tester-Programista
 * --------------------------
 * */

namespace Matrix
{
    public static class Configuration
    {
        public const int SIZE = 400;
        public const int BIGSIZE = 160000;
    }
    class Fraction 
    { 
        private static BigInteger GCD(BigInteger a, BigInteger b)
        {
            a = Math.Abs((dynamic)a);
            b = Math.Abs((dynamic)b);
            for (; ; )
            {
                BigInteger remainder = a % b;
                if (remainder == 0) return b;
                a = b;
                b = remainder;
            };
        }     
        public BigInteger Numerator, Denominator;
        public Fraction(string txt)
        {
            int size;
            string[] pieces = txt.Split(new char[] {',','/'});
            Numerator = BigInteger.Parse(pieces[1]);
            size = pieces[1].Length;
            Denominator = (BigInteger)Math.Pow(10, size);
            Simplify();
        }
        public Fraction(BigInteger numer, BigInteger denom)
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
            BigInteger gcd_ab = GCD(a.Denominator, b.Denominator);

            BigInteger numer =
                a.Numerator * (b.Denominator / gcd_ab) +
                b.Numerator * (a.Denominator / gcd_ab);
            BigInteger denom =
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

       BigInteger gcd_ab = GCD(Numerator, Denominator);
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
            using (var reader = File.OpenText(@"matrix.txt"))
            {
                while (reader.ReadLine() != null)
                {
                    lineCount++;
                }
            }
            return lineCount;
        }
        public static void DoubleArray(int SIZE)
        {
            double[] numbers = new double[Configuration.BIGSIZE];
            var fileNumbers = File.ReadLines("matrix.txt").Select(double.Parse);
            int index = 0;
            foreach (var number in fileNumbers)
            {
                numbers[index] = number;
                index++;
            }
            MyMatrix<double> A = new MyMatrix<double>();
            MyMatrix<double> B = new MyMatrix<double>();
            MyMatrix<double> X = new MyMatrix<double>();
            int k = 0;
            for (int i = 0; i < SIZE; i++)
            {   
                for (int j = 0; j < SIZE; j++)
                {
                    {
                        double value = numbers[k];
                        A[i, j] = value;
                        B[i, j] = value;
                        if (k % SIZE == 0)
                        {
                            A[i, SIZE] = value;
                            X[i] = value;
                        }
                    }
                    k++;
                }
            }
            var watch = System.Diagnostics.Stopwatch.StartNew();
            X = MyMatrix<double>.Gauss(SIZE, A);
            watch.Stop();
            double elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine((elapsedMs / 1000 ));
     
        }
        public static void FloatArray(int SIZE)
        {
            float[] numbers = new float[Configuration.BIGSIZE];
            var fileNumbers = File.ReadLines("matrix.txt").Select(float.Parse);
            int index = 0;
            foreach (var number in fileNumbers)
            {
                numbers[index] = number;
                index++;
            }
            MyMatrix<float> A = new MyMatrix<float>();
            MyMatrix<float> B = new MyMatrix<float>();
            MyMatrix<float> X = new MyMatrix<float>();
            int k = 0;
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    {
                        float value = numbers[k];
                        A[i, j] = value;
                        B[i, j] = value;
                        if (k % SIZE == 0)
                        {
                            A[i, SIZE] = value;
                            X[i] = value;                          
                        }
                    }
                    k++;
                }
            }
        }
        public static void MyArray(int SIZE)
        {
            string[] numbers = new string[Configuration.BIGSIZE];
            var fileNumbers = File.ReadLines("matrix.txt");
            int index = 0;
            foreach (var number in fileNumbers)
            {
                numbers[index] = number;
                index++;
            }
            MyMatrix<Fraction> MyMatrix = new MyMatrix<Fraction>();
            MyMatrix<Fraction> A = new MyMatrix<Fraction>();
            MyMatrix<Fraction> B = new MyMatrix<Fraction>();
            MyMatrix<Fraction> X = new MyMatrix<Fraction>();
            int k = 0;
            for (int i = 0; i < SIZE; i++)
                for (int j = 0; j < SIZE; j++)
                {
                    {
                        Fraction value = new Fraction(numbers[k]);
                        A[i, j] = value;
                        B[i, j] = value;
                        if (k % SIZE == 0)
                        {
                            A[i, SIZE] = value;
                            X[i] = value;
                        }
                    }
                    k++;
                }
            var watch = System.Diagnostics.Stopwatch.StartNew();
            watch.Stop();
            double elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(elapsedMs / 1000);

        }
        static void Main(string[] args)
        {
             //MyArray(Configuration.SIZE);
             DoubleArray(Configuration.SIZE);
             //FloatArray(Configuration.SIZE);
        }
        class MyMatrix<T>
        {
            private T[,] arr = new T[Configuration.SIZE, Configuration.SIZE+1];          
            public  T this[int i, int j]
            {
                get
                {
                    return arr[i,j];
                }
                set
                {
                    arr[i,j] = value;
                }
            }
            private T[] x = new T[Configuration.SIZE];
            public T this[int i]
            {
                get
                {
                    return x[i];
                }
                set
                {
                    x[i] = value;
                }
            }
            public static MyMatrix<T> operator +(MyMatrix<T> a, MyMatrix<T> b)
            {
                MyMatrix<T> result = new MyMatrix<T>();
                for (int i = 0; i < Configuration.SIZE; i++)
                {
                    for (int j = 0; j < Configuration.SIZE; j++)
                    {
                        result[i, j] = (dynamic)a[i, j] + b[i, j];
                    }                    
                }
                return result;  
            }
           public static MyMatrix<T> operator *(MyMatrix<T> matrixA, MyMatrix<T> matrixB)
            {
                MyMatrix<T> result = new MyMatrix<T>();
                for (int i = 0; i < Configuration.SIZE; i++)
                {
                    for (int j = 0; j < Configuration.SIZE; j++)
                    {
                        for (int k = 0; k < Configuration.SIZE; k++)
                        {
                            result[i, j] += (dynamic)matrixA[i, k] * matrixB[k, j];
                        }
                    }
                }
                return result;
            }
            public static MyMatrix<T> operator /(MyMatrix<T> matrixA, MyMatrix<T> matrixB)
            {
                MyMatrix<T> result = new MyMatrix<T>();
                for (int i = 0; i < Configuration.SIZE; i++)
                {
                    for (int j = 0; j < Configuration.SIZE; j++)
                    { 
                      result[i] += (dynamic)matrixA[i, j] * matrixB[j];  
                    }
                }
                return result;
            }
            public static MyMatrix<T> Gauss(int n, MyMatrix<T> a)
            {
                MyMatrix<T> x = new MyMatrix<T>();
               // partial(a);
                for (int i = 0; i < n - 1; i++)
                {
                    for (int k = i + 1; k < n; k++)
                    {
                        double t = (dynamic)a[k, i] / a[i, i];
                        for (int j = 0; j <= n; j++)
                            a[k, j] = a[k, j] - (dynamic)t * a[i, j];
                    }
                }
                
                for (int i = n - 1; i >= 0; i--)
                {
                    x[i] = a[i, n];
                    for (int j = i + 1; j < n; j++)
                        if (j != i)
                          x[i] = x[i] - (dynamic)a[i, j] * x[j];
                    x[i] = (dynamic)x[i] / a[i, i];
                }
                return x;
            }
            public static void partial(MyMatrix<T> a)
            {
            for (int i = 0; i < Configuration.SIZE; i++)
                for (int k = i + 1; k < Configuration.SIZE; k++)
                   if (Math.Abs((dynamic)a[i, i]) < Math.Abs((dynamic)a[k, i]))
                   {
                        for (int j = 0; j <= Configuration.SIZE; j++)
                        {
                            double temp = (dynamic)a[i, j];
                            a[i, j] = a[k, j];
                            a[k, j] = (dynamic)temp;
                        }
                   }
            }
        }
    }
}
