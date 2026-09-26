namespace Chess_Game.Exceptions;
public class InvalidMoveException : Exception
{
    public InvalidMoveException(string Message):base(Message){}
}
