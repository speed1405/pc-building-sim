using SiliconLegacy.Models;

namespace SiliconLegacy.Engine;

public class CompatibilityEngine
{
    public List<string> CheckCompatibility(PC pc, Part newPart)
    {
        var issues = new List<string>();

        switch (newPart.Type)
        {
            case PartType.CPU:
                issues.AddRange(CheckCPUCompatibility(pc, newPart));
                break;
            case PartType.RAM:
                issues.AddRange(CheckRAMCompatibility(pc, newPart));
                break;
            case PartType.GPU:
                issues.AddRange(CheckGPUCompatibility(pc, newPart));
                break;
            case PartType.Storage:
                issues.AddRange(CheckStorageCompatibility(pc, newPart));
                break;
            case PartType.PSU:
                issues.AddRange(CheckPSUCompatibility(pc, newPart));
                break;
        }

        return issues;
    }

    private List<string> CheckCPUCompatibility(PC pc, Part cpu)
    {
        var issues = new List<string>();
        
        if (pc.Motherboard == null)
        {
            issues.Add("No motherboard installed. CPU requires a compatible motherboard.");
            return issues;
        }

        if (!string.IsNullOrEmpty(cpu.SocketType) && 
            !string.IsNullOrEmpty(pc.Motherboard.SocketType) &&
            cpu.SocketType != pc.Motherboard.SocketType)
        {
            issues.Add($"CPU socket ({cpu.SocketType}) incompatible with motherboard socket ({pc.Motherboard.SocketType}).");
        }

        return issues;
    }

    private List<string> CheckRAMCompatibility(PC pc, Part ram)
    {
        var issues = new List<string>();
        
        if (pc.Motherboard == null)
        {
            issues.Add("No motherboard installed. RAM requires a compatible motherboard.");
            return issues;
        }

        if (!string.IsNullOrEmpty(ram.SlotType) &&
            pc.Motherboard.Compatibility.Count > 0 &&
            !pc.Motherboard.Compatibility.Contains(ram.SlotType))
        {
            issues.Add($"RAM type ({ram.SlotType}) incompatible with motherboard.");
        }

        return issues;
    }

    private List<string> CheckGPUCompatibility(PC pc, Part gpu)
    {
        var issues = new List<string>();
        
        if (pc.Motherboard == null)
        {
            issues.Add("No motherboard installed. GPU requires a compatible motherboard.");
            return issues;
        }

        if (!string.IsNullOrEmpty(gpu.SlotType) &&
            pc.Motherboard.Compatibility.Count > 0 &&
            !pc.Motherboard.Compatibility.Contains(gpu.SlotType))
        {
            issues.Add($"GPU slot type ({gpu.SlotType}) incompatible with motherboard.");
        }

        return issues;
    }

    private List<string> CheckStorageCompatibility(PC pc, Part storage)
    {
        var issues = new List<string>();
        
        if (pc.Motherboard == null)
        {
            issues.Add("No motherboard installed. Storage requires a compatible motherboard.");
            return issues;
        }

        return issues;
    }

    private List<string> CheckPSUCompatibility(PC pc, Part psu)
    {
        var issues = new List<string>();
        
        if (psu.PowerConsumption.HasValue)
        {
            var totalNeeded = pc.TotalPowerConsumption;
            if (totalNeeded > psu.PowerConsumption.Value)
            {
                issues.Add($"PSU wattage ({psu.PowerConsumption}W) insufficient for total system power draw ({totalNeeded}W).");
            }
        }

        return issues;
    }

    public bool CanInstallPart(PC pc, Part part)
    {
        var issues = CheckCompatibility(pc, part);
        return issues.Count == 0;
    }
}
