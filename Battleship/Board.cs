using System.Text;

namespace Battleship;

internal sealed class Board
{
    private const int BoardSide = 10;

    private const string VerticalCoords = "   А Б В Г Д Е Ж З И К";

    private readonly bool _isAutoGenerate;

    private readonly int[] _ships = [4, 3, 3, 2, 2, 2, 1, 1, 1, 1];

    private readonly Cell[,] _board;

    public Board(bool autoGenerate)
    {
        _isAutoGenerate = autoGenerate;

        _board = new Cell[BoardSide, BoardSide];

        for (int v = 0; v < BoardSide; v++)
        {
            for (int h = 0; h < BoardSide; h++)
            {
                _board[v, h] = new Cell();
            }
        }

        if (autoGenerate)
        {
            GenerateShips();
            return;
        }

        AskForShips();
    }

    private void AskForShips()
    {
        foreach (int shipSize in _ships)
        {
            while (true)
            {
                Ship ship = Dialog.AskForShip(shipSize);
                if (TryPlaceShip(ship))
                {
                    break;
                }

                Console.WriteLine("Выявлено соприкосновение кораблей!");
            }

            Print();
        }
    }

    private void GenerateShips()
    {
        foreach (int shipSize in _ships)
        {
            CalculateShip(shipSize);
        }
    }

    private void CalculateShip(int shipSize)
    {
        Random rnd = new();
        Line line = (Line)rnd.Next(0, 2);
        int x, y;
        Ship ship;
        do
        {
            x = rnd.Next(0, 10);
            y = rnd.Next(0, 10);
            ship = new Ship(line, shipSize, x, int.MinValue, y, int.MinValue);
        } while (!TryPlaceShip(ship));
    }

    private bool TryPlaceShip(Ship ship)
    {
        return ship.Line switch
        {
            Line.Horizontal => TryPlaceShipHorizontal(ship),
            Line.Vertical => TryPlaceShipVertical(ship),
            _ => true
        };
    }

    private bool TryPlaceShipVertical(Ship ship)
    {
        int lowCoord;
        int highCoord;
        if (_isAutoGenerate)
        {
            lowCoord = ship.StartY > BoardSide - ship.ShipSize
                ? ship.StartY - ship.ShipSize + 1
                : ship.StartY;
            highCoord = ship.StartY > BoardSide - ship.ShipSize
                ? ship.StartY
                : ship.StartY + ship.ShipSize - 1;
        }
        else
        {
            lowCoord = ship.StartY;
            highCoord = ship.EndY;
        }

        int lowIndex = lowCoord - 1 < 0 ? 0 : lowCoord - 1;
        int highIndex = highCoord + 1 > BoardSide - 1
            ? BoardSide - 1
            : highCoord + 1;

        int leftIndex = ship.StartX - 1 < 0 ? 0 : ship.StartX - 1;
        int rightIndex = ship.StartX + 1 > BoardSide - 1
            ? BoardSide - 1
            : ship.StartX + 1;

        if (HasNeighbour(leftIndex, rightIndex, lowIndex, highIndex))
        {
            return false;
        }

        for (int v = lowCoord; v <= highCoord; v++)
        {
            _board[v, ship.StartX].State = CellState.Unbroken;
        }

        return true;
    }

    private bool TryPlaceShipHorizontal(Ship ship)
    {
        int leftCoord;
        int rightCoord;
        if (_isAutoGenerate)
        {
            leftCoord = ship.StartX > BoardSide - ship.ShipSize
                ? ship.StartX - ship.ShipSize + 1
                : ship.StartX;
            rightCoord = ship.StartX > BoardSide - ship.ShipSize
                ? ship.StartX
                : ship.StartX + ship.ShipSize - 1;
        }
        else
        {
            leftCoord = ship.StartX;
            rightCoord = ship.EndX;
        }

        int leftIndex = leftCoord - 1 < 0 ? 0 : leftCoord - 1;
        int rightIndex = rightCoord + 1 > BoardSide - 1
            ? BoardSide - 1
            : rightCoord + 1;

        int lowIndex = ship.StartY - 1 < 0 ? 0 : ship.StartY - 1;
        int highIndex = ship.StartY + 1 > BoardSide - 1
            ? BoardSide - 1
            : ship.StartY + 1;

        if (HasNeighbour(leftIndex, rightIndex, lowIndex, highIndex))
        {
            return false;
        }

        for (int h = leftCoord; h <= rightCoord; h++)
        {
            _board[ship.StartY, h].State = CellState.Unbroken;
        }

        return true;
    }

    private bool HasNeighbour(int leftIndex, int rightIndex, int lowIndex, int highIndex)
    {
        for (int v = lowIndex; v <= highIndex; v++)
        {
            for (int h = leftIndex; h <= rightIndex; h++)
            {
                if (_board[v, h].State == CellState.Unbroken)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void Print()
    {
        Console.WriteLine(VerticalCoords);
        for (int v = 0; v < BoardSide; v++)
        {
            StringBuilder sb = new();
            sb.Append($"{v + 1}".PadRight(3));
            for (int h = 0; h < BoardSide; h++)
            {
                sb.Append(_board[v, h]);
                sb.Append(' ');
            }
            Console.WriteLine(sb.ToString().TrimEnd());
        }
    }
}
