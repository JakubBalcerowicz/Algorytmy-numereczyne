using System;
using System.Linq;
using System.IO;


namespace MatrixGenerator
{

    class Program
    {   
        public static class Configuration
        {
            public const int SIZE = 10;
            public const int BIGSIZE = 100;
        }
        public static void Datadouble()
        {
            double[] array = new double[Configuration.BIGSIZE];
            Random rnd = new Random();
            for (int i = 0; i < Configuration.BIGSIZE; i++)
            {
                double value = rnd.NextDouble();
                array[i] = value;
           //     Console.WriteLine(array[i]);

            }
            File.WriteAllLines(@"matrix.txt", array.Select(d => d.ToString()));
        }
        public static void Reformat()
        {
            string[] result = new string[Configuration.BIGSIZE];
            string[] lines = File.ReadLines("matrix.txt").ToArray();
            for(int i = 0; i < Configuration.BIGSIZE; i++)
            {
                string[] pieces = lines[i].Split(',');
                result[i] = "0." + pieces[1];
                Console.WriteLine(result[i]);
            }

            File.WriteAllLines(@"matrix1.txt", result.Select(d => d.ToString()));


        }
        public static void Datafloat()
        {
            float[] array = new float[Configuration.BIGSIZE];
            Random rnd = new Random();
            for (int i = 0; i < Configuration.BIGSIZE; i++)
            {
                double maximum = rnd.NextDouble();
                double minimum = rnd.NextDouble();
                float value = (float)(rnd.NextDouble() * (maximum - minimum) + minimum);
                array[i] = value;
                Console.WriteLine(array[i]);

            }
            File.WriteAllLines(@"matrixfloat.txt", array.Select(d => d.ToString()));
        }
        static void Main(string[] args)
        {



            //Datadouble(); 
            // Datafloat();
            Reformat();

        }
    }
}

   
