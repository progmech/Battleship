using System;

namespace Battleship;

internal static class Dialog
{
    internal static void AskForShip(int shipSize)
    {
        Console.WriteLine($"Размещаем корабль размером {shipSize} палубы");
        Line line = GetDirection();
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
