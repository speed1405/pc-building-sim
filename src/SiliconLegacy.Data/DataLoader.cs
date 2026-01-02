using System.Text.Json;
using SiliconLegacy.Models;

namespace SiliconLegacy.Data;

public class DataLoader
{
    private readonly string _dataPath;

    public DataLoader(string dataPath)
    {
        _dataPath = dataPath;
    }

    public List<Era> LoadEras()
    {
        var eraFile = Path.Combine(_dataPath, "db", "eras.json");
        
        if (!File.Exists(eraFile))
        {
            return GetDefaultEras();
        }

        try
        {
            var json = File.ReadAllText(eraFile);
            return JsonSerializer.Deserialize<List<Era>>(json) ?? GetDefaultEras();
        }
        catch
        {
            return GetDefaultEras();
        }
    }

    public List<Part> LoadParts()
    {
        var parts = new List<Part>();
        var dbPath = Path.Combine(_dataPath, "db");
        
        if (!Directory.Exists(dbPath))
        {
            return parts;
        }

        foreach (var file in Directory.GetFiles(dbPath, "parts_era*.json"))
        {
            try
            {
                var json = File.ReadAllText(file);
                var eraParts = JsonSerializer.Deserialize<List<Part>>(json);
                if (eraParts != null)
                {
                    parts.AddRange(eraParts);
                }
            }
            catch
            {
                // Skip files that can't be loaded
            }
        }

        return parts;
    }

    public List<Quest> LoadQuests()
    {
        var quests = new List<Quest>();
        var questsPath = Path.Combine(_dataPath, "quests");
        
        if (!Directory.Exists(questsPath))
        {
            return quests;
        }

        foreach (var file in Directory.GetFiles(questsPath, "*.json"))
        {
            try
            {
                var json = File.ReadAllText(file);
                var questList = JsonSerializer.Deserialize<List<Quest>>(json);
                if (questList != null)
                {
                    quests.AddRange(questList);
                }
            }
            catch
            {
                // Skip files that can't be loaded
            }
        }

        return quests;
    }

    private List<Era> GetDefaultEras()
    {
        return new List<Era>
        {
            new Era
            {
                Id = 1,
                Name = "Era I: The Jumper Era",
                StartYear = 1990,
                EndYear = 1995,
                Description = "DOS, ISA Bus, 30-pin SIMMs",
                Focus = "Manual IRQ/DMA configuration, ISA expansion cards",
                KeyMilestone = "The IRQ Conflict: Resolve a Sound Blaster vs. Printer conflict",
                IsUnlocked = true
            },
            new Era
            {
                Id = 2,
                Name = "Era II: The 3D Boom",
                StartYear = 1996,
                EndYear = 2001,
                Description = "Windows 98, AGP, 3dfx Voodoo",
                Focus = "AGP graphics, Pentium II/III processors",
                KeyMilestone = "Y2K Audit: Update BIOS for 50 PCs before midnight",
                IsUnlocked = false
            },
            new Era
            {
                Id = 3,
                Name = "Era III: The GHz Race",
                StartYear = 2002,
                EndYear = 2010,
                Description = "DDR2, SATA, PCIe, Dual-Core",
                Focus = "Heat management, SATA storage, PCIe slots",
                KeyMilestone = "The Prescott Heater: Manage the 100W+ heat of a P4",
                IsUnlocked = false
            },
            new Era
            {
                Id = 4,
                Name = "Era IV: The SSD Era",
                StartYear = 2011,
                EndYear = 2019,
                Description = "Windows 7/10, NVMe, GTX 1080 Ti",
                Focus = "NVMe storage, high-performance gaming",
                KeyMilestone = "The Crysis Bench: Build a rig that hits 60FPS in Crysis",
                IsUnlocked = false
            },
            new Era
            {
                Id = 5,
                Name = "Era V: The Modern Titan",
                StartYear = 2020,
                EndYear = 2025,
                Description = "RTX 4090, DDR5, AI Workstations",
                Focus = "Ray tracing, DDR5, PCIe 5.0",
                KeyMilestone = "The 12VHPWR Check: Ensure the GPU cable is seated",
                IsUnlocked = false
            }
        };
    }

    public void SaveGameState(GameState gameState, string savePath)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(gameState, options);
        File.WriteAllText(savePath, json);
    }

    public GameState? LoadGameState(string savePath)
    {
        if (!File.Exists(savePath))
        {
            return null;
        }

        try
        {
            var json = File.ReadAllText(savePath);
            return JsonSerializer.Deserialize<GameState>(json);
        }
        catch
        {
            return null;
        }
    }
}
