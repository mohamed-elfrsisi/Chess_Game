
using Chess_Game.Enums;
using Chess_Game.Factory;
using Chess_Game.Models.Pieces;

namespace Chess_Game.Models;

// Represents the chess board.
// It stores pieces and their positions.
// It does NOT decide whether a move is legal.
public class Board
{
    // The board has 8 rows and 8 columns.
    // A square can contain a Piece or be empty (null).
    private readonly Piece?[,] grid;

    // Creates a normal chess board.
    public Board()
    {
        grid = new Piece?[8, 8];

        SetupBoard();
    }

    // Returns the piece at a specific position.
    // If the position is outside the board, return null.
    public Piece? GetPiece(Position position)
    {
        if (!IsInside(position))
            return null;

        return grid[position.Row, position.Column];
    }

    // Puts a piece on a specific square.
    public void SetPiece(Position position, Piece piece)
    {
        if (!IsInside(position))
            throw new ArgumentOutOfRangeException(nameof(position));

        grid[position.Row, position.Column] = piece;

        // Make sure the piece also knows its new position.
        piece.Position = position;
    }

    // Removes a piece from a square.
    public void RemovePiece(Position position)
    {
        if (!IsInside(position))
            throw new ArgumentOutOfRangeException(nameof(position));

        grid[position.Row, position.Column] = null;
    }

    // Moves a piece from one square to another.
    public void MovePiece(Position from, Position to)
    {
        // Get the piece from the starting square.
        Piece? piece = GetPiece(from);

        // There must be a piece at the starting position.
        if (piece == null)
            throw new InvalidOperationException(
                "There is no piece at the starting position."
            );

        // Put the piece in the new square.
        grid[to.Row, to.Column] = piece;

        // Empty the old square.
        grid[from.Row, from.Column] = null;

        // Update the piece's position.
        piece.Position = to;
    }

    // Checks if a position is inside the 8x8 board.
    public bool IsInside(Position position)
    {
        return position.Row >= 0 &&
               position.Row < 8 &&
               position.Column >= 0 &&
               position.Column < 8;
    }

    // Returns all pieces currently on the board.
    public List<Piece> GetAllPieces()
    {
        List<Piece> pieces = new List<Piece>();

        // Check every square.
        for (int row = 0; row < 8; row++)
        {
            for (int column = 0; column < 8; column++)
            {
                Piece? piece = grid[row, column];

                // If there is a piece, add it to the list.
                if (piece != null)
                {
                    pieces.Add(piece);
                }
            }
        }

        return pieces;
    }

    // Creates a copy of the board.
    // This is useful when we want to test a move
    // without changing the real board.
    public Board Clone()
    {
        // Create an empty board.
        Board copy = new Board(false);

        // Go through every square.
        for (int row = 0; row < 8; row++)
        {
            for (int column = 0; column < 8; column++)
            {
                Piece? piece = grid[row, column];

                // If the square contains a piece,
                // create a new copy of that piece.
                if (piece != null)
                {
                    Position position = new Position(row, column);

                    Piece newPiece = CopyPiece(piece, position);

                    copy.SetPiece(position, newPiece);
                }
            }
        }

        return copy;
    }

    // Creates a new piece with the same type, color and position.
    private Piece CopyPiece(Piece piece, Position position)
    {
        if (piece is King)
            return new King(piece.Color, position);

        if (piece is Queen)
            return new Queen(piece.Color, position);

        if (piece is Rook)
            return new Rook(piece.Color, position);

        if (piece is Bishop)
            return new Bishop(piece.Color, position);

        if (piece is Knight)
            return new Knight(piece.Color, position);

        // If it is not one of the pieces above,
        // it is a Pawn.
        return new Pawn(piece.Color, position);
    }

    // Creates an empty board.
    // Used by Clone().
    private Board(bool setupPieces)
    {
        grid = new Piece?[8, 8];

        if (setupPieces)
        {
            SetupBoard();
        }
    }

    // Places all chess pieces in their starting positions.
    private void SetupBoard()
    {
        // Order of the pieces in the first row.
        string[] backRow =
        {
            "rook",
            "knight",
            "bishop",
            "queen",
            "king",
            "bishop",
            "knight",
            "rook"
        };

        // Go through all 8 columns.
        for (int column = 0; column < 8; column++)
        {
            // -------------------------
            // WHITE PIECES
            // -------------------------

            // White back-row piece.
            Position whiteBackPosition = new Position(7, column);

            Piece whiteBackPiece = PieceFactory.Create(
                backRow[column],
                PieceColor.White,
                whiteBackPosition
            );

            SetPiece(whiteBackPosition, whiteBackPiece);

            // White pawn.
            Position whitePawnPosition = new Position(6, column);

            Piece whitePawn = PieceFactory.Create(
                "pawn",
                PieceColor.White,
                whitePawnPosition
            );

            SetPiece(whitePawnPosition, whitePawn);


            // -------------------------
            // BLACK PIECES
            // -------------------------

            // Black back-row piece.
            Position blackBackPosition = new Position(0, column);

            Piece blackBackPiece = PieceFactory.Create(
                backRow[column],
                PieceColor.Black,
                blackBackPosition
            );

            SetPiece(blackBackPosition, blackBackPiece);

            // Black pawn.
            Position blackPawnPosition = new Position(1, column);

            Piece blackPawn = PieceFactory.Create(
                "pawn",
                PieceColor.Black,
                blackPawnPosition
            );

            SetPiece(blackPawnPosition, blackPawn);
        }
    }
}
