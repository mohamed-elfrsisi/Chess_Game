namespace Chess_Game.Models;

// Task: Omar
// Goal: Tie everything together: the board, both players, whose turn it is,
// the game status, and the move history.
// What to do:
//  1) Store Board, WhitePlayer, BlackPlayer, CurrentTurn, Status, MoveHistory.
//  2) MakeMove(from, to): validate the move (MoveValidator), move the piece,
//     record it in MoveHistory, check for pawn promotion, switch turns,
//     then update Status (GameRules).
//  3) PromotePawnIfNeeded: if a pawn reaches the far row, replace it with a Queen.
//  4) IsGameOver(): true when Status is Checkmate or Stalemate.
public class Game
{
    // TODO: add Board, WhitePlayer, BlackPlayer, CurrentTurn, Status, MoveHistory
    // TODO: implement MakeMove(Position from, Position to)
    // TODO: implement PromotePawnIfNeeded
    // TODO: implement IsGameOver
}
