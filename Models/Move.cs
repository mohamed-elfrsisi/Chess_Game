namespace Chess_Game.Models;
using Chess_Game.Models.Pieces;

public class Move
{

    Position From{get;}
    Position To{get;}

    Piece MovedPiece {get;}
    Piece? CapturedPiece{get;}


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
