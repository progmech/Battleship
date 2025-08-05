namespace Battleship;

internal sealed class Cell
{
    internal CellState State { get; set; } = CellState.Empty;

    private readonly bool _isMachineBoard;

    internal Cell(bool isMachineBoard)
    {
        _isMachineBoard = isMachineBoard;
    }

    public override string ToString()
    {
        return State switch
        {
            CellState.Empty => "\u00B7",
            CellState.Unbroken => _isMachineBoard ? "\u00B7" : "?",
            CellState.Damaged => "X",
            _ => "*"
        };
    }
}
