using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

/*
 Ma w sobie
Pole gracz 1
Pole gracz 2
I liste równań
Np
X0 = X1 + x2 + x3
no ale to jest podstawa
i z tego stanu
pierwszego
są tworzone tyle substanów, ile jest scian kostki
a są tworzone tak, że zmienia się tura, w zaleznosci od tego czyja jest tura dodaje się i'ty w kolejnosci wartość kostki na ściance i
tworzy się stan i sprawdza się czy nie został nigdzie wczesniej utworzony
jesli byl, to do listy tego stanu dodaj wczesniej zapisana referencje
jesli nie, to go utworz, dodaj do listy wszystkich stanow i do listy equation tego stanu
*/

namespace Grzybobranie
{
    public class Configuration
    {
        public static int cubeSize = 2;
        public static int mapSize = 5;
    }
    public class GameStatus
    {
        public List<GameStatus> equal = new List<GameStatus>();
        public int positionP1;
        public int positionP2;
        public int turn;
        public int casenumber;
        public GameStatus(int a,int b,int c,int d)
        {
             positionP1 = a;
             positionP2 = b;
             turn = c;
             casenumber = d;         
        }
        public GameStatus(GameStatus a, List<GameStatus> d)
        {
            positionP1 = a.positionP1;
            positionP2 = a.positionP2;
            turn = a.turn;
            casenumber = a.casenumber;
            equal = d;
        }
    }
    class Program
    {
         public static int casenumber  = 1;
        static void MonteCarlo(int postionP1,int postionP2)
        {
            int count = 0;
            double countP1 = 0;
            int a = postionP1;
            int b = postionP2;
            for (int i = 0; i < 100000; i++)
            {
                do
                {
                    if (count % 2 == 0)
                    {
                        Random rnd = new Random();
                        int cube = rnd.Next(-Configuration.cubeSize, Configuration.cubeSize);
                        a = a + cube;
                        a = a % Configuration.mapSize;
                        if (a == 0)
                        {
                            //Console.WriteLine("Gracz P1 wygrywa ustal na polu {0}", a);
                            countP1++;
                            break;
                        }
                        //Console.WriteLine("a={0}", a);
                        count += 1;
                    }
                    else
                    {
                        Random rnd = new Random();
                        int cube = rnd.Next(-Configuration.cubeSize, Configuration.cubeSize);
                        b = b + cube;
                        b = b % Configuration.mapSize;
                        if (b == 0)
                        {
                            //Console.WriteLine("Gracz P2 wygrywa ustal na polu {0}", b);
                            break;
                        }
                        //Console.WriteLine("b={0}", b);
                        count += 1;
                    }
                } while (a % Configuration.mapSize != 0 || b % Configuration.mapSize != 0);
            }

            Console.WriteLine("Gracz 1 wygral {0}% gier", countP1/1000);

        }
        static void Print(List<GameStatus> stan)
        {
            foreach (var i in stan)
            {
                Console.WriteLine("P({0},{1},{2},{3})", i.positionP1, i.positionP2, i.turn,i.casenumber);
               
            }
            Console.WriteLine();
        }
        public static bool CheckAll(int a, List<GameStatus> b, int pos1, int pos2, int turn)
        { 
            for(int i = 0; i < a; i++)
            {
               if( b[i].positionP1  == pos1 && b[i].positionP2 == pos2 && b[i].turn == turn)
                { return false;}
            }
            return true;
        }
        static List<GameStatus> GeneratorP1(GameStatus a,List<GameStatus> b)
        {
            int size = b.Count;
            for (int i = -Configuration.cubeSize; i <= Configuration.cubeSize; i++)
            {
                int turn = 2;
                if (CheckAll(size, b, a.positionP1, a.positionP2, turn) == true)
                {
                    var dane = new GameStatus(((a.positionP1 + i) + Configuration.mapSize) % Configuration.mapSize, a.positionP2, turn,casenumber);
                    casenumber++;
                    b.Add(dane);
                }
            }

            return b;
        }


        static List<GameStatus> GeneratorP2(GameStatus a, List<GameStatus> b)
        {
            int size = b.Count;
            for (int i = -Configuration.cubeSize; i <= Configuration.cubeSize; i++)
            {
    
                int turn = 1;
                if (CheckAll(size, b, a.positionP1, a.positionP2, turn) == true)
                {
                    var dane = new GameStatus(a.positionP1, ((a.positionP2 + i) + Configuration.mapSize) % Configuration.mapSize, turn, casenumber);
                    casenumber++;
                    b.Add(dane);
                }
            }

            return b;
        }
        static void Main(string[] args)
        {

            //GameStatus przypadek1 = new GameStatus(0, 3, 4);
            //  List<GameStatus>[] a = new List<GameStatus>[100];  ///////////// tablica kazda komorka to rownanie nastepnego przypadku
            List<GameStatus> fullstan = new List<GameStatus>();
            List<GameStatus> casestan = new List<GameStatus>();

            
            var dane = new GameStatus(2,3,1,1);
            fullstan = GeneratorP1(dane, casestan);
            Print(casestan);
            var test = new GameStatus(dane, casestan);
            fullstan.Add(test);
            

            Print(fullstan[0].equal);
            //Console.WriteLine((fullstan.turn));
           
            
            //  MonteCarlo(3, 4);


        }
    }
}
