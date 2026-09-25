namespace Chess_Game.Exceptions;

// Task: Mohamed
// Goal: A custom error for when the player types text that isn't a real square/move (like "e9").
// What to do: Inherit from Exception. Add a constructor that takes a string message
// and passes it to the base Exception class.
public class InvalidInputException : Exception
{
    // TODO: add constructor(string message) : base(message)
}
