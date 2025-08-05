namespace Battleship;

public sealed class Game
{
    private Board _playerBoard;

    private Board _machineBoard;

    private Move _currentMove = Move.Human;

    private bool _gameIsOn = true;

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
        while (_gameIsOn)
        {
            DoNextMove();
        }
        PrintBoard();
    }

    private void DoNextMove()
    {
        PrintBoard();
        switch (_currentMove)
        {
            case Move.Human:
                if (!_machineBoard.HasUnbrokenCell())
                {
                    Console.WriteLine("ПОЗДРАВЛЯЕМ! ВЫ ПОБЕДИЛИ!");
                    _gameIsOn = false;
                    return;
                }
                DoHumanMove();
                break;
            case Move.Machine:
                if (!_playerBoard.HasUnbrokenCell())
                {
                    Console.WriteLine("ВЫ ПРОИГРАЛИ! Печально...");
                    _gameIsOn = false;
                    return;
                }
                DoMachineMove();
                break;
        }
    }

    private void DoMachineMove()
    {
        int coordX;
        int coordY;
        do
        {
            Random rnd = new();
            coordX = rnd.Next(0, 10);
            coordY = rnd.Next(0, 10);
        } while (!_playerBoard.GetUnshottedCell(coordY, coordX));

        bool success = Dialog.GetHumanConfirmation(coordY, coordX);

        _playerBoard.ChangeCellStatus(coordY, coordX, success);

        if (success)
        {
            Console.WriteLine("Компьютер попал! Снова его ход!");
            return;
        }

        _currentMove = Move.Human;
    }

    private void DoHumanMove()
    {
        int coordY;
        int coordX;
        while (!Dialog.TryGetSingleCoord(CoordType.Single, Line.Horizontal, out coordX)
        || !Dialog.TryGetSingleCoord(CoordType.Single, Line.Vertical, out coordY))
        {
            Console.WriteLine("Введены неправильные координаты!");
        }

        if (_machineBoard.CheckHumanMove(coordY, coordX))
        {
            Console.WriteLine("Вы попали! Снова ваш ход!");
            return;
        }

        _currentMove = Move.Machine;
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
