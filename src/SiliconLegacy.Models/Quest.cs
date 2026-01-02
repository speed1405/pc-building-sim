namespace SiliconLegacy.Models;

public class Quest
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int EraId { get; set; }
    public decimal Reward { get; set; }
    public int ReputationReward { get; set; }
    public QuestType Type { get; set; }
    public List<QuestRequirement> Requirements { get; set; } = new();
    public bool IsCompleted { get; set; }
    public bool IsActive { get; set; }
}

public enum QuestType
{
    Tutorial,
    Repair,
    Build,
    Upgrade,
    Diagnostic,
    Milestone
}

public class QuestRequirement
{
    public string Description { get; set; } = string.Empty;
    public RequirementType Type { get; set; }
    public string? PartType { get; set; }
    public string? MinSpec { get; set; }
    public bool IsMet { get; set; }
}

public enum RequirementType
{
    InstallPart,
    ConfigureBIOS,
    ConnectCable,
    TestBoot,
    AchieveScore,
    FixIssue
}
