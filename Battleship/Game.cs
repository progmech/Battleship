namespace Battleship;

public sealed class Game
{
    internal Board PlayerBoard { get; set; }

    internal Board MachineBoard { get; set; }

    internal Move CurrentMove { get; set; } = Move.Human;

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
        string filePath = string.Empty;
        try
        {
            filePath = GameKeeper.Save(this);
            Console.WriteLine($"Игра успешно сохранена в файл {filePath}");
            Console.WriteLine($"Для возврата в игру загрузите этот файл в пункте 'Загрузить'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка сохранения игры в файл {filePath}.");
            Console.WriteLine(ex.Message);
        }
        finally
        {
            Start();
        }
    }

    private void Load()
    {
        Console.WriteLine("Будет реализовано в следующей версии.");
    }

    private void New()
    {
        MachineBoard = new(true);
        PlayerBoard = new(Dialog.AutoGenerateUserBoard());
        while (_gameIsOn)
        {
            DoNextMove();
        }

        Start();
    }

    private void DoNextMove()
    {
        PrintBoard();
        switch (CurrentMove)
        {
            case Move.Human:
                if (!MachineBoard.HasUnbrokenCell())
                {
                    Console.WriteLine("ПОЗДРАВЛЯЕМ! ВЫ ПОБЕДИЛИ!");
                    _gameIsOn = false;
                    return;
                }
                try
                {
                    DoHumanMove();
                }
                catch (QuitToSaveException)
                {
                    _gameIsOn = false;
                }
                break;
            case Move.Machine:
                if (!PlayerBoard.HasUnbrokenCell())
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
        } while (!PlayerBoard.GetUnshottedCell(coordY, coordX));

        bool success = Dialog.GetHumanConfirmation(coordY, coordX);

        PlayerBoard.ChangeCellStatus(coordY, coordX, success);

        if (success)
        {
            Console.WriteLine("Компьютер попал! Снова его ход!");
            return;
        }

        CurrentMove = Move.Human;
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

        if (MachineBoard.CheckHumanMove(coordY, coordX))
        {
            Console.WriteLine("Вы попали! Снова ваш ход!");
            return;
        }

        CurrentMove = Move.Machine;
    }

    private void PrintBoard()
    {
        Console.WriteLine("Ваша доска:\n");
        PlayerBoard.Print();
        Console.WriteLine("\nДоска компьютера:\n");
        MachineBoard.Print();
    }

    private void Quit()
    {
        Console.WriteLine("Выход");
    }
}
