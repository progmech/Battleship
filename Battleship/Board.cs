using System;
using System.Text;

namespace Battleship;

internal sealed class Board
{
    private const int BoardSide = 10;

    private const string VerticalCoords = "   А Б В Г Д Е Ж З И К";

    private readonly int[] _ships = [4, 3, 3, 2, 2, 2, 1, 1, 1, 1];

    private readonly Cell[,] _board;

    public Board()
    {
        _board = new Cell[BoardSide, BoardSide];

        for (int v = 0; v < BoardSide; v++)
        {
            for (int h = 0; h < BoardSide; h++)
            {
                _board[v, h] = new Cell();
            }
        }
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
