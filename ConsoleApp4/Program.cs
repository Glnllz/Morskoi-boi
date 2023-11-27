Board playerBoard = new Board();
Board computerBoard = new Board();

// Расстановка кораблей
playerBoard.PlaceShips();
computerBoard.PlaceShipsRandomly();

while (!playerBoard.AllShipsSunk() && !computerBoard.AllShipsSunk())
{
    Console.Clear();
    Console.WriteLine("Ваша доска:");
    playerBoard.DisplayBoard();

    Console.WriteLine("\nДоска компьютера:");
    computerBoard.DisplayHiddenBoard();

    Console.WriteLine("\nВаш ход. Введите координаты (например, A1): ");
    string playerMove = Console.ReadLine().ToUpper();
    playerBoard.ProcessMove(playerMove, computerBoard);

    if (!computerBoard.AllShipsSunk())
    {
        Console.WriteLine("\nХод компьютера:");
        computerBoard.ComputerMove(playerBoard);
    }
}

Console.Clear();
Console.WriteLine("Игра окончена!");
Console.WriteLine(playerBoard.AllShipsSunk() ? "Вы проиграли!" : "Вы победили!");


