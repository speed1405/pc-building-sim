using Spectre.Console;
using SiliconLegacy.Data;
using SiliconLegacy.Engine;
using SiliconLegacy.Models;
using SiliconLegacy.UI;

namespace SiliconLegacy.App;

class Program
{
    static void Main(string[] args)
    {
        // Display welcome screen
        DisplayWelcome();

        // Get the data path
        var dataPath = GetDataPath();
        
        // Initialize services
        var dataLoader = new DataLoader(dataPath);
        var gameEngine = new GameEngine();
        var consoleUI = new ConsoleUI(gameEngine);

        // Load game data
        var eras = dataLoader.LoadEras();
        var parts = dataLoader.LoadParts();
        var quests = dataLoader.LoadQuests();

        // Initialize game state
        var gameState = new GameState
        {
            Money = 1000m,
            Reputation = 0,
            CurrentYear = 1990,
            CurrentEraId = 1,
            Workbench = new PC(),
            Inventory = new List<Part>(),
            AvailableQuests = quests.Where(q => q.EraId == 1).ToList(),
            CompletedQuests = new List<Quest>()
        };

        // Add some starting parts to inventory
        var starterParts = parts.Where(p => p.EraId == 1).Take(3).ToList();
        gameState.Inventory.AddRange(starterParts);

        // Start the game
        try
        {
            consoleUI.ShowMainMenu(gameState, eras, parts);
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex);
        }

        AnsiConsole.MarkupLine("[yellow]Thanks for playing Silicon Legacy![/]");
    }

    static void DisplayWelcome()
    {
        Console.Clear();
        
        var rule = new Rule("[yellow]SILICON LEGACY[/]")
        {
            Style = Style.Parse("yellow"),
            Justification = Justify.Center
        };
        AnsiConsole.Write(rule);
        
        AnsiConsole.WriteLine();
        
        var panel = new Panel(
            "[cyan]PC Building Simulator[/]\n" +
            "[white]Navigate through five decades of personal computing history[/]\n" +
            "[white]Build, repair, and upgrade PCs from 1990 to 2025[/]\n\n" +
            "[grey]Version 1.0.0[/]")
        {
            Header = new PanelHeader("[green]Welcome[/]"),
            Border = BoxBorder.Double,
            Padding = new Padding(2, 1)
        };
        
        AnsiConsole.Write(panel);
        AnsiConsole.WriteLine();
        
        AnsiConsole.Markup("[grey]Press any key to start...[/]");
        Console.ReadKey(true);
    }

    static string GetDataPath()
    {
        // Try to find the data directory relative to the executable
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        
        // Check if we're running from src/SiliconLegacy.App/bin
        var dataPath = Path.Combine(baseDir, "..", "..", "..", "..", "..", "data");
        if (Directory.Exists(dataPath))
            return Path.GetFullPath(dataPath);
        
        // Check if we're running from published build
        dataPath = Path.Combine(baseDir, "data");
        if (Directory.Exists(dataPath))
            return Path.GetFullPath(dataPath);
        
        // Default to current directory
        dataPath = Path.Combine(Directory.GetCurrentDirectory(), "data");
        if (Directory.Exists(dataPath))
            return Path.GetFullPath(dataPath);
        
        // As a last resort, go up from base directory
        dataPath = Path.Combine(baseDir, "..", "..", "..", "data");
        return Path.GetFullPath(dataPath);
    }
}
