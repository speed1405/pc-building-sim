# Silicon Legacy: PC Building Simulator

A text-based, cross-platform PC building simulation written in C# (.NET 8). Players navigate the history of personal computing through five distinct eras, completing grounded, real-life quests while managing compatibility, heat, and power.

## Features

- **Five Eras of PC History** (1990-2025): Experience the evolution of personal computing
- **Realistic Component System**: CPUs, GPUs, RAM, storage, motherboards, and more
- **Compatibility Engine**: Real socket types, slot compatibility, and power requirements
- **Quest System**: Tutorial missions, repairs, builds, and milestone challenges
- **Rich Console UI**: Built with Spectre.Console for an engaging terminal experience

## Requirements

- .NET 8.0 SDK or Runtime
- Terminal with ANSI color support (Windows Terminal, iTerm2, or modern Linux terminal)

## Building

```bash
# Clone the repository
git clone https://github.com/speed1405/pc-building-sim.git
cd pc-building-sim

# Build the solution
dotnet build

# Run the game
dotnet run --project src/SiliconLegacy.App
```

## Publishing

Build for your platform:

```bash
# Windows x64
dotnet publish src/SiliconLegacy.App -c Release -r win-x64 --self-contained

# Linux x64
dotnet publish src/SiliconLegacy.App -c Release -r linux-x64 --self-contained

# macOS ARM64 (Apple Silicon)
dotnet publish src/SiliconLegacy.App -c Release -r osx-arm64 --self-contained

# macOS x64 (Intel)
dotnet publish src/SiliconLegacy.App -c Release -r osx-x64 --self-contained
```

The compiled binary will be in `src/SiliconLegacy.App/bin/Release/net8.0/{platform}/publish/`.

## Project Structure

```
SiliconLegacy/
├── src/
│   ├── SiliconLegacy.Models/    # Data models (Part, PC, Quest, Era)
│   ├── SiliconLegacy.Engine/    # Game logic and compatibility rules
│   ├── SiliconLegacy.Data/      # JSON data loading and saving
│   ├── SiliconLegacy.UI/        # Spectre.Console user interface
│   └── SiliconLegacy.App/       # Main application entry point
├── data/
│   ├── db/                      # Parts database (JSON files by era)
│   └── quests/                  # Quest definitions
└── docs/                        # Documentation
```

## Game Eras

### Era I: The Jumper Era (1990-1995)
DOS, ISA Bus, 30-pin SIMMs, manual IRQ/DMA configuration

### Era II: The 3D Boom (1996-2001)
Windows 98, AGP, 3dfx Voodoo, Pentium II/III processors

### Era III: The GHz Race (2002-2010)
DDR2, SATA, PCIe, dual-core processors, heat management

### Era IV: The SSD Era (2011-2019)
Windows 7/10, NVMe, GTX 1080 Ti, modern gaming PCs

### Era V: The Modern Titan (2020-2025)
RTX 4090, DDR5, PCIe 5.0, AI workstations

## How to Play

1. **Shop for Parts**: Buy components from the era-appropriate catalog
2. **Build Your PC**: Install parts on the workbench
3. **Test Boot**: Verify your build works
4. **Complete Quests**: Earn money and reputation
5. **Unlock New Eras**: Progress through computing history

## Contributing

See [PLAN.md](PLAN.md) for the complete design document and roadmap.

Contributions are welcome! Please:
- Follow existing code style and conventions
- Add tests for new features
- Update documentation as needed

## License

See LICENSE file for details.

## Credits

Built with:
- [.NET 8](https://dotnet.microsoft.com/)
- [Spectre.Console](https://spectreconsole.net/)
