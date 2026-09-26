namespace Chess_Game.Exceptions;

public class NotYourTurnException : Exception
{
    public NotYourTurnException(string Message):base(Message){}
}
