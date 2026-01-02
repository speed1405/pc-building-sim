namespace SiliconLegacy.Models;

public class GameState
{
    public decimal Money { get; set; } = 500m;
    public int Reputation { get; set; } = 0;
    public int CurrentYear { get; set; } = 1990;
    public int CurrentEraId { get; set; } = 1;
    public PC Workbench { get; set; } = new();
    public List<Part> Inventory { get; set; } = new();
    public List<Quest> AvailableQuests { get; set; } = new();
    public List<Quest> CompletedQuests { get; set; } = new();
}
