namespace Battleship;

internal record class Ship(
    Line Line,
    int ShipSize,
    int StartX,
    int EndX,
    int StartY,
    int EndY);
