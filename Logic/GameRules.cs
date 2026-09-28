using Chess_Game.Enums;
using Chess_Game.Models;
using Chess_Game.Models.Pieces;

namespace Chess_Game.Logic;

public class GameRules
{
    public bool IsKingInCheck(Board board, PieceColor color)
    {
        // 1) Find that color's king
        Position? kingPosition = null;

        foreach (Piece piece in board.GetAllPieces())
        {
            if (piece is King && piece.Color == color)
            {
                kingPosition = piece.Position;
                break;
            }
        }

        // If there is no king (shouldn't normally happen), no check.
        if (kingPosition is null)
            return false;

        // 2) Check if any enemy piece's moves include the king's square
        foreach (Piece piece in board.GetAllPieces())
        {
            if (piece.Color != color)
            {
                List<Position> enemyMoves = piece.GetValidMoves(board);

                foreach (Position move in enemyMoves)
                {
                    if (move.Row == kingPosition.Row && move.Column == kingPosition.Column)
                        return true;
                }
            }
        }

        return false;
    }
}
