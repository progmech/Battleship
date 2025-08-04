namespace Battleship;

public sealed class Game
{
    private Board _playerBoard;

    private Board _machineBoard;

    public void Start()
    {
        GameState userChoice;
        do
        {
            PrintMenu();
            userChoice = ValidateInput();
            switch (userChoice)
            {
                case GameState.Quit:
                    Quit();
                    break;
                case GameState.New:
                    New();
                    break;
                case GameState.Load:
                    Load();
                    break;
                case GameState.Save:
                    Save();
                    break;
                default:
                    break;
            }
        } while (userChoice == GameState.Menu);
    }

    private void Save()
    {
        Console.WriteLine("Будет реализовано в следующей версии.");
    }

    private void Load()
    {
        Console.WriteLine("Будет реализовано в следующей версии.");
    }

    private void New()
    {
        _playerBoard = new(false);
        _machineBoard = new(true);
        PrintBoard();
    }

    private void PrintBoard()
    {
        Console.WriteLine("Ваша доска:\n");
        _playerBoard.Print();
        Console.WriteLine("\nДоска компьютера:\n");
        _machineBoard.Print();
    }

    private void Quit()
    {
        Console.WriteLine("Выход");
    }

    private GameState ValidateInput()
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

    private void PrintMenu()
    {
        Console.WriteLine("\n1. Новая игра");
        Console.WriteLine("2. Загрузить игру");
        Console.WriteLine("3. Сохранить игру");
        Console.WriteLine("0. Выход\n");
        Console.WriteLine("Ваш выбор:");
    }
}
