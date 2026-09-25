namespace Chess_Game.Models.Pieces;

// Task: Mohamed + Saeid (work on this one together)
// Goal: The trickiest piece. Pawns move forward only, can move two squares
// from their starting row, and capture only diagonally.
// What to do:
//  1) Set Symbol to 'P'.
//  2) Work out the forward direction based on color (White goes up, Black goes down).
//  3) Allow one square forward if it's empty.
//  4) Allow two squares forward only if the pawn is still on its starting row
//     and both squares are empty.
//  5) Allow a diagonal move only if there's an enemy piece there (capture only,
//     never move diagonally onto an empty square).
// Go slow on this one — talk through each rule out loud before writing it.
public class Pawn : Piece
{
    // TODO: add Symbol => 'P'
    // TODO: add constructor
    // TODO: implement GetValidMoves(Board board)
}
