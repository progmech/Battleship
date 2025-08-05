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
            Dialog.PrintMenu();
            userChoice = Dialog.ValidateInput();
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
        _machineBoard = new(true);
        _playerBoard = new(Dialog.AutoGenerateUserBoard());
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
}
