while (true)
{
    Console.WriteLine("\n1. Новая игра");
    Console.WriteLine("2. Загрузить игру");
    Console.WriteLine("3. Сохранить игру");
    Console.WriteLine("0. Выход\n");
    Console.WriteLine("Ваш выбор:");

    string userInput = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(userInput)
    || !int.TryParse(userInput, out int userChoice)
    || userChoice < 0
    || userChoice > 3)
    {
        Console.WriteLine("Неправильный ввод! Введите число от 0 до 3.");
        continue;
    }

    Console.WriteLine("Вы молодец!");
    break;
}
