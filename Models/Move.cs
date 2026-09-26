namespace Chess_Game.Models;
using Chess_Game.Models.Pieces;
// Task: Mohamed
// Goal: Remember one move that was played (which piece, from where, to where,
// and what it captured, if anything).
// What to do:
//  1) Add properties: From, To, MovedPiece, CapturedPiece (CapturedPiece can be null).
//  2) Add a constructor that sets them.
//  3) Add a ToString() that prints something like "P: e2 -> e4".
public class Move
{
    // TODO: add From, To, MovedPiece, CapturedPiece properties
    Position From{get;}
    Position To{get;}

    Piece MovedPiece {get;}
    Piece? CapturedPiece{get;}

    // TODO: add constructor
    public Move(Position from,Position to,Piece movedPiece, Piece? capturedPiece)
    {
        From = from;
        To = to;
        MovedPiece = movedPiece;
        CapturedPiece = capturedPiece;
    }

    public override string ToString()
    {
        string text = MovedPiece.Symbol + ": " + From.ToAlgebraic + " -> " + To.ToAlgebraic;
        if(CapturedPiece is not null)
            text+=$"(captured {CapturedPiece.Symbol})";

        return text;
    }
}
