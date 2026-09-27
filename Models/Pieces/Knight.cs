using Chess_Game.Enums;
//AYMAN
namespace Chess_Game.Models.Pieces;

// Knight inherits from the abstract class Piece,
// so it must implement Symbol and GetValidMoves.
public class Knight : Piece
{
    // The letter that represents the Knight on the board/console.
    public override char Symbol => 'N';

    // Constructor: just passes color and position to the base class (Piece).
    public Knight(PieceColor color, Position position) : base(color, position)
    {
    }

    // Returns all the valid squares this Knight can move to.
    public override List<Position> GetValidMoves(Board board)
    {
        // The Knight moves in an "L" shape: 8 possible jumps.
        // Each pair is (change in Row, change in Column).
        //LIST Include the  knight's 8 STEPS.
        List<(int Row, int Column)> steps = new List<(int Row, int Column)>
        {
            (-2, -1), (-2, 1),   // 2 up, 1 left/right
            (-1, -2), (-1, 2),   // 1 up, 2 left/right
            (1, -2),  (1, 2),    // 1 down, 2 left/right
            (2, -1),  (2, 1)     // 2 down, 1 left/right
        };

        // GetStepMoves (from the base Piece class) checks each jump:
        // - is it inside the board?
        // - is the target square empty, or has an enemy piece?
        // It returns only the valid ones.
        return GetStepMoves(board, steps);
    }
}