using Chess_Game.Exceptions;
using Chess_Game.Models;
using Chess_Game.Models.Pieces;

namespace Chess_Game.Logic;

// Checks whether a move the player typed is actually allowed.
// If it is not allowed, it throws an exception explaining why.
public class MoveValidator
{
    private readonly GameRules gameRules;

    public MoveValidator()
    {
        gameRules = new GameRules();
    }

    public void ValidateMove(Game game, Position from, Position to)
    {
        Piece? piece = game.Board.GetPiece(from);

        if (piece == null)
            throw new InvalidMoveException("There is no piece on that square.");

        if (piece.Color != game.CurrentTurn)
            throw new NotYourTurnException("It is not your turn.");

        if (!game.Board.IsInside(to))
            throw new InvalidMoveException("You cannot move off the board.");

        if (from.Row == to.Row && from.Column == to.Column)
            throw new InvalidMoveException("You must move the piece to a different square.");

        Piece? targetPiece = game.Board.GetPiece(to);

        if (targetPiece != null && targetPiece.Color == piece.Color)
            throw new InvalidMoveException("You already have a piece on that square.");

        // Is "to" one of the squares this piece is normally allowed to move to?
        bool isInMoveList = false;
        List<Position> validMoves = piece.GetValidMoves(game.Board);

        foreach (Position move in validMoves)
        {
            if (move.Row == to.Row && move.Column == to.Column)
            {
                isInMoveList = true;
                break;
            }
        }

        if (!isInMoveList)
            throw new InvalidMoveException(piece.Symbol + " cannot move to that square.");

        // Even if the move above is normally allowed, it is still illegal
        // if it would leave your own king in check.
        if (gameRules.MoveLeavesKingInCheck(game.Board, from, to))
            throw new InvalidMoveException("You cannot make a move that leaves your king in check.");
    }
}