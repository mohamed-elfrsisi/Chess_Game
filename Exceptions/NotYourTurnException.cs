namespace Chess_Game.Exceptions;

// Task: Mohamed
// Goal: A custom error for when a player tries to move a piece that isn't theirs.
// What to do: Inherit from Exception. Add a constructor that takes a string message
// and passes it to the base Exception class.
public class NotYourTurnException : Exception
{
    // TODO: add constructor(string message) : base(message)
}
