using Chess_Game.Enums;
using Chess_Game.Models.Pieces;
using Chess_Game.Exceptions;
using Chess_Game.Models;
namespace Chess_Game.Factory;

// Task: Mohamed
// Goal: Build a piece just from its name (like "rook"), so the rest of the
// code doesn't need to write "new Rook(...)" everywhere.
// What to do: Add a static Create(string type, PieceColor color, Position position)
// method that returns the matching piece, and throws InvalidInputException for
// an unknown name.
public static class PieceFactory
{
    public static Piece Create(string type, PieceColor color, Position position)
    {
        switch (type.ToLower())
        {
            case "king":
                return new King(color,position);
            case "queen":
                return new Queen(color,position);
            case "rook":
                return new Rook(color,position);
            case "bishop":
                return new Bishop(color,position);
            case "knight":
                return new Knight(color,position);
            case "Pawn":
                return new Pawn(color,position);

            default:
                throw new InvalidInputException($"Unknown Piece {type}");
        }
    }
}
