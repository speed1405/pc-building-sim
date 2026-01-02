namespace SiliconLegacy.Models;

public class PC
{
    public string Name { get; set; } = "Workbench";
    public Part? Case { get; set; }
    public Part? Motherboard { get; set; }
    public Part? CPU { get; set; }
    public List<Part> RAM { get; set; } = new();
    public Part? GPU { get; set; }
    public List<Part> Storage { get; set; } = new();
    public Part? PSU { get; set; }
    public Part? Cooling { get; set; }
    public Part? SoundCard { get; set; }
    public Part? NetworkCard { get; set; }
    
    public int TotalPowerConsumption => CalculatePowerConsumption();
    public int TotalTDP => CalculateTDP();
    
    private int CalculatePowerConsumption()
    {
        var total = 0;
        if (CPU?.PowerConsumption.HasValue == true) total += CPU.PowerConsumption.Value;
        if (GPU?.PowerConsumption.HasValue == true) total += GPU.PowerConsumption.Value;
        if (Motherboard?.PowerConsumption.HasValue == true) total += Motherboard.PowerConsumption.Value;
        foreach (var ram in RAM)
            if (ram?.PowerConsumption.HasValue == true) total += ram.PowerConsumption.Value;
        foreach (var storage in Storage)
            if (storage?.PowerConsumption.HasValue == true) total += storage.PowerConsumption.Value;
        return total;
    }
    
    private int CalculateTDP()
    {
        var total = 0;
        if (CPU?.TDP.HasValue == true) total += CPU.TDP.Value;
        if (GPU?.TDP.HasValue == true) total += GPU.TDP.Value;
        return total;
    }
    
    public List<Part> GetAllParts()
    {
        var parts = new List<Part>();
        if (Case != null) parts.Add(Case);
        if (Motherboard != null) parts.Add(Motherboard);
        if (CPU != null) parts.Add(CPU);
        parts.AddRange(RAM);
        if (GPU != null) parts.Add(GPU);
        parts.AddRange(Storage);
        if (PSU != null) parts.Add(PSU);
        if (Cooling != null) parts.Add(Cooling);
        if (SoundCard != null) parts.Add(SoundCard);
        if (NetworkCard != null) parts.Add(NetworkCard);
        return parts;
    }
}
