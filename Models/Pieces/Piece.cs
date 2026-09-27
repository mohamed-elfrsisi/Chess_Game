using Chess_Game.Enums;

namespace Chess_Game.Models.Pieces;

public abstract class Piece
{

    public PieceColor Color{get;}
    public Position Position{get;set;}

        public abstract char Symbol{get;}

    public Piece(PieceColor color, Position position)
    {
        Color = color;
        Position = position;
    }
        //each piece has a differnet move soooo we are make this abstarct List to define each move for each piece 
        // and define each move valid or not based on current board
        public abstract List<Position> GetValidMoves(Board board);



    //all squares that piece can move on it when moves in straight line until captured anything
    //(Rook  -  Bishop  -  Queen)
   protected List<Position> GetSlidingMoves(Board board,List<(int Row,int Column)> directions )
    {
        //moves list ==> to store all positions that piece can move on 
        List<Position> moves = new List<Position>();

        foreach(var direction in directions)
        {
            int row = Position.Row + direction.Row;
            int column = Position.Column + direction.Column;

            while(board.IsInside(new Position(row, column)))
            {
                Position target = new Position (row,column);
                Piece? pieceOnTarget = board.GetPiece(target);

                if(pieceOnTarget is null)
                moves.Add(target);

                else
                {
                    if(pieceOnTarget.Color != null)
                    moves.Add(target);
                    break;
                }
                row +=direction.Row;
                column +=direction.Column;


            }
        }
        return moves;
    }





// look at spesific squares that piece can go to, and check all squares at one time
// (King  -  Knight)
protected List<Position> GetStepMoves(Board board, List<(int Row,int Column)> steps)
    {
        List<Position> moves = new List<Position>();
        foreach(var step in steps)
        {
            Position target = new Position(Position.Row + step.Row, Position.Column + step.Column);

            if(!board.IsInside(target))
            continue;

            Piece? pieceOnTarget = board.GetPiece(target);

            if(pieceOnTarget ==null || pieceOnTarget.Color != Color)
            moves.Add(target);
        }
        return moves;
    }












}
