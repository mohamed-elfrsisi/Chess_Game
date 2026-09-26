using Chess_Game.Enums;
namespace Chess_Game.Models.Pieces;
// The King moves one square in any direction (8 directions total).

public class King : Piece
{
  public override char Symbol => 'K';

    public King(PieceColor color, Position position) : base(color, position)
    {
    }

    public override List<Position> GetValidMoves(Board board)
    {
        List<(int Row, int Column)> steps = new List<(int Row, int Column)>
        {
            (-1, -1), (-1, 0), (-1, 1),
            (0, -1),           (0, 1),
            (1, -1),  (1, 0),  (1, 1)
        };

        return GetStepMoves(board, steps);
    }
}
