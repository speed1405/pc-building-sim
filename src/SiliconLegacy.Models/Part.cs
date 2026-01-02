namespace SiliconLegacy.Models;

public class Part
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public PartType Type { get; set; }
    public int EraId { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    
    // Compatibility properties
    public string? SocketType { get; set; }
    public string? SlotType { get; set; }
    public int? PowerConsumption { get; set; }
    public int? TDP { get; set; }
    public int? Capacity { get; set; }
    public string? FormFactor { get; set; }
    public List<string> Compatibility { get; set; } = new();
    
    // Performance properties
    public int? ClockSpeed { get; set; }
    public int? Cores { get; set; }
    public int? MemorySize { get; set; }
    
    public Dictionary<string, object> Properties { get; set; } = new();
}
