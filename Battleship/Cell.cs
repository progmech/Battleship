namespace Battleship;

internal sealed class Cell
{
    public CellState State { get; set; } = CellState.Empty;

    public override string ToString()
    {
        return State switch
        {
            CellState.Empty => "\u00B7",
            CellState.Unbroken => "?",
            CellState.Damaged => "X",
            _ => "*"
        };
    }
}
