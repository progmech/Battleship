using System;

namespace Battleship;

internal sealed class Cell
{
    public CellState State { get; private set; } = CellState.Empty;

    public override string ToString()
    {
        return State switch
        {
            CellState.Empty or CellState.Unbroken or CellState.AlongSide => "*",
            CellState.Damaged or CellState.Destroyed => "X",
            _ => "?"
        };
    }
}
