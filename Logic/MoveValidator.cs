namespace Chess_Game.Logic;

// Task: Saeid (on your own this time)
// Goal: Check if a move the player typed is actually allowed, and throw the
// right exception explaining why if it's not.
// What to do, check in this order:
//  1) Is there a piece on the starting square?
//  2) Is it that player's turn?
//  3) Is the target square on the board?
//  4) Is the player trying to "move" to the same square?
//  5) Do they already have their own piece on the target square?
//  6) Is the target square in the piece's normal move list (piece.GetValidMoves)?
//  7) Would the move leave their own king in check (use GameRules)?
public class MoveValidator
{
    // TODO: implement ValidateMove(Game game, Position from, Position to)
}
