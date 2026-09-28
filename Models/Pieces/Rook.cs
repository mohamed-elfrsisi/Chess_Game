namespace Chess_Game.Models.Pieces;
using Chess_Game.Enums;


public class Rook : Piece
{
    public override char Symbol => 'R';

    public Rook(PieceColor color,Position position) : base(color, position)
    {
        
    }

    public override List<Position> GetValidMoves(Board board)
    {
        List<(int Row,int Column)> directions = new List<(int Row,int Column)>
        {
          (-1,0), //up
          (1,0), //down
          (0,-1), // left
          (0,1) //right  
        };
        return GetSlidingMoves(board, directions);
    }
}
