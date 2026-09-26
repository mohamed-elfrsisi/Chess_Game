namespace Chess_Game.Models;

public class Position
{

    public int Row{get;}
    public int Column{get;}


    public Position(int row,int column)
    {
        Row = row;
        Column = column;
    }
    //ToAlgebraic() make inputs(row,column) from (1,6) to "a6"

    public string ToAlgebraic()
    {
        char file = (char)('a' + Column);
        int rank = 8 - Row;
        return $"{file}{rank}";
    }

}
