# Contributing to Silicon Legacy

Thank you for your interest in contributing to Silicon Legacy! This document provides guidelines for contributing to the project.

## Getting Started

1. Fork the repository
2. Clone your fork: `git clone https://github.com/YOUR-USERNAME/pc-building-sim.git`
3. Create a feature branch: `git checkout -b feature/your-feature-name`
4. Make your changes
5. Commit your changes: `git commit -am 'Add some feature'`
6. Push to the branch: `git push origin feature/your-feature-name`
7. Open a Pull Request

## Development Setup

### Prerequisites

- .NET 8.0 SDK
- A code editor (Visual Studio, VS Code, or JetBrains Rider recommended)
- Git

### Building the Project

```bash
dotnet restore
dotnet build
```

### Running the Game

```bash
dotnet run --project src/SiliconLegacy.App
```

## Code Style

- Follow standard C# coding conventions
- Use meaningful variable and method names
- Add comments for complex logic
- Keep methods focused and concise

## Adding New Content

### Adding Parts

Parts are stored in JSON files in the `data/db/` directory, organized by era:
- `parts_era1.json` - Era I: The Jumper Era (1990-1995)
- `parts_era2.json` - Era II: The 3D Boom (1996-2001)
- `parts_era3.json` - Era III: The GHz Race (2002-2010)
- `parts_era4.json` - Era IV: The SSD Era (2011-2019)
- `parts_era5.json` - Era V: The Modern Titan (2020-2025)

Each part should include:
- `Id` - Unique identifier (e.g., "era1-cpu-001")
- `Name` - Display name
- `Type` - Component type (0=Case, 1=Motherboard, 2=CPU, 3=RAM, etc.)
- `EraId` - Which era it belongs to (1-5)
- `Price` - Cost in dollars
- `Description` - Brief description
- Relevant technical properties (SocketType, PowerConsumption, etc.)

### Adding Quests

Quests are stored in JSON files in the `data/quests/` directory. Each quest includes:
- `Id` - Unique identifier
- `Name` - Quest name
- `Description` - What the player needs to do
- `EraId` - Which era it belongs to
- `Reward` - Money earned
- `ReputationReward` - Reputation points earned
- `Type` - Quest type (0=Tutorial, 1=Repair, 2=Build, etc.)
- `Requirements` - List of requirements to complete the quest

## Testing

Currently, the project uses manual testing. We welcome contributions to add automated tests!

## Pull Request Guidelines

- Keep PRs focused on a single feature or bug fix
- Update documentation if needed
- Test your changes thoroughly
- Reference any related issues in your PR description

## Reporting Bugs

Please open an issue on GitHub with:
- A clear title and description
- Steps to reproduce the bug
- Expected vs actual behavior
- Your environment (OS, .NET version)

## Feature Requests

We welcome feature requests! Please open an issue with:
- A clear description of the feature
- Why it would be useful
- Any implementation ideas you have

## Questions?

Feel free to open an issue with your question, or reach out to the maintainers.

Thank you for contributing!
