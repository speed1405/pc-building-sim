using Spectre.Console;
using SiliconLegacy.Models;
using SiliconLegacy.Engine;

namespace SiliconLegacy.UI;

public class ConsoleUI
{
    private readonly GameEngine _engine;
    
    public ConsoleUI(GameEngine engine)
    {
        _engine = engine;
    }

    public void ShowMainMenu(GameState gameState, List<Era> eras, List<Part> allParts)
    {
        while (true)
        {
            Console.Clear();
            ShowHeader(gameState);

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]SILICON LEGACY - Main Menu[/]")
                    .AddChoices(new[] {
                        "View Workbench",
                        "Manage Inventory",
                        "View Quests",
                        "Shop (Buy Parts)",
                        "View Eras",
                        "Test Boot PC",
                        "Exit Game"
                    }));

            switch (choice)
            {
                case "View Workbench":
                    ShowWorkbench(gameState);
                    break;
                case "Manage Inventory":
                    ManageInventory(gameState);
                    break;
                case "View Quests":
                    ShowQuests(gameState);
                    break;
                case "Shop (Buy Parts)":
                    ShopParts(gameState, allParts, eras);
                    break;
                case "View Eras":
                    ShowEras(eras);
                    break;
                case "Test Boot PC":
                    TestBootPC(gameState);
                    break;
                case "Exit Game":
                    return;
            }
        }
    }

    private void ShowHeader(GameState gameState)
    {
        var table = new Table();
        table.Border(TableBorder.Heavy);
        table.AddColumn(new TableColumn("[yellow]SILICON LEGACY[/]").Centered());
        
        var info = $"[cyan]Year:[/] {gameState.CurrentYear} | " +
                   $"[green]Bank:[/] ${gameState.Money:F2} | " +
                   $"[yellow]Reputation:[/] {new string('★', Math.Min(gameState.Reputation / 20, 5))}";
        
        table.AddRow(info);
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();
    }

    private void ShowWorkbench(GameState gameState)
    {
        Console.Clear();
        ShowHeader(gameState);

        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.AddColumn("[yellow]Component[/]");
        table.AddColumn("[green]Installed Part[/]");

        var pc = gameState.Workbench;
        
        table.AddRow("CASE", pc.Case?.Name ?? "[red]Empty[/]");
        table.AddRow("MOTHERBOARD", pc.Motherboard?.Name ?? "[red]Empty[/]");
        table.AddRow("CPU", pc.CPU?.Name ?? "[red]Empty[/]");
        table.AddRow("RAM", pc.RAM.Count > 0 ? string.Join(", ", pc.RAM.Select(r => r.Name)) : "[red]Empty[/]");
        table.AddRow("GPU", pc.GPU?.Name ?? "[red]Empty[/]");
        table.AddRow("STORAGE", pc.Storage.Count > 0 ? string.Join(", ", pc.Storage.Select(s => s.Name)) : "[red]Empty[/]");
        table.AddRow("PSU", pc.PSU?.Name ?? "[red]Empty[/]");
        table.AddRow("COOLING", pc.Cooling?.Name ?? "[red]Empty[/]");

        AnsiConsole.Write(table);
        
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"[cyan]Total Power Draw:[/] {pc.TotalPowerConsumption}W");
        AnsiConsole.MarkupLine($"[cyan]Total TDP:[/] {pc.TotalTDP}W");
        
        AnsiConsole.WriteLine();
        AnsiConsole.Markup("[grey]Press any key to continue...[/]");
        Console.ReadKey(true);
    }

    private void ManageInventory(GameState gameState)
    {
        Console.Clear();
        ShowHeader(gameState);

        if (gameState.Inventory.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]Your inventory is empty![/]");
            AnsiConsole.Markup("[grey]Press any key to continue...[/]");
            Console.ReadKey(true);
            return;
        }

        var partChoices = gameState.Inventory.Select(p => $"{p.Name} ({p.Type})").ToList();
        partChoices.Add("Back to Main Menu");

        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Select a part to install:[/]")
                .PageSize(10)
                .AddChoices(partChoices));

        if (choice == "Back to Main Menu")
            return;

        var selectedPart = gameState.Inventory[partChoices.IndexOf(choice)];
        
        var action = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"[yellow]What would you like to do with {selectedPart.Name}?[/]")
                .AddChoices(new[] { "Install on Workbench", "View Details", "Cancel" }));

        if (action == "Install on Workbench")
        {
            if (_engine.InstallPart(gameState, selectedPart))
            {
                AnsiConsole.MarkupLine($"[green]✓ {selectedPart.Name} installed successfully![/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]✗ Cannot install {selectedPart.Name} - compatibility issues![/]");
            }
            AnsiConsole.Markup("[grey]Press any key to continue...[/]");
            Console.ReadKey(true);
        }
        else if (action == "View Details")
        {
            ShowPartDetails(selectedPart);
        }
    }

    private void ShowPartDetails(Part part)
    {
        Console.Clear();
        
        var panel = new Panel($"""
            [yellow]Name:[/] {part.Name}
            [yellow]Type:[/] {part.Type}
            [yellow]Price:[/] ${part.Price:F2}
            [yellow]Description:[/] {part.Description}
            
            [cyan]Technical Specs:[/]
            Socket: {part.SocketType ?? "N/A"}
            Slot: {part.SlotType ?? "N/A"}
            Power: {part.PowerConsumption?.ToString() ?? "N/A"}W
            TDP: {part.TDP?.ToString() ?? "N/A"}W
            """)
        {
            Header = new PanelHeader($"[green]{part.Name}[/]"),
            Border = BoxBorder.Rounded
        };

        AnsiConsole.Write(panel);
        AnsiConsole.WriteLine();
        AnsiConsole.Markup("[grey]Press any key to continue...[/]");
        Console.ReadKey(true);
    }

    private void ShowQuests(GameState gameState)
    {
        Console.Clear();
        ShowHeader(gameState);

        if (gameState.AvailableQuests.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No quests available at the moment.[/]");
            AnsiConsole.Markup("[grey]Press any key to continue...[/]");
            Console.ReadKey(true);
            return;
        }

        // Show active quests first if any
        var activeQuests = gameState.AvailableQuests.Where(q => q.IsActive).ToList();
        if (activeQuests.Any())
        {
            AnsiConsole.MarkupLine("[green]═══ Active Quests ═══[/]\n");
            var activeTable = new Table();
            activeTable.Border(TableBorder.Rounded);
            activeTable.AddColumn("[yellow]Quest[/]");
            activeTable.AddColumn("[green]Reward[/]");
            activeTable.AddColumn("[cyan]Type[/]");
            activeTable.AddColumn("[magenta]Progress[/]");

            foreach (var quest in activeQuests)
            {
                var completedReqs = quest.Requirements.Count(r => r.IsMet);
                var totalReqs = quest.Requirements.Count;
                var progress = $"{completedReqs}/{totalReqs}";
                activeTable.AddRow(quest.Name, $"${quest.Reward:F2}", quest.Type.ToString(), progress);
            }

            AnsiConsole.Write(activeTable);
            AnsiConsole.WriteLine();
        }

        // Show available quests
        var availableQuests = gameState.AvailableQuests.Where(q => !q.IsActive).ToList();
        if (availableQuests.Any())
        {
            AnsiConsole.MarkupLine("[cyan]═══ Available Quests ═══[/]\n");
            var table = new Table();
            table.Border(TableBorder.Rounded);
            table.AddColumn("[yellow]Quest[/]");
            table.AddColumn("[green]Reward[/]");
            table.AddColumn("[cyan]Type[/]");

            foreach (var quest in availableQuests)
            {
                table.AddRow(quest.Name, $"${quest.Reward:F2}", quest.Type.ToString());
            }

            AnsiConsole.Write(table);
        }

        AnsiConsole.WriteLine();
        
        // Prompt to view/start quest - create a dictionary to map choices to quests
        var questChoiceMap = new Dictionary<string, Quest>();
        var questChoices = new List<string>();
        
        foreach (var quest in gameState.AvailableQuests)
        {
            var displayName = quest.IsActive ? $"[green]★[/] {quest.Name}" : quest.Name;
            questChoices.Add(displayName);
            questChoiceMap[displayName] = quest;
        }
        questChoices.Add("Back to Main Menu");

        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Select a quest to view details or start:[/]")
                .PageSize(10)
                .AddChoices(questChoices));

        if (choice == "Back to Main Menu")
            return;

        // Find the selected quest using the dictionary
        if (questChoiceMap.TryGetValue(choice, out var selectedQuest))
        {
            ShowQuestDetails(gameState, selectedQuest);
        }
    }

    private void ShowQuestDetails(GameState gameState, Quest quest)
    {
        Console.Clear();
        ShowHeader(gameState);

        var statusColor = quest.IsActive ? "green" : "yellow";
        var statusText = quest.IsActive ? "ACTIVE" : "AVAILABLE";

        var panel = new Panel($"""
            [yellow]Quest:[/] {quest.Name}
            [yellow]Status:[/] [{statusColor}]{statusText}[/{statusColor}]
            [yellow]Type:[/] {quest.Type}
            [yellow]Reward:[/] ${quest.Reward:F2} + {quest.ReputationReward} reputation
            
            [cyan]Description:[/]
            {quest.Description}
            """)
        {
            Header = new PanelHeader($"[{statusColor}]{quest.Name}[/{statusColor}]"),
            Border = BoxBorder.Double
        };

        AnsiConsole.Write(panel);
        AnsiConsole.WriteLine();

        // Show requirements
        AnsiConsole.MarkupLine("[cyan]Requirements:[/]");
        var reqTable = new Table();
        reqTable.Border(TableBorder.Rounded);
        reqTable.AddColumn("[cyan]Requirement[/]");
        reqTable.AddColumn("[green]Status[/]");

        foreach (var req in quest.Requirements)
        {
            var status = req.IsMet ? "[green]✓ Complete[/]" : "[yellow]○ Pending[/]";
            reqTable.AddRow(req.Description, status);
        }

        AnsiConsole.Write(reqTable);
        AnsiConsole.WriteLine();

        // Show action options
        var actions = new List<string>();
        if (!quest.IsActive)
        {
            actions.Add("Start Quest");
        }
        else
        {
            actions.Add("View Progress");
        }
        actions.Add("Back to Quest List");

        var action = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]What would you like to do?[/]")
                .AddChoices(actions));

        if (action == "Start Quest")
        {
            if (_engine.StartQuest(gameState, quest))
            {
                AnsiConsole.MarkupLine("[green]✓ Quest started! Check your workbench to begin.[/]");
                AnsiConsole.Markup("[grey]Press any key to continue...[/]");
                Console.ReadKey(true);
            }
        }
        else if (action == "View Progress")
        {
            AnsiConsole.MarkupLine($"[cyan]Progress: {quest.Requirements.Count(r => r.IsMet)}/{quest.Requirements.Count} requirements completed[/]");
            AnsiConsole.Markup("[grey]Press any key to continue...[/]");
            Console.ReadKey(true);
        }
    }

    private void ShopParts(GameState gameState, List<Part> allParts, List<Era> eras)
    {
        Console.Clear();
        ShowHeader(gameState);

        var currentEra = eras.FirstOrDefault(e => e.Id == gameState.CurrentEraId);
        var availableParts = allParts.Where(p => p.EraId <= gameState.CurrentEraId).ToList();

        if (availableParts.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No parts available for purchase![/]");
            AnsiConsole.Markup("[grey]Press any key to continue...[/]");
            Console.ReadKey(true);
            return;
        }

        var partChoices = availableParts.Select(p => $"{p.Name} - ${p.Price:F2} ({p.Type})").ToList();
        partChoices.Add("Back to Main Menu");

        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"[yellow]Shop - {currentEra?.Name ?? "Unknown Era"}[/]")
                .PageSize(10)
                .AddChoices(partChoices));

        if (choice == "Back to Main Menu")
            return;

        var selectedPart = availableParts[partChoices.IndexOf(choice)];

        if (gameState.Money >= selectedPart.Price)
        {
            var confirm = AnsiConsole.Confirm($"Buy {selectedPart.Name} for ${selectedPart.Price:F2}?");
            if (confirm)
            {
                gameState.Money -= selectedPart.Price;
                gameState.Inventory.Add(selectedPart);
                AnsiConsole.MarkupLine($"[green]✓ Purchased {selectedPart.Name}![/]");
                AnsiConsole.Markup("[grey]Press any key to continue...[/]");
                Console.ReadKey(true);
            }
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Not enough money![/]");
            AnsiConsole.Markup("[grey]Press any key to continue...[/]");
            Console.ReadKey(true);
        }
    }

    private void ShowEras(List<Era> eras)
    {
        Console.Clear();

        var table = new Table();
        table.Border(TableBorder.Heavy);
        table.AddColumn("[yellow]Era[/]");
        table.AddColumn("[cyan]Period[/]");
        table.AddColumn("[green]Focus[/]");
        table.AddColumn("[magenta]Status[/]");

        foreach (var era in eras)
        {
            var status = era.IsUnlocked ? "[green]Unlocked[/]" : "[red]Locked[/]";
            table.AddRow(era.Name, $"{era.StartYear}-{era.EndYear}", era.Focus, status);
        }

        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();
        AnsiConsole.Markup("[grey]Press any key to continue...[/]");
        Console.ReadKey(true);
    }

    private void TestBootPC(GameState gameState)
    {
        Console.Clear();
        ShowHeader(gameState);

        AnsiConsole.Status()
            .Start("[yellow]Testing PC boot...[/]", ctx =>
            {
                ctx.Spinner(Spinner.Known.Dots);
                ctx.SpinnerStyle(Style.Parse("green"));
                Thread.Sleep(2000);
            });

        var canBoot = _engine.TestBoot(gameState.Workbench);

        if (canBoot)
        {
            AnsiConsole.MarkupLine("[green]✓ PC boots successfully![/]");
            AnsiConsole.MarkupLine("[cyan]POST completed. All systems operational.[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[red]✗ PC failed to boot![/]");
            AnsiConsole.MarkupLine("[yellow]Check that all essential components are installed:[/]");
            
            var pc = gameState.Workbench;
            if (pc.Case == null) AnsiConsole.MarkupLine("  [red]• Missing: Case[/]");
            if (pc.Motherboard == null) AnsiConsole.MarkupLine("  [red]• Missing: Motherboard[/]");
            if (pc.CPU == null) AnsiConsole.MarkupLine("  [red]• Missing: CPU[/]");
            if (pc.RAM.Count == 0) AnsiConsole.MarkupLine("  [red]• Missing: RAM[/]");
            if (pc.PSU == null) AnsiConsole.MarkupLine("  [red]• Missing: PSU[/]");
        }

        AnsiConsole.WriteLine();
        AnsiConsole.Markup("[grey]Press any key to continue...[/]");
        Console.ReadKey(true);
    }
}
