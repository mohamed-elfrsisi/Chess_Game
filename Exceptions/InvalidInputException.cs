namespace Chess_Game.Exceptions;

public class InvalidInputException : Exception
{
    public InvalidInputException(string Message):base(Message){}
}
