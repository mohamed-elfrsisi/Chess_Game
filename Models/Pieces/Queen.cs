using Chess_Game.Enums;

namespace Chess_Game.Models.Pieces;

public class Queen : Piece
{

    public override char Symbol => 'Q';

    public Queen(PieceColor color,Position position) : base(color, position)
    {
        
    }

    public override List<Position> GetValidMoves(Board board)
    {
        //queen move like rook + bishop 

         List<(int Row, int Column)> directions = new List<(int Row, int Column)>
        {
            (-1, 0), (1, 0), (0, -1), (0, 1),   // straight lines (Rook)
            (-1, -1), (-1, 1), (1, -1), (1, 1)  // diagonals (Bishop)
        };

        return GetSlidingMoves(board, directions);
    }
}
