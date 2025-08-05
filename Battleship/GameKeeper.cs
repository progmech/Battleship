using System.Text;

namespace Battleship;

public class GameKeeper
{
    internal static void Load(string filePath, Game game)
    {
        string[] gameInfo = File.ReadAllLines(filePath);
        game.CurrentMove = (Move)int.Parse(gameInfo[0]);
        game.MachineBoard = new Board(DeserializeBoard(gameInfo[1..11]));
        game.PlayerBoard = new Board(DeserializeBoard(gameInfo[11..]));
    }

    private static Cell[,] DeserializeBoard(string[] gameInfo)
    {
        Cell[,] board = new Cell[Board.BoardSide, Board.BoardSide];
        for (int v = 0; v < gameInfo.Length; v++)
        {
            string[] row = gameInfo[v].Split(" ");
            for (int h = 0; h < row.Length; h++)
            {
                board[v, h] = new Cell()
                {
                    State = (CellState)int.Parse(row[h])
                };
            }
        }
        return board;
    }

    internal static string Save(Game game)
    {
        int hash = DateTime.Now.GetHashCode();
        string filePath = Path.Combine(
            Environment.CurrentDirectory,
            $"battleship{hash}");
        List<string> gameInfo = Serialize(game);
        File.AppendAllLines(filePath, gameInfo);

        return filePath;
    }

    private static List<string> Serialize(Game game)
    {
        List<string> result = new();
        result.Add(((int)game.CurrentMove).ToString());
        var machineBoard = SerializeBoard(game.MachineBoard.UnderlyingBoard);
        var playerBoard = SerializeBoard(game.PlayerBoard.UnderlyingBoard);
        result.AddRange(machineBoard);
        result.AddRange(playerBoard);
        return result;
    }

    private static List<string> SerializeBoard(Cell[,] board)
    {
        List<string> result = new();
        for (int v = 0; v < Board.BoardSide; v++)
        {
            StringBuilder sb = new();
            for (int h = 0; h < Board.BoardSide; h++)
            {
                sb.Append($"{(int)board[v, h].State} ");
            }
            result.Add(sb.ToString().TrimEnd());
        }
        return result;
    }
}
