class Board
{
    public int BoardSize = 10;
    public char[,] grid;
    public Ship[] ships;

    public Board()
    {
        grid = new char[BoardSize, BoardSize];
        ships = new Ship[]
        {
            new Ship(4), // четырехпалубный
            new Ship(3), new Ship(3), // два трехпалубных
            new Ship(2), new Ship(2), new Ship(2), // три двухпалубных
            new Ship(1), new Ship(1), new Ship(1), new Ship(1) // четыре однопалубных
        };

        InitializeBoard();
    }

    public void InitializeBoard()
    {
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                grid[i, j] = ' ';
            }
        }
    }

    public void DisplayBoard()
    {
        Console.WriteLine("   A B C D E F G H I J");
        for (int i = 0; i < BoardSize; i++)
        {
            Console.Write($"{i + 1:D2} ");
            for (int j = 0; j < BoardSize; j++)
            {
                Console.Write($"{grid[i, j]} ");
            }
            Console.WriteLine();
        }
    }

    public void DisplayHiddenBoard()
    {
        Console.WriteLine("   A B C D E F G H I J");
        for (int i = 0; i < BoardSize; i++)
        {
            Console.Write($"{i + 1:D2} ");
            for (int j = 0; j < BoardSize; j++)
            {
                char displayChar = grid[i, j] == 'X' ? ' ' : grid[i, j];
                Console.Write($"{displayChar} ");
            }
            Console.WriteLine();
        }
    }

    public void PlaceShips()
    {
        foreach (Ship ship in ships)
        {
            Console.Clear();
            DisplayBoard();
            Console.WriteLine($"\nРасставьте корабль длиной {ship.Length} палуб. Введите начальную координату (например, A1): ");
            string startCoordinate = Console.ReadLine().ToUpper();
            if (ship.PlaceShip(this, startCoordinate))
            {
                var temp = new List<Ship>(ships);
                temp.Remove(ship);
                ships = temp.ToArray();
            }
        }
        Console.Clear();
        DisplayBoard();
    }

    public void PlaceShipsRandomly()
    {
        Random random = new Random();

        foreach (Ship ship in ships)
        {
            bool placed = false;

            while (!placed)
            {
                char column = (char)('A' + random.Next(0, BoardSize));
                int row = random.Next(0, BoardSize);
                string startCoordinate = $"{column}{row + 1}";

                placed = ship.PlaceShipComputer(this, startCoordinate);
            }
        }
    }



    public void ProcessMove(string move, Board targetBoard)
    {
        int row = move[1] - '1';
        int column = move[0] - 'A';

        if (targetBoard.grid[row, column] == ' ')
        {
            Console.WriteLine("Промах!");
            targetBoard.grid[row, column] = 'O';
        }
        else if (targetBoard.grid[row, column] == 'X' || targetBoard.grid[row, column] == 'O')
        {
            Console.WriteLine("Вы уже стреляли в эту клетку!");
        }
        else
        {
            Console.WriteLine("Попадание!");
            targetBoard.grid[row, column] = 'X';
        }
        Console.WriteLine("Нажмите Enter для продолжения...");
        Console.ReadLine();
    }

    public bool AllShipsSunk()
    {
        foreach (Ship ship in ships)
        {
            if (!ship.IsSunk())
            {
                return false;
            }
        }
        return true;
    }

    public void ComputerMove(Board targetBoard)
    {
        Random random = new Random();

        int row, column;

        do
        {
            row = random.Next(0, BoardSize);
            column = random.Next(0, BoardSize);
        } while (grid[row, column] == 'X' || grid[row, column] == 'O');

        if (targetBoard.grid[row, column] == ' ')
        {
            Console.WriteLine($"Промах компьютера! Компьютер стреляет в {Convert.ToChar('A' + column)}{row + 1}");
            targetBoard.grid[row, column] = 'O';
        }
        else
        {
            Console.WriteLine($"Попадание компьютера! Компьютер стреляет в {Convert.ToChar('A' + column)}{row + 1}");
            targetBoard.ships[targetBoard.grid[row, column]].Hit();
            targetBoard.grid[row, column] = 'X';
        }
        Console.WriteLine("Нажмите Enter для продолжения...");
        Console.ReadLine();
    }
}
