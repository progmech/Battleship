namespace Battleship;

internal record class Ship(
    Line Line,
    int ShipSize,
    int StartX,
    int EndX,
    int StartY,
    int EndY);

internal class QuitToSaveException : Exception { }

internal enum Move
{
    Human,
    Machine
}

internal enum Line
{
    Horizontal,
    Vertical
}

internal enum GameState
{
    Quit,
    New,
    Load,
    Save,
    Menu
}

internal enum CoordType
{
    Single,
    Start,
    End
}

internal enum CellState
{
    Empty,

    Unbroken,

    Damaged,

    OffTarget
}
