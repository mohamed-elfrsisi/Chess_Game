namespace Chess_Game.UI.Console;
using Chess_Game.Models;
using Chess_Game.Exceptions;

public class InputParser
{

    private Position ParsePosition(string input)
    {
        if(input.Length is not 2)
        throw new InvalidInputException($"'{input}' is not square, for example use e2");

        char file = char.ToLower(input[0]);
        char rank = input[1];

        if(file<'a' || file > 'h')
        throw new InvalidInputException("the letter most be between a and h");

        if(rank< '1'|| rank>'8')
        throw new InvalidInputException("the number must be between 1 and 8");

        int column = file - 'a';
        int row = 8 - (rank - '0');

        return new Position(row,column);

    }



    public (Position From,Position To) ParseMove(string input)
    {
        string[] parts = input.Trim().Split(' ');

        if(parts.Length is not 2 )
        throw new InvalidInputException("Please type a move like this: e2 e4");

        Position from = ParsePosition(parts[0]);
        Position to = ParsePosition(parts[1]);

        return (from,to);
    }
}
