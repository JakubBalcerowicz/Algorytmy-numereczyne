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
        public static void BigIntArray(double SIZE)
        {

            double[] numbers = new double[100];
            var fileNumbers = File.ReadLines("matrixbigint.txt");
            int index = 0;
            foreach (var number in fileNumbers)
            {
                numbers[index] = number;
                index++;
            }

            Kolekcja<double> kolekcja = new Kolekcja<double>();
            int k = 0;
            for (int i = 0; i < SIZE; i++)
                for (int j = 0; j < SIZE; j++)
                {
                    {
                        double value = numbers[k];
                        kolekcja.Dodaj(value, i, j);
                        Console.WriteLine(value);
                    }
                    k++;
                }
            kolekcja.Drukuj();
        }
        public static void DoubleArray(double SIZE)
        {
            
            double[] numbers = new double[100];
            var fileNumbers = File.ReadLines("matrixdouble.txt").Select(double.Parse);
            int index = 0;
            foreach (var number in fileNumbers)
            {
                numbers[index] = number;
                index++;
            }

            Kolekcja<double> kolekcja = new Kolekcja<double>();
            int k = 0;
            for (int i = 0; i < SIZE; i++)
                for (int j = 0; j < SIZE; j++)
                {
                    {
                        double value = numbers[k];
                        kolekcja.Dodaj(value, i, j);
                        Console.WriteLine(value);
                    }
                    k++;
                }
            kolekcja.Drukuj();
        }
        public static void FloatArray(double SIZE)
        {
            float[] numbers = new float[100];
            var fileNumbers = File.ReadLines("matrixfloat.txt").Select(float.Parse);
            int index = 0;
            foreach (var number in fileNumbers)
            {
                numbers[index] = number;
                Console.WriteLine(numbers[index]);
                index++;
            }

            Kolekcja<float> kolekcja = new Kolekcja<float>();
            int k = 0;
            for (int i = 0; i < SIZE; i++)
                for (int j = 0; j < SIZE; j++)
                {
                    {
                        float value = (float)numbers[k];
                        kolekcja.Dodaj(value, i, j);
                    }
                    k++;
                }
            kolekcja.Drukuj();
        }
        static void Main(string[] args)
        {

            double SIZE = Math.Sqrt(LineCount());
            Console.WriteLine(SIZE);
           // DoubleArray(SIZE);
            FloatArray(SIZE);
        }
    }
    class Kolekcja<T> where T : IComparable<T>
    {
        T[,] array = new T[10,10];
        public void Dodaj(T o,int i,int j)
        {
            array[i,j] = o;
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
      //  public T Suma()
      //  {
           // dynamic a = array[0];
           // dynamic b = array[1];
          //  return a + b;
       // }
 
    }
}
