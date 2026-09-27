
using Chess_Game.Enums;

namespace Chess_Game.Models.Pieces;

public class Bishop : Piece
{

    public override char Symbol => 'B';

    public Bishop(PieceColor color, Position position)

        : base(color, position)
    {
    }


    public override List<Position> GetValidMoves(Board board)
    {

        var directions = new List<(int Row, int Column)>
        {

            (-1, -1),


            (-1, 1),

  
            (1, -1),

            (1, 1)
        };
        return GetSlidingMoves(board, directions);
    }
}