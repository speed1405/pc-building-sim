namespace SiliconLegacy.Models;

public class Era
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int StartYear { get; set; }
    public int EndYear { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Focus { get; set; } = string.Empty;
    public string KeyMilestone { get; set; } = string.Empty;
    public bool IsUnlocked { get; set; }
}
