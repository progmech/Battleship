using System.Text;

namespace Battleship;

internal sealed class Board
{
    private const int BoardSide = 10;

    private const string VerticalCoords = "   А Б В Г Д Е Ж З И К";

    private readonly int[] _ships = [4, 3, 3, 2, 2, 2, 1, 1, 1, 1];

    private readonly Cell[,] _board;

    public Board(bool autoGenerate)
    {
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
        do
        {
            x = rnd.Next(0, 10);
            y = rnd.Next(0, 10);
        } while (!TryPlaceShip(line, shipSize, x, y));
    }

    private bool TryPlaceShip(Line line, int shipSize, int x, int y)
    {
        return line switch
        {
            Line.Horizontal => TryPlaceShipHorizontal(shipSize, x, y),
            Line.Vertical => TryPlaceShipVertical(shipSize, x, y),
            _ => true
        };
    }

    private bool TryPlaceShipVertical(int shipSize, int x, int y)
    {
        throw new NotImplementedException();
    }

    private bool TryPlaceShipHorizontal(int shipSize, int x, int y)
    {
        throw new NotImplementedException();
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
