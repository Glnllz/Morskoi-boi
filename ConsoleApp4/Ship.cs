class Ship
{
    Random random = new Random();
    public int length;
    public int hits;

    public Ship(int length)
    {
        this.length = length;
        hits = 0;
    }

    public int Length { get { return length; } }

    public void Hit()
    {
        hits++;
    }

    public bool IsSunk()
    {
        return hits == length;
    }

    public bool PlaceShip(Board board, string startCoordinate)
    {
        Console.WriteLine($"Введите направление для корабля длиной {Length} палуб (Г - горизонтально, В - вертикально): ");
        char direction = Console.ReadLine().ToUpper()[0];

        int row = startCoordinate[1] - '1';
        int column = startCoordinate[0] - 'A';

        if (direction == 'Г')
        {
            if (column + Length > board.BoardSize)
            {
                Console.WriteLine("Корабль выходит за пределы поля!");
                return false;
            }
            for (int i = column; i < column + Length; i++)
            {
                if (board.grid[row, i] != ' ')
                {
                    Console.WriteLine("На этом месте уже находится другой корабль!");
                    return false;
                }
            }
            for (int i = column; i < column + Length; i++)
            {
                board.grid[row, i] = 'K';
            }
        }
        else if (direction == 'В')
        {
            if (row + Length > board.BoardSize)
            {
                Console.WriteLine("Корабль выходит за пределы поля!");
                return false;
            }
            for (int i = row; i < row + Length; i++)
            {
                if (board.grid[i, column] != ' ')
                {
                    Console.WriteLine("На этом месте уже находится другой корабль!");
                    return false;
                }
            }
            for (int i = row; i < row + Length; i++)
            {
                board.grid[i, column] = 'K';
            }
        }
        else
        {
            Console.WriteLine("Неверное направление!");
            return false;
        }
        return true;
    }
    public bool PlaceShipComputer(Board board, string startCoordinate)
    {
        char[] directions = new char[] { 'В', 'Г' };
        char direction = directions[random.Next(2)];

        int row = startCoordinate[1] - '1';
        int column = startCoordinate[0] - 'A';

        if (direction == 'Г')
        {
            if (column + Length > board.BoardSize)
            {
                return false;
            }
            for (int i = column; i < column + Length; i++)
            {
                if (board.grid[row, i] != ' ')
                {
                    return false;
                }
            }
            for (int i = column; i < column + Length; i++)
            {
                board.grid[row, i] = 'K';
            }
        }
        else if (direction == 'В')
        {
            if (row + Length > board.BoardSize)
            {
                return false;
            }
            for (int i = row; i < row + Length; i++)
            {
                if (board.grid[i, column] != ' ')
                {
                    return false;
                }
            }
            for (int i = row; i < row + Length; i++)
            {
                board.grid[i, column] = 'K';
            }
        }
        else
        {
            return false;
        }
        return true;
    }
}
