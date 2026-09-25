namespace Chess_Game.Logic;

// Task: Ayman
// Goal: Know the "big picture" rules of chess: is a king in check, is it
// checkmate, is it stalemate.
// What to do:
//  1) IsKingInCheck(board, color): find that color's king, then check if any
//     enemy piece's moves include the king's square.
//  2) MoveLeavesKingInCheck(board, from, to): copy the board (Board.Clone()),
//     make the move on the copy, then call IsKingInCheck on the copy.
//  3) GetLegalMoves(board, piece): the piece's normal moves, minus any move
//     that would leave its own king in check.
//  4) HasAnyLegalMove(board, color): true if that player has at least one
//     legal move anywhere on the board.
//  5) GetGameStatus(game): combine the above into InProgress / Check /
//     Checkmate / Stalemate for whoever's turn it is.
public class GameRules
{
    // TODO: implement IsKingInCheck
    // TODO: implement MoveLeavesKingInCheck
    // TODO: implement GetLegalMoves
    // TODO: implement HasAnyLegalMove
    // TODO: implement GetGameStatus
}
