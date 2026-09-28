using Chess_Game.Enums;

namespace Chess_Game.Models;

public class Player
{
    public string Name { get; }
    PieceColor Color{get;}

    public Player(string name, PieceColor color)
    {
        Name = name;
        Color = color;
    }
}
