# Silicon Legacy: PC Building Simulator (1990-2025)
## Official Design & Development Plan

### 1. Project Vision
A text-based, cross-platform PC building simulation written in C# (.NET 8). Players navigate the history of personal computing through five distinct eras, completing grounded, real-life quests while managing compatibility, heat, and power.

---

### 2. The Five Finite Eras

| Era | Timeline | Focus | Key Milestone Quest |
| :--- | :--- | :--- | :--- |
| **I: The Jumper Era** | 1990-1995 | DOS, ISA Bus, 30-pin SIMMs | **The IRQ Conflict:** Resolve a Sound Blaster vs. Printer conflict. |
| **II: The 3D Boom** | 1996-2001 | Win 98, AGP, 3dfx Voodoo | **Y2K Audit:** Update BIOS for 50 PCs before midnight. |
| **III: The GHz Race** | 2002-2010 | DDR2, SATA, PCIe, Dual-Core | **The Prescott Heater:** Manage the 100W+ heat of a P4. |
| **IV: The SSD Era** | 2011-2019 | Win 7/10, NVMe, GTX 1080 Ti | **The Crysis Bench:** Build a rig that hits 60FPS in Crysis. |
| **V: The Modern Titan**| 2020-2025 | RTX 4090, DDR5, AI Workstations| **The 12VHPWR Check:** Ensure the GPU cable is seated. |

---

### 3. GUI Mockup (Console-Based)

Using `Spectre.Console` for a rich terminal experience.

```text
+-----------------------------------------------------------------------+
|  SILICON LEGACY [Year: 1998] | Bank: $1,240.50 | Rep: *****           |
+-----------------------------------------------------------------------+
|                                                                       |
|  [ CURRENT WORKBENCH ]                                                |
|  CASE: Beige AT Tower (Generic)                                       |
|  MOBO: ASUS P2B (Slot 1)                                              |
|  CPU : Intel Pentium II 350MHz [INSTALLED]                            |
|  RAM : 64MB SDRAM [SLOT 1]                                            |
|  GPU : NVIDIA RIVA TNT (AGP) [PENDING]                                |
|                                                                       |
|  > Actions:                                                           |
|    1. Install Part from Inventory                                     |
|    2. Configure Jumpers / BIOS                                        |
|    3. Connect Cables                                                  |
|    4. Test Boot                                                       |
|                                                                       |
+-----------------------------------------------------------------------+
|  LOG: "You successfully applied thermal paste. It's a bit messy."     |
+-----------------------------------------------------------------------+
```

---

### 4. Technical Architecture & Directory Structure

- **Core:** .NET 8.0 (Cross-platform support for Windows, Linux, macOS).
- **UI:** Spectre.Console.
- **Data:** JSON-based component database.

**Directory Structure:**
```text
SiliconLegacy/
├── src/
│   ├── Engine/       # Compatibility logic, Physics (Heat/Power)
│   ├── Models/       # Part, PC, Customer, Quest definitions
│   ├── UI/           # Console rendering and Menu systems
│   └── Data/         # JSON Serialization logic
├── data/
│   ├── db/           # Parts database (divided by Era)
│   └── quests/       # Story and mission scripts
└── docs/             # Technical manuals
```

---

### 5. Future DLC & Expansion Plan

The game is built with a modular "Plug-in" architecture for future content:

1. **The Server Room DLC:** Adds enterprise hardware (Xeon/EPYC), rack-mount cases, and networking quests (setting up local subnets).
2. **The "Sleeper" Pack:** Focuses on retro-modding—fitting 2025 hardware into 1990 cases with custom mounting brackets.
3. **The Boutique Brand Pack:** Licensed (or parody) real-world brands with high-fidelity ASCII renders of iconic parts (e.g., OG Alienware cases).

---

### 6. Cross-Platform Deployment
- **Windows:** `.exe` self-contained via `dotnet publish -r win-x64`.
- **Linux:** Native binary via `dotnet publish -r linux-x64`.
- **macOS:** Universal binary via `dotnet publish -r osx-arm64`.
