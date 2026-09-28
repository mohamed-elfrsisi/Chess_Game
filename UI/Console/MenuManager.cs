using Chess_Game.Enums;
using Chess_Game.Exceptions;
using Chess_Game.Models;
using Chess_Game.Models.Pieces;
using static System.Console;

namespace Chess_Game.UI.Console;

public class MenuManager
{
    // Parses player input such as "e2 e4".
    private readonly InputParser parser = new InputParser();

    public void Start()
    {
        // Needed to show the Unicode chess pieces and box lines.
        OutputEncoding = System.Text.Encoding.UTF8;

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

    // Displays the main menu.
    private void ShowMainMenu()
    {
        Clear();
        WriteLine("╔══════════════════════════════════╗");
        WriteLine("║        ♟ CONSOLE CHESS           ║");
        WriteLine("╠══════════════════════════════════╣");
        WriteLine("║                                  ║");
        WriteLine("║      1. New Game                 ║");
        WriteLine("║      2. Exit                     ║");
        WriteLine("║                                  ║");
        WriteLine("╚══════════════════════════════════╝");
    }

    // Creates the two players and starts a new game.
    private Game CreateGame()
    {
        WriteLine();
        WriteLine("════════════ NEW GAME ════════════");
        WriteLine();

        string whiteName = ReadName("White player name: ");
        string blackName = ReadName("Black player name: ");

        Player white = new Player(whiteName, PieceColor.White);
        Player black = new Player(blackName, PieceColor.Black);

        return new Game(white, black);
    }

    // Main game loop: display board, get move, and play it.
    private void PlayGame(Game game)
    {
        string message = "";

        while (!game.IsGameOver())
        {
            ShowBoard(game.Board);
            ShowTurnInfo(game);

            // Show an error from the previous move, if any.
            if (message != "")
                WriteLine(message);

            WriteLine();
            string input = ReadText("Enter move (e2 e4) or 'quit': ");

            if (input.Equals("quit", StringComparison.OrdinalIgnoreCase))
                return;

            message = TryPlayMove(game, input);
        }

        ShowResult(game);
    }

    // Parses and validates the move through the Game class.
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

    // Shows whose turn it is and whether the player is in check.
    private void ShowTurnInfo(Game game)
    {
        WriteLine();

        string playerName = GetPlayerName(game, game.CurrentTurn);
        WriteLine($"Turn: {game.CurrentTurn} ({playerName})");

        if (game.Status == GameStatus.Check)
        {
            ForegroundColor = ConsoleColor.Red;
            WriteLine("⚠ CHECK!");
            ResetColor();
        }
    }

    // Displays the final result when the game ends.
    private void ShowResult(Game game)
    {
        ShowBoard(game.Board);
        WriteLine();
        WriteLine("════════════ GAME OVER ════════════");
        WriteLine();

        if (game.Status == GameStatus.Checkmate)
        {
            // The player whose turn it is has been checkmated.
            PieceColor winner = PieceColor.White;

            if (game.CurrentTurn == PieceColor.White)
                winner = PieceColor.Black;

            string winnerName = GetPlayerName(game, winner);

            ForegroundColor = ConsoleColor.Green;
            WriteLine($"♚ Checkmate! {winnerName} ({winner}) wins!");
            ResetColor();
        }
        else
        {
            // Stalemate means the game ends in a draw.
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("Stalemate! The game is a draw.");
            ResetColor();
        }

        WriteLine();
        ReadText("Press Enter to go back to the menu...");
    }

    // Returns the name of the player who has the given color.
    private string GetPlayerName(Game game, PieceColor color)
    {
        if (color == PieceColor.White)
            return game.WhitePlayer.Name;

        return game.BlackPlayer.Name;
    }

    // Draws the chess board with colored squares and Unicode pieces.
    private void ShowBoard(Board board)
    {
        Clear();
        WriteLine();
        WriteLine("             CHESS BOARD");
        WriteLine();
        WriteLine("     a   b   c   d   e   f   g   h");
        WriteLine("   ┌───┬───┬───┬───┬───┬───┬───┬───┐");

        for (int row = 0; row < 8; row++)
        {
            int chessRow = 8 - row;
            Write($" {chessRow} │");

            for (int column = 0; column < 8; column++)
            {
                Position position = new Position(row, column);
                Piece? piece = board.GetPiece(position);

                // Alternate the background color like a real chess board.
                bool isLightSquare = (row + column) % 2 == 0;
                SetSquareColor(isLightSquare);

                if (piece == null)
                {
                    Write("   ");
                }
                else
                {
                    SetPieceColor(piece);
                    Write($" {GetPieceText(piece)} ");
                }

                ResetColor();
                Write("│");
            }

            WriteLine($" {chessRow}");

            if (row < 7)
                WriteLine("   ├───┼───┼───┼───┼───┼───┼───┼───┤");
        }

        WriteLine("   └───┴───┴───┴───┴───┴───┴───┴───┘");
        WriteLine("     a   b   c   d   e   f   g   h");
        ResetColor();
    }

    // Sets the square background color.
    private void SetSquareColor(bool isLightSquare)
    {
        if (isLightSquare)
            BackgroundColor = ConsoleColor.Gray;
        else
            BackgroundColor = ConsoleColor.DarkGray;
    }

    // Sets the piece color according to its chess color.
    private void SetPieceColor(Piece piece)
    {
        if (piece.Color == PieceColor.White)
            ForegroundColor = ConsoleColor.White;
        else
            ForegroundColor = ConsoleColor.Black;
    }

    // Converts internal symbols (K, Q, R...) to Unicode chess symbols.
    private string GetPieceText(Piece piece)
    {
        if (piece.Color == PieceColor.White)
        {
            return piece.Symbol switch
            {
                'K' => "♔",
                'Q' => "♕",
                'R' => "♖",
                'B' => "♗",
                'N' => "♘",
                'P' => "♙",
                _ => "?"
            };
        }

        return piece.Symbol switch
        {
            'K' => "♚",
            'Q' => "♛",
            'R' => "♜",
            'B' => "♝",
            'N' => "♞",
            'P' => "♟",
            _ => "?"
        };
    }

    // Reads a player name and prevents empty names.
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

    // Reads and cleans user input from the console.
    private string ReadText(string message)
    {
        Write(message);
        string? text = ReadLine();

        if (text == null)
            return "";

        return text.Trim();
    }
}