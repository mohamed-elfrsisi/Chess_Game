using Chess_Game.Enums;
using Chess_Game.Exceptions;
using Chess_Game.Models;
using Chess_Game.Models.Pieces;
using static System.Console;

namespace Chess_Game.UI.Console;

public class MenuManager
{
    private InputParser parser = new InputParser();

    
    public void Start()
    {
        bool running = true;

        while (running)
        {
            ShowMainMenu();
            string choice = ReadText("Choose: ");

            if (choice == "1")
            {
                Game game = CreateGame();
                PlayGame(game);
            }
            else if (choice == "2")
            {
                WriteLine("Goodbye!");
                running = false;
            }
            else
            {
                WriteLine("Invalid choice.");
                ReadText("Press Enter to continue...");
            }
        }
    }

    private void ShowMainMenu()
    {
        Clear();
        WriteLine("=== Console Chess ===");
        WriteLine("1. New Game");
        WriteLine("2. Exit");
    }

    private Game CreateGame()
    {
        string whiteName = ReadName("White player name: ");
        string blackName = ReadName("Black player name: ");

        Player white = new Player(whiteName, PieceColor.White);
        Player black = new Player(blackName, PieceColor.Black);

        return new Game(white, black);
    }

    private void PlayGame(Game game)
    {
        string message = "";

        while (!game.IsGameOver())
        {
            ShowBoard(game.Board);
            ShowTurnInfo(game);

            if (message != "")
                WriteLine(message);

            string input = ReadText("Enter move (e2 e4) or 'quit': ");

            if (input.ToLower() == "quit")
                return;

            message = TryPlayMove(game, input);
        }

        ShowResult(game);
    }

    private string TryPlayMove(Game game, string input)
    {
        try
        {
            (Position fromSquare, Position toSquare) = parser.ParseMove(input);
            game.MakeMove(fromSquare, toSquare);
            return "";
        }
        catch (InvalidInputException ex)
        {
            return "Bad input: " + ex.Message;
        }
        catch (NotYourTurnException ex)
        {
            return "Not your turn: " + ex.Message;
        }
        catch (InvalidMoveException ex)
        {
            return "Invalid move: " + ex.Message;
        }
    }

    private void ShowTurnInfo(Game game)
    {
        WriteLine();
        WriteLine("Turn: " + game.CurrentTurn);

        if (game.Status == GameStatus.Check)
            WriteLine("CHECK!");
    }

    private void ShowResult(Game game)
    {
        ShowBoard(game.Board);
        WriteLine();

        if (game.Status == GameStatus.Checkmate)
        {
            
            PieceColor winner = PieceColor.White;

            if (game.CurrentTurn == PieceColor.White)
                winner = PieceColor.Black;

            WriteLine("Checkmate! " + winner + " wins.");
        }
        else
        {
            WriteLine("Stalemate! It is a draw.");
        }

        ReadText("Press Enter to go back to the menu...");
    }

    private void ShowBoard(Board board)
    {
        Clear();
        WriteLine("    a b c d e f g h");
        WriteLine("  +-----------------+");

        for (int row = 0; row < 8; row++)
        {
            Write((8 - row) + " | ");

            for (int column = 0; column < 8; column++)
            {
                Piece? piece = board.GetPiece(new Position(row, column));
                Write(GetPieceText(piece) + " ");
            }

            WriteLine("| " + (8 - row));
        }

        WriteLine("  +-----------------+");
        WriteLine("    a b c d e f g h");
    }

    private string GetPieceText(Piece? piece)
    {
        if (piece == null)
            return ".";

        if (piece.Color == PieceColor.White)
            return char.ToUpper(piece.Symbol).ToString();

        return char.ToLower(piece.Symbol).ToString();
    }

    private string ReadName(string message)
    {
        string name = ReadText(message);

        while (name == "")
        {
            WriteLine("Name can't be empty.");
            name = ReadText(message);
        }

        return name;
    }

    private string ReadText(string message)
    {
        Write(message);
        string? text = ReadLine();

        if (text == null)
            return "";

        return text.Trim();
    }
}