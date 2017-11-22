using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Numerics;



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
            Numerator = long.Parse(pieces[1]);
            size = pieces[1].Length;
            Denominator = (long)Math.Pow(10, size);
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
            // MyMatrix<double> dupa = new MyMatrix<double>();
            //MyMatrix<double> [,]array = new MyMatrix<double>[4,4];
            // MyMatrix<double>[] test = new MyMatrix<double>[4];
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
            X = MyMatrix<double>.Gauss(SIZE,A);
            //X = A / X;      
            for(int i = 0; i <10; i++)
            {
                Console.WriteLine(X[i]);
            }
            MyMatrix<double> D = new MyMatrix<double>();
            MyMatrix<double> C = new MyMatrix<double>();
            //double z = A[0, 0] + B[0, 0];
            //Console.WriteLine(z);

            var watch = System.Diagnostics.Stopwatch.StartNew();
            double avrage = 0;

            X = MyMatrix<double>.Gauss(SIZE, A);

            //    C = B * A;     D = C * C;    //  (A*B)*C
            // D = A / B;     // C * wektor X (pierwsza kolumna macierzy B)
            //  C = A + B; D = C + C; D = D / B;

            watch.Stop();
            double elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine((elapsedMs / 1000 ));


            MyMatrix<double> result = new MyMatrix<double>();
            ////////////////////////// srednia wierszy
            for (int i = 0; i < Configuration.SIZE; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    for (int s = 0; s < Configuration.SIZE; s++)
                    {
                        result[i, j] += D[i, s];
                    }
                }
            }
            /////////////////srednia calej macierzy
            for(int i = 0; i < Configuration.SIZE; i++)
            {
                avrage += result[i, 0];
            }
            avrage = avrage / 10;
       //     Console.WriteLine(avrage);      
            for (int i = 0; i < Configuration.SIZE; i++)
             {
                 for (int j = 0; j < Configuration.SIZE; j++)
                 {
                     //Console.Write(string.Format("{0} ", A[i, j]));
                 }
               //  Console.Write(Environment.NewLine + Environment.NewLine);
             }        
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
            MyMatrix<float> D = new MyMatrix<float>();
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
                         //   A[i, SIZE] = value;
                          //  B[i, SIZE] = value;
                            X[i] = value;
                            
                        }
                    }
                    k++;
                }
            }
            MyMatrix<float> C = new MyMatrix<float>();
            // double z = A[0, 0] + B[0, 0];
            // Console.WriteLine(z);

            var watch = System.Diagnostics.Stopwatch.StartNew();
                C = B * A;     D = C * C;    //  (A*B)*C
           //  D = A / X;     // C * wektor X (pierwsza kolumna macierzy B)
             // C = A + B; D = C + C; D = D / X;
            watch.Stop();
            
            double elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(elapsedMs / 1000);

            for (int i = 0; i < Configuration.SIZE; i++)
            {
                for (int j = 0; j < Configuration.SIZE; j++)
                {
              //      Console.Write(string.Format("{0} ", D[i,j]));
                }
               //   Console.Write(Environment.NewLine + Environment.NewLine);
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
                            //   A[i, SIZE] = value;
                            //  B[i, SIZE] = value;
                            X[i] = value;

                        }

                    }
                    k++;
                }
            MyMatrix<Fraction> C = new MyMatrix<Fraction>();
            MyMatrix<Fraction> result = new MyMatrix<Fraction>();

           


            var watch = System.Diagnostics.Stopwatch.StartNew();
            // C = B * A; D = C * C;    //  (A*B)*C
            //  D = A / X;     // C * wektor X (pierwsza kolumna macierzy B)
            // C = A + B; D = C + C; D = D / X;
            C = A;
          //  C = A * B;
            //// mnzoenie przez wektor 

            for (int i = 0; i < Configuration.SIZE; i++)
            {
                for (int j = 0; j < Configuration.SIZE; j++)
                {
                    for (int s = 0; s < Configuration.SIZE; s++)
                    {
                        result[i, j] = C[i, 0] * B[0, 0] + C[i, 1] * B[1, 0] + C[i, 2] * B[2, 0] + C[i, 3] * B[3, 0] +
                                       C[i, 4] * B[4, 0] + C[i, 5] * B[5, 0] + C[i, 6] * B[6, 0] + C[i, 7] * B[7, 0] +
                                       C[i, 8] * B[8, 0] + C[i, 9] * B[9, 0];

                    }
                }
            }

            watch.Stop();

            double elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(elapsedMs / 1000);

            /////////////////////////////////////mnozenie  (A+B) * B 

         

            MyMatrix<Fraction> avrage = new MyMatrix<Fraction>();
            Fraction dzilenik = new Fraction(1, 10);
            Fraction srednia = new Fraction(1, 1);
            for (int i = 0; i < Configuration.SIZE; i++)         // srednia wiersza
            {
                for (int j = 0; j < 1; j++)
                {
                    for (int s = 0; s < Configuration.SIZE; s++)
                    {
                        avrage[i, j] = (result[i, 0] + result[i, 1] + result[i, 2] + result[i, 3] + result[i, 4] + result[i, 5] +
                                       result[i, 6] + result[i, 7] + result[i, 8] + result[i, 9])*dzilenik;

                    }
                }
            }
           
            srednia = (avrage[0, 0] + avrage[1, 0] + avrage[2, 0] + avrage[3, 0] + avrage[4, 0] + avrage[5, 0] +
                                       avrage[6,0] + avrage[7,0] + avrage[8,0] + avrage[9,0]) * dzilenik;   

            Console.WriteLine("Srednia {0}",srednia);


            for (int  i = 0; i < Configuration.SIZE; i++)
            {
              //  for (int j = 0; j < Configuration.SIZE; j++)
                {
                    Console.Write(string.Format("{0} ", result[i, 0]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
        }
        static void Main(string[] args)
        {
           // MyArray(Configuration.SIZE);
            // Console.WriteLine(SIZE);
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
