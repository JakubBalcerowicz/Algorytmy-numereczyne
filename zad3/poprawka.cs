  static List<GameStatus> GeneratorP1(GameStatus a, List<GameStatus> b)
        {
            int size = b.Count;
            for (int i = -Configuration.cubeSize; i <= Configuration.cubeSize; i++)
            {
                int turn = 2;
                if (CheckAll(size, b, a.positionP1, a.positionP2, turn) == true)
                {
                    var dane = new GameStatus(((a.positionP1 + i) + Configuration.mapSize) % Configuration.mapSize, a.positionP2, turn, (rozmiar listy));
                   // casenumber++;
                    b.Add(dane);
                   //a dodaj do rownania (dane)

                }else{

                    // dodaj tego X ktory juz jest w liscie wszytskich X 
                    //a dodaj do rownania (dane)
                }
            }

            return b;
        }
