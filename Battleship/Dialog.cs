using System;

namespace Battleship;

internal static class Dialog
{
    private static readonly string[] _horizontalCoords =
    ["А", "Б", "В", "Г", "Д", "Е", "Ж", "З", "И", "К"];

    private static readonly string[] _verticalCoords =
        ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10"];

    internal static void AskForShip(int shipSize)
    {
        Console.WriteLine($"Размещаем корабль размером {shipSize} палубы");
        Line line = GetDirection();
        (int startX, int endX) = GetHorizontalCoords(line, shipSize);
    }

    private static (int startX, int endX) GetHorizontalCoords(Line line, int shipSize)
    {
        while (true)
        {
            int startX = int.MinValue;
            int endX = int.MinValue;
            string userInput;

            if (line == Line.Vertical || shipSize == 1)
            {
                Console.WriteLine("Введите координату по горизонтали (от А до К).");
                userInput = Console.ReadLine();
                if (!!string.IsNullOrWhiteSpace(userInput)
                    && _horizontalCoords.Contains(userInput))
                {
                    int coord = Array.IndexOf(_horizontalCoords, userInput);
                    return (coord, coord);
                }
            }

            Console.WriteLine("Введите начальную координату по горизонтали (от А до К).");
            userInput = Console.ReadLine();
            if (!!string.IsNullOrWhiteSpace(userInput)
                && _horizontalCoords.Contains(userInput))
            {
                startX = Array.IndexOf(_horizontalCoords, userInput);
            }

            Console.WriteLine("Введите конечную координату по вертикали (от А до К).");
            userInput = Console.ReadLine();
            if (!!string.IsNullOrWhiteSpace(userInput)
                && _horizontalCoords.Contains(userInput))
            {
                endX = Array.IndexOf(_horizontalCoords, userInput);
            }

            if (startX != int.MinValue
                && endX != int.MinValue
                && startX < endX
                && endX - startX + 1 == shipSize)
            {
                return (startX, endX);
            }

            Console.WriteLine("Введены неправильные координаты!");
        }
    }

    private static Line GetDirection()
    {

        Console.WriteLine("Введите направление. Горизонтально - 1, Вертикально - любая другая клавиша.");
        string? userInput = Console.ReadLine();
        Line line = !string.IsNullOrWhiteSpace(userInput)
            && int.TryParse(userInput, out int userChoice)
            && userChoice == 1
            ? Line.Horizontal
            : Line.Vertical;

        return line;
    }

    internal static bool AutoGenerateUserBoard()
    {
        Console.WriteLine("Расставить ваши корабли в автоматическом режиме?");
        Console.WriteLine("Да - 1, Нет - любая другая клавиша.");
        string? userInput = Console.ReadLine();
        return
            !string.IsNullOrWhiteSpace(userInput)
            && int.TryParse(userInput, out int userChoice)
            && userChoice == 1;
    }

    internal static void PrintMenu()
    {
        Console.WriteLine("\n1. Новая игра");
        Console.WriteLine("2. Загрузить игру");
        Console.WriteLine("3. Сохранить игру");
        Console.WriteLine("0. Выход\n");
        Console.WriteLine("Ваш выбор:");
    }

    internal static GameState ValidateInput()
    {
        string userInput = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(userInput)
        || !int.TryParse(userInput, out int userChoice)
        || userChoice < (int)GameState.Quit
        || userChoice > (int)GameState.Save)
        {
            Console.WriteLine("Неправильный ввод! Введите число от 0 до 3.");
            return GameState.Menu;
        }

        return (GameState)userChoice;
    }
}
