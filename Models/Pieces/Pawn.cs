using Chess_Game.Enums;

namespace Chess_Game.Models.Pieces;

// The Pawn is the trickiest piece:
//  - it moves forward only (never sideways or backward)
//  - it moves one square, or two squares from its starting row
//  - it can only capture diagonally, one square forward
public class Pawn : Piece
{
    public override char Symbol => 'P';

    public Pawn(PieceColor color, Position position) : base(color, position)
    {
    }

    public override List<Position> GetValidMoves(Board board)
    {
        List<Position> moves = new List<Position>();

        // White moves up the board (toward row 0), Black moves down (toward row 7).
        int direction = Color == PieceColor.White ? -1 : 1;
        int startRow = Color == PieceColor.White ? 6 : 1;

        // Move forward 
        Position oneStep = new Position(Position.Row + direction, Position.Column);

        if (board.IsInside(oneStep) && board.GetPiece(oneStep) == null)
        {
            moves.Add(oneStep);

            // Still on the starting row? Then two squares forward is also allowed,
            // as long as that square is empty too.
            if (Position.Row == startRow)
            {
                Position twoSteps = new Position(Position.Row + direction * 2, Position.Column);

                if (board.GetPiece(twoSteps) == null)
                    moves.Add(twoSteps);
            }
        }

        // --- Capture diagonally ---
        int[] captureColumns = [ Position.Column - 1, Position.Column + 1 ];

        foreach (int column in captureColumns)
        {
            Position capturePosition = new Position(Position.Row + direction, column);

            if (!board.IsInside(capturePosition))
                continue;

            Piece? target = board.GetPiece(capturePosition);

            if (target != null && target.Color != Color)
                moves.Add(capturePosition);
        }

        return moves;
    }
}