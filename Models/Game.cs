using Chess_Game.Enums;
using Chess_Game.Logic;
using Chess_Game.Models.Pieces;

namespace Chess_Game.Models;

public class Game
{
    public Board Board { get; }

    public Player WhitePlayer { get; }
    public Player BlackPlayer { get; }

    // Only the Game class can change the current turn.
    public PieceColor CurrentTurn { get; private set; }

    // Updated after every valid move.
    public GameStatus Status { get; private set; }

    // Stores all moves in the order they were played.
    public List<Move> MoveHistory { get; }

    private readonly MoveValidator moveValidator;
    private readonly GameRules gameRules;

    public Game(Player whitePlayer, Player blackPlayer)
    {
        WhitePlayer = whitePlayer;
        BlackPlayer = blackPlayer;

        Board = new Board();

        // White always moves first in chess.
        CurrentTurn = PieceColor.White;

        Status = GameStatus.InProgress;
        MoveHistory = new List<Move>();

        moveValidator = new MoveValidator();
        gameRules = new GameRules();
    }

    public void MakeMove(Position from, Position to)
    {
        // Validate the move before changing the board.
        moveValidator.ValidateMove(this, from, to);

        Piece? movedPiece = Board.GetPiece(from);

        if (movedPiece == null)
            return;

        // Save the captured piece so the move can be recorded in history.
        Piece? capturedPiece = Board.GetPiece(to);

        Move move = new Move(from, to, movedPiece, capturedPiece);

        Board.MovePiece(from, to);

        // A pawn is promoted when it reaches the opposite end of the board.
        PromotePawnIfNeeded(movedPiece);

        MoveHistory.Add(move);

        ChangeTurn();

        // Recalculate the game state after the move.
        Status = gameRules.GetGameStatus(this);
    }

    private void PromotePawnIfNeeded(Piece piece)
    {
        if (!(piece is Pawn))
            return;

        // White promotes on row 0, while Black promotes on row 7.
        int lastRow = piece.Color == PieceColor.White ? 0 : 7;

        if (piece.Position.Row != lastRow)
            return;

        Piece queen = new Queen(piece.Color, piece.Position);
        Board.SetPiece(piece.Position, queen);
    }

    private void ChangeTurn()
    {
        if (CurrentTurn == PieceColor.White)
            CurrentTurn = PieceColor.Black;
        else
            CurrentTurn = PieceColor.White;
    }

    public bool IsGameOver()
    {
        return Status == GameStatus.Checkmate ||
               Status == GameStatus.Stalemate;
    }
}
