using SiliconLegacy.Models;

namespace SiliconLegacy.Engine;

public class GameEngine
{
    private readonly CompatibilityEngine _compatibility;
    
    public GameEngine()
    {
        _compatibility = new CompatibilityEngine();
    }

    public bool InstallPart(GameState gameState, Part part)
    {
        var pc = gameState.Workbench;
        var issues = _compatibility.CheckCompatibility(pc, part);
        
        if (issues.Count > 0)
        {
            return false;
        }

        switch (part.Type)
        {
            case PartType.Case:
                pc.Case = part;
                break;
            case PartType.Motherboard:
                pc.Motherboard = part;
                break;
            case PartType.CPU:
                pc.CPU = part;
                break;
            case PartType.RAM:
                pc.RAM.Add(part);
                break;
            case PartType.GPU:
                pc.GPU = part;
                break;
            case PartType.Storage:
                pc.Storage.Add(part);
                break;
            case PartType.PSU:
                pc.PSU = part;
                break;
            case PartType.Cooling:
                pc.Cooling = part;
                break;
            case PartType.SoundCard:
                pc.SoundCard = part;
                break;
            case PartType.NetworkCard:
                pc.NetworkCard = part;
                break;
        }

        gameState.Inventory.Remove(part);
        return true;
    }

    public bool RemovePart(GameState gameState, Part part)
    {
        var pc = gameState.Workbench;
        
        switch (part.Type)
        {
            case PartType.Case:
                if (pc.Case == part) pc.Case = null;
                break;
            case PartType.Motherboard:
                if (pc.Motherboard == part) pc.Motherboard = null;
                break;
            case PartType.CPU:
                if (pc.CPU == part) pc.CPU = null;
                break;
            case PartType.RAM:
                pc.RAM.Remove(part);
                break;
            case PartType.GPU:
                if (pc.GPU == part) pc.GPU = null;
                break;
            case PartType.Storage:
                pc.Storage.Remove(part);
                break;
            case PartType.PSU:
                if (pc.PSU == part) pc.PSU = null;
                break;
            case PartType.Cooling:
                if (pc.Cooling == part) pc.Cooling = null;
                break;
            case PartType.SoundCard:
                if (pc.SoundCard == part) pc.SoundCard = null;
                break;
            case PartType.NetworkCard:
                if (pc.NetworkCard == part) pc.NetworkCard = null;
                break;
        }

        gameState.Inventory.Add(part);
        return true;
    }

    public bool TestBoot(PC pc)
    {
        // Basic boot requirements
        if (pc.Case == null) return false;
        if (pc.Motherboard == null) return false;
        if (pc.CPU == null) return false;
        if (pc.RAM.Count == 0) return false;
        if (pc.PSU == null) return false;

        // Check power
        if (pc.TotalPowerConsumption > (pc.PSU.PowerConsumption ?? 0))
            return false;

        return true;
    }

    public bool CompleteQuest(GameState gameState, Quest quest)
    {
        if (quest.IsCompleted) return false;

        quest.IsCompleted = true;
        gameState.Money += quest.Reward;
        gameState.Reputation += quest.ReputationReward;
        gameState.AvailableQuests.Remove(quest);
        gameState.CompletedQuests.Add(quest);

        return true;
    }
}
