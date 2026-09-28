using Chess_Game.Enums;
using Chess_Game.Models;
using Chess_Game.Models.Pieces;

namespace Chess_Game.Logic;

public class GameRules
{
    // true if an enemy piece can capture the king of this color
    public bool IsKingInCheck(Board board, PieceColor color)
    {
        // find where the king is
        Position? kingPosition = null;

        foreach (Piece piece in board.GetAllPieces())
        {
            if (piece is King && piece.Color == color)
            {
                kingPosition = piece.Position;
                break;
            }
        }

        // no king on the board, so no check
        if (kingPosition is null)
            return false;

        // if any enemy move ends on the king's square, it's check
        foreach (Piece piece in board.GetAllPieces())
        {
            if (piece.Color != color)
            {
                foreach (Position move in piece.GetValidMoves(board))
                {
                    if (move.Row == kingPosition.Row && move.Column == kingPosition.Column)
                        return true;
                }
            }
        }

        return false;
    }

    // play the move on a copy of the board and see if our king ends up in check
    public bool MoveLeavesKingInCheck(Board board, Position from, Position to)
    {
        Piece? piece = board.GetPiece(from);

        // nothing on the starting square
        if (piece is null)
            return false;

        // the copy is used so the real board doesn't change
        Board copy = board.Clone();
        copy.MovePiece(from, to);

        return IsKingInCheck(copy, piece.Color);
    }

    // normal moves of the piece, without the ones that put our own king in check
    public List<Position> GetLegalMoves(Board board, Piece piece)
    {
        List<Position> legalMoves = new List<Position>();

        foreach (Position move in piece.GetValidMoves(board))
        {
            if (!MoveLeavesKingInCheck(board, piece.Position, move))
                legalMoves.Add(move);
        }

        return legalMoves;
    }

    // true if this color has at least one legal move (stops at the first one)
    public bool HasAnyLegalMove(Board board, PieceColor color)
    {
        foreach (Piece piece in board.GetAllPieces())
        {
            if (piece.Color == color && GetLegalMoves(board, piece).Count > 0)
                return true;
        }

        return false;
    }

    // status for the player whose turn it is
    // (takes board + color for now because Game.cs isn't written yet)
    public GameStatus GetGameStatus(Board board, PieceColor turn)
    {
        bool inCheck = IsKingInCheck(board, turn);
        bool hasMove = HasAnyLegalMove(board, turn);

        // check and no way out
        if (inCheck && !hasMove)
            return GameStatus.Checkmate;

        if (inCheck)
            return GameStatus.Check;

        // not in check but nothing to play
        if (!hasMove)
            return GameStatus.Stalemate;

        return GameStatus.InProgress;
    }
}