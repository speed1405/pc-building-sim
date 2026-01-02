# Silicon Legacy: PC Building Simulator (1990-2025)
## Official Design & Development Plan

### 1. Project Vision
A text-based, cross-platform PC building simulation written in C# (.NET 8). Players navigate the history of personal computing through five distinct eras, completing grounded, real-life quests while managing compatibility, heat, and power.

- Core loop: tutorial → small repair jobs → era unlocks → complex builds → boutique/custom requests.
- Player personas: retro hobbyist, budget builder, performance tuner; each supported via quests and parts availability.
- Success metrics: session length targets, retention goals, database coverage targets.

---

### 2. The Five Finite Eras

| Era | Timeline | Focus | Key Milestone Quest |
| :--- | :--- | :--- | :--- |
| **I: The Jumper Era** | 1990-1995 | DOS, ISA Bus, 30-pin SIMMs | **The IRQ Conflict:** Resolve a Sound Blaster vs. Printer conflict. |
| **II: The 3D Boom** | 1996-2001 | Win 98, AGP, 3dfx Voodoo | **Y2K Audit:** Update BIOS for 50 PCs before midnight. |
| **III: The GHz Race** | 2002-2010 | DDR2, SATA, PCIe, Dual-Core | **The Prescott Heater:** Manage the 100W+ heat of a P4. |
| **IV: The SSD Era** | 2011-2019 | Win 7/10, NVMe, GTX 1080 Ti | **The Crysis Bench:** Build a rig that hits 60FPS in Crysis. |
| **V: The Modern Titan**| 2020-2025 | RTX 4090, DDR5, AI Workstations| **The 12VHPWR Check:** Ensure the GPU cable is seated. |

#### Era Reference Hardware
- **Era I (1990-1995):** ISA/AT motherboards, 486/early Pentium, 30-pin SIMMs, ISA sound/video cards, AT PSU.
- **Era II (1996-2001):** AGP/ATX motherboards, Pentium II/III, SDRAM, 3dfx Voodoo/RIVA TNT, ATX PSU.
- **Era III (2002-2010):** PCIe/SATA motherboards, Pentium 4/Athlon 64/Core 2, DDR2, GeForce 6/7/8 series, ATX12V PSU.
- **Era IV (2011-2019):** NVMe/SATA motherboards, Core i5/i7, DDR3/DDR4, GTX 900/1000 series, modular ATX12V PSU.
- **Era V (2020-2025):** ATX 3.0/PCIe 5.0 motherboards, Ryzen 5000/Core 12th-14th gen, DDR5, RTX 4000 series, 12VHPWR ATX 3.0 PSU.

#### Signature Mechanics
- **Era I:** ISA IRQ/DMA conflicts (manual jumper settings), AT keyboard connectors, turbo buttons.
- **Era II:** AGP voltage compatibility (1.5V vs. 3.3V), slot/socket transitions (Socket 370, Slot 1), Y2K BIOS issues.
- **Era III:** VRM/heat constraints (Prescott thermal limits), SATA/IDE coexistence, early PCIe power connectors.
- **Era IV:** NVMe M.2 slot compatibility, multi-GPU SLI/CrossFire, cable management challenges.
- **Era V:** 12VHPWR strain relief and transient load spikes, DDR5 stability tuning, PCIe 5.0 signal integrity.

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

#### Accessibility & Diagnostics
- **Diagnostics Panel:** Real-time display of CPU/GPU temperatures, voltage rails, fan RPM, and system stability indicators.
- **Tooltips & Inline Validation:** Context-sensitive help for part compatibility, error messages with actionable suggestions.
- **Undo/Rollback:** Ability to revert risky configuration changes (BIOS settings, overclocking) to last stable state.

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

- **Architecture Layering:** Domain (models/rules), Application (services/use cases), Infrastructure (IO/storage), UI; enforce boundaries via DI (Microsoft.Extensions.DependencyInjection).
- **JSON Schemas:** Formal schemas for parts, quests, DLC manifests; runtime validation with clear errors and migration strategies.
- **Observability:** Structured logging (Serilog) and optional opt-in telemetry for diagnostics and performance monitoring.

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

#### Release Strategy & Distribution Channels

**Primary Distribution:**
- **Steam:** Cross-platform releases via Steamworks SDK; Steam Cloud for save synchronization; achievements and community features.
- **Itch.io:** DRM-free builds for all platforms; flexible pricing and bundles; developer-friendly revenue split.
- **GOG (Good Old Games):** DRM-free emphasis aligns with retro computing theme; GOG Galaxy optional integration.
- **GitHub Releases:** Open-source community builds; pre-release and beta channels; automated nightly builds from CI.

**Secondary/Alternative:**
- **Microsoft Store:** Windows-specific distribution; UWP or desktop bridge packaging.
- **Homebrew (macOS):** Formula for command-line installation (`brew install siliconlegacy`).
- **APT/Snap/Flatpak (Linux):** Package for major distributions; leverage cross-distro formats (AppImage, Snap, Flatpak).

#### Platform-Specific Packaging

**Windows:**
- **Installer:** NSIS or WiX-based MSI installer with optional desktop/start menu shortcuts.
- **Portable:** Self-contained `.exe` with embedded .NET runtime (no installation required).
- **Store:** MSIX package for Microsoft Store submission.
- **Code Signing:** Authenticode certificate to avoid SmartScreen warnings.
- **Compatibility:** Target Windows 10+ (x64); test on Windows 11 with Dark Mode and HDR.

**Linux:**
- **AppImage:** Single-file executable; universal compatibility across distributions.
- **Snap:** Confined sandbox with auto-updates via snapd.
- **Flatpak:** Sandboxed distribution via Flathub; declarative permissions for filesystem access.
- **Deb/RPM:** Native packages for Debian/Ubuntu and Fedora/RHEL families.
- **Portable Tarball:** `.tar.gz` with self-contained runtime for manual installation.
- **Compatibility:** Test on Ubuntu LTS, Fedora, Arch, and SteamOS (Steam Deck).

**macOS:**
- **DMG Installer:** Drag-and-drop `.app` bundle with background image and license agreement.
- **Homebrew Cask:** Automated installation via `brew install --cask siliconlegacy`.
- **Universal Binary:** Combine x64 and ARM64 slices for Intel and Apple Silicon Macs.
- **Notarization:** Apple Developer ID signing and notarization to avoid Gatekeeper blocks.
- **Compatibility:** Target macOS 11+ (Big Sur); test on Intel and M1/M2/M3 chips.

#### Auto-Update Mechanism

- **Update Service:** RESTful API serving version manifest (current version, changelog, download URLs per platform).
- **In-Game Updater:** Check for updates on launch (configurable: always, daily, weekly, manual).
- **Delta Patching:** Download only changed files when possible; full installer fallback.
- **Rollback Support:** Keep previous version for quick revert if update fails.
- **Notification:** Non-intrusive banner with "Update Available" prompt; defer or install options.
- **Silent Updates (Optional):** Background download with install on next launch (opt-in).

#### Platform-Specific Considerations

**File Paths:**
- Windows: `%AppData%/SiliconLegacy` (config/saves), `%ProgramFiles%/SiliconLegacy` (binaries).
- Linux: `~/.config/siliconlegacy` (config), `~/.local/share/siliconlegacy` (saves/data).
- macOS: `~/Library/Application Support/SiliconLegacy` (config/saves), `/Applications/SiliconLegacy.app` (bundle).

**Permissions:**
- Linux: Ensure executable bit set (`chmod +x`); avoid requiring root for installation.
- macOS: Request Full Disk Access only if needed (e.g., DLC in restricted folders); prefer user-accessible paths.
- Windows: Installer requires admin for Program Files; portable version runs in user context.

**Terminal/Console:**
- Windows: Detect and enable VT100 sequences for color/formatting; fallback to legacy console if unavailable.
- Linux: Native VT100/ANSI support; respect `$TERM` and `$COLORTERM` environment variables.
- macOS: Native Terminal.app and iTerm2 support; test with default and Solarized themes.

#### Release Pipeline (CI/CD)

- **Build Matrix:** GitHub Actions jobs for `win-x64`, `linux-x64`, `osx-arm64`, and `osx-x64`.
- **Artifacts:** Generate platform-specific binaries, installers, and portable archives.
- **Checksums:** SHA-256 hashes for all release artifacts; published alongside binaries.
- **Automated Testing:** Run unit/integration tests on each platform before artifact creation.
- **Release Tagging:** Semantic versioning (`v1.2.3`); auto-generate release notes from commit history.
- **Steam Deployment:** Automated SteamPipe upload via `steamcmd` for stable releases.
- **Notification:** Webhook to Discord/Slack on successful release; update website and social media.

#### Beta & Early Access

- **Beta Branches:** Separate Steam/Itch.io beta branches for testing new features.
- **Early Access Program:** Opt-in via game settings; receive pre-release builds with known issues disclaimer.
- **Feedback Loop:** In-game "Report Issue" button linking to GitHub Issues or dedicated feedback form.
- **Rollback:** Easy switch between stable and beta channels without losing save data.

---

### 7. Core Mechanics Enhancements
- **Deep Compatibility Engine**: Advanced logic for physical clearance (GPU length/height), chipset/socket dependencies, and power rail distribution.
- **Interactive OS & BIOS Simulation**: A dedicated sub-terminal experience for driver installation, HDD partitioning, and manual BIOS overclocking.
- **Advanced Thermal & Stability Model**: Real-time heat calculation based on case airflow, ambient temperature, and component TDP, affecting part longevity.
- **Dynamic Retro Marketplace**: A simulated auction house for sourcing "untested" or "rare" parts from previous eras, with prices shifting based on rarity.
- **Maintenance & Customization**: Deep interaction tools for cleaning dust, re-padding GPUs, and custom case modifications (painting/window cutting).

---

### 8. Integrated DLC Management System

- **DLC Manifest Service:**
  - A remote JSON manifest served over HTTPS listing available DLC packs with fields: `id`, `name`, `version`, `description`, `size`, `checksum` (SHA-256), `signature`, `download_url`, `dependencies`, `minimum_game_version`, `release_notes`.
  - Caching of the manifest locally with TTL to support offline browsing.

- **In-Game Content Manager (UI):**
  - A Spectre.Console-driven “Content” menu with tabs: Available, Installed, Updates, Settings.
  - For each DLC: detail view (features, quests, parts), version, size, changelog, disk space required, compatibility warnings.
  - Actions: Download, Install, Update, Disable, Uninstall.

- **Download & Verification Pipeline:**
  - Asynchronous downloader with resumable transfers and progress bar (percent, speed, ETA).
  - Integrity verification via SHA-256; optional signature verification (Ed25519) for authenticity.
  - Clear error handling and retry policy; informative messages for network and verification failures.

- **Packaging & Installation:**
  - DLC packages distributed as zip archives containing:
    - `content/parts/*.json`
    - `content/quests/*.json`
    - `content/assets/*` (ASCII art, icons)
    - `meta/manifest.json`
    - `meta/license.txt`
  - Install to sandboxed game path:
    - Windows: `%AppData%/SiliconLegacy/dlc/{dlc-id}`
    - Linux: `~/.local/share/SiliconLegacy/dlc/{dlc-id}`
    - macOS: `~/Library/Application Support/SiliconLegacy/dlc/{dlc-id}`
  - Maintain an `installed.json` registry with status, version, install date, and content indexes.

- **Hot-Loading & Integration:**
  - Live reload of part and quest databases: merge DLC JSON with base datasets using safe schema validation.
  - Conflict resolution via namespaced IDs (e.g., `dlc.serverroom.part.cpu.epyc-7742`).
  - Dependency checks: prevent enabling DLC with unmet dependencies or incompatible game versions.

- **Updates & Uninstall:**
  - Periodic check for updates (manual or auto) with delta-aware downloads when available.
  - Uninstall routine removes DLC files and registry entries while preserving save-game references gracefully (disable content rather than hard-delete references).

- **Mod/DLC Safety & Governance:**
  - Strict schema validation for all DLC JSON with clear error messages pointing to file and path.
  - Sandbox isolation: DLC cannot overwrite core engine files; only additive content allowed.
  - Optional “Safe Mode” startup that disables third-party DLC if validation fails.

- **Offline & Enterprise Scenarios:**
  - Support “side-load” DLC from local file paths for air-gapped environments.
  - Command-line flags for administrators to preinstall DLC across multiple machines.

- **Telemetry (Optional):**
  - Anonymous opt-in metrics for install successes/failures and download performance to improve the pipeline.

- **Developer Tooling:**
  - A CLI helper `siliconlegacy dlc pack` to package and validate DLC archives (checksum/signature generation, schema validation).
  - Unit tests covering manifest parsing, checksum verification, conflict resolution, and hot-load behavior.

#### Security & Reliability Enhancements
- **Certificate Pinning:** Pin trusted certificate for manifest server to prevent MITM attacks.
- **Signed Packages:** Use Ed25519 signatures for package authenticity; reject unsigned or tampered packages.
- **Path Traversal Protection:** Strict validation of all file paths within DLC packages to prevent directory escape attacks.
- **Resumable Downloads:** Support partial-file verification and resume capability for interrupted downloads.
- **Transactional Install:** Atomic activation with automatic rollback on installation failure; maintain previous working state.

---

### 9. Save System & Migration
- Save format: JSON with `schemaVersion` and `contentVersion` (support compression for large inventories).
- Backups: automatic timestamped backups before major updates; configurable retention.
- Migration: forward-compatible loaders with explicit migration steps on version bumps; detailed error reporting and safe rollback.
- Conflict resolution: handle missing or disabled DLC content gracefully (disable references rather than hard-delete).

---

### 10. Testing & CI
- Unit tests for compatibility engine, thermal model, power rails, driver/boot flow, and DLC loader.
- Property-based tests for JSON parsers and configuration validators (invalid ranges, edge cases).
- Fuzz tests for JSON ingestion and hot-loading.
- CI: GitHub Actions matrix on Windows, Linux, macOS; code coverage reporting; build artifacts for each platform.
- Static analysis: Roslyn analyzers; optional security scanning for DLC packages.

---

### 11. Accessibility & Localization
- Accessibility: high-contrast theme, colorblind-safe palette, font-size scaling, keyboard-only navigation with mnemonics.
- Localization: externalized strings (resource files), language packs, and RTL considerations where needed.
- Internationalization testing: automated checks to prevent layout breakage.

---

### 12. Performance & Profiling
- Simulation tick budget targets; profiling hooks for compatibility/thermal loops.
- Performance targets: typical build time-to-feedback (<100ms for UI actions); memory footprint limits.
- Tools: integration with .NET performance counters; optional diagnostics mode to log hotspots.

---

### 13. Documentation & Community
- Contributing guide, Code of Conduct, issue/PR templates.
- Docs site generated from `/docs` (Markdown), including API schemas for parts/quests/DLC.
- Release notes and upgrade guides, including DLC compatibility notes.

---

### 14. Risk & QA Plan
- Risks: data integrity on hot-load, DLC security, platform-specific file path/permissions.
- Mitigations: transactional installs, signature verification, schema validation, sandboxed paths.
- Test matrices per OS and per Era; explicit pass/fail criteria for major milestones.

---

### 15. In-Game Benchmarking System

#### Overview
A comprehensive benchmarking system that simulates real-world performance testing, allowing players to validate their builds, diagnose bottlenecks, and earn reputation points by achieving target scores across different eras.

#### Benchmark Categories

**Synthetic Benchmarks (Era-Appropriate):**
- **Era I (1990-1995):** Norton SysInfo, CheckIt, Landmark Speed Test; focus on CPU speed, RAM latency, disk access time.
- **Era II (1996-2001):** 3DMark 99/2000, Quake III Arena timedemo; measure 3D rendering, texture fill rate, polygon throughput.
- **Era III (2002-2010):** 3DMark 03/06, PCMark Vantage; CPU multi-threading, shader performance, HDD sequential/random I/O.
- **Era IV (2011-2019):** 3DMark Fire Strike, Cinebench R15/R20; DX11 rendering, CPU single/multi-core, NVMe throughput.
- **Era V (2020-2025):** 3DMark Time Spy Extreme, Cinebench R23, Corona Renderer; ray tracing, AI workloads, PCIe 4.0/5.0 bandwidth.

**Application Benchmarks (Real-World):**
- **Gaming:** Simulated FPS tests in era-defining titles (Doom, Crysis, Cyberpunk 2077); resolution scaling tests.
- **Productivity:** Encoding (H.264/H.265), compilation time (Linux kernel build), compression (7-Zip), rendering (Blender).
- **Professional:** CAD viewport performance, video editing scrubbing, AI inference (Stable Diffusion), database queries.

**Stability & Stress Tests:**
- **Prime95 (CPU):** Small FFT, large FFT, blend tests; detect thermal throttling, crashes, or instability.
- **MemTest86 (RAM):** Multi-pass memory testing; catch bad DIMMs or unstable overclocks.
- **FurMark (GPU):** Power virus-style stress; test PSU capacity, GPU thermals, and VRM stability.
- **CrystalDiskMark (Storage):** Sequential/random read/write; test SATA/NVMe performance and controller stability.

#### Benchmark Execution Flow

1. **Pre-Check:** System validates all components are installed, drivers loaded, and no compatibility errors exist.
2. **Configuration:** Player selects benchmark type, resolution/settings (for graphics tests), and duration (quick/standard/extended).
3. **Warmup Phase:** 5-10 second warmup to stabilize thermals and clocks; display real-time temps and fan speeds.
4. **Test Run:** Execute benchmark with live progress bar, FPS counter (if applicable), and system vitals (CPU/GPU load, temps, power draw).
5. **Results Display:** Final score, percentile ranking (vs. similar builds), detailed breakdown (CPU score, GPU score, memory score, etc.).
6. **Diagnostics:** Flag bottlenecks (e.g., "GPU limited at 99% usage," "CPU thermal throttling detected," "RAM running below rated speed").

#### Scoring System

**Score Calculation:**
- Base score from component raw performance (clock speed, core count, VRAM, etc.).
- Multipliers for proper configuration (XMP enabled, optimal airflow, latest drivers).
- Penalties for thermal throttling, stability issues, or mismatched components (e.g., slow RAM on Ryzen).

**Percentile Rankings:**
- Compare player's score against database of historical builds from the same era.
- Display rank: "Top 5% of Era III builds," "Above average for 2005 hardware," etc.
- Leaderboards per benchmark per era (global and friends-only).

**Era-Specific Targets:**
- Quests may require achieving specific scores: "Build a PC that scores 5000+ in 3DMark 2000" or "Achieve 60 FPS in Crysis at 1080p High."
- Unlock bonuses for exceeding targets (reputation boost, rare part unlock, achievement).

#### Detailed Results & Analysis

**Performance Breakdown:**
- Component contribution chart: pie chart showing CPU (35%), GPU (50%), RAM (10%), Storage (5%) impact on overall score.
- Frame time graph: 1% low, 0.1% low, average, max FPS for gaming benchmarks.
- Thermal timeline: graph of CPU/GPU temps over benchmark duration; highlight throttling events.
- Power consumption: peak/average wattage; compare to PSU capacity; warn if approaching limits.

**Bottleneck Detection:**
- CPU-bound: "GPU utilization <80%, CPU at 100% on multiple cores. Consider faster CPU or lower settings."
- GPU-bound: "GPU at 99%, CPU idle. Normal for this workload. Upgrade GPU for higher FPS."
- Memory-bound: "High memory latency detected. Enable XMP or upgrade to faster RAM."
- Thermal-bound: "Thermal throttling reduced performance by 15%. Improve cooling or reduce voltage."
- Power-bound: "PSU struggled under load (voltage droop detected). Consider higher wattage unit."

**Stability Report:**
- Pass/Fail for stress tests with detailed error log if failed.
- Crash dumps with error codes (e.g., "BSOD: MEMORY_MANAGEMENT," "GPU driver timeout," "CPU WHEA error").
- Suggestions: "RAM instability detected. Try increasing DRAM voltage or loosening timings."

#### Historical Comparisons

**Era Leaderboards:**
- Per-benchmark, per-era rankings: "Top 486 DX4-100 overclock," "Fastest Pentium 4 Prescott build," "Best RTX 4090 score."
- Filter by component: "All builds with Voodoo 2 SLI," "Ryzen 9 5950X in Era V."
- Historical context: "This score would rank #1 in Era III but is mid-tier in Era IV."

**Personal Records:**
- Track player's best scores per benchmark per era.
- "Personal Best" overlay on results screen.
- Progress tracking: "You improved 23% since your last Era II build."

**Community Sharing:**
- Export benchmark results as shareable text summary or ASCII art graph.
- Post to in-game community board or export to clipboard for forum posting.
- Challenge friends: "Can you beat my 3DMark 2000 score with Era II hardware?"

#### Quest Integration

**Benchmark Quests:**
- "Prove yourself: Score 3500+ in 3DMark 99 to unlock advanced Era II parts."
- "The Crysis Challenge: Build a rig that maintains 60 FPS in Crysis at 1080p High settings."
- "Stress Test Survival: Pass a 1-hour Prime95 blend test without throttling."
- "Overclocking Master: Achieve a 20% performance boost via BIOS tuning and pass stability tests."

**Customer Requests:**
- "Client needs a video editing rig that scores 8000+ in Cinebench R20."
- "Budget gamer wants 60 FPS in CS:GO at 1080p; prove it before delivery."
- "Enthusiast demands top 10% ranking in 3DMark Time Spy for their build."

#### Realism & Simulation Accuracy

**Physics-Based Modeling:**
- Thermal dynamics: simulate heat dissipation based on cooler TDP rating, case airflow, ambient temp.
- Power delivery: model VRM efficiency, 12V rail stability, transient load response.
- Component degradation: long-term overclocking or poor cooling affects benchmark scores over in-game time.

**Driver & BIOS Impact:**
- Outdated drivers may reduce score by 5-15%; simulate "day-one" driver bugs for new GPUs.
- BIOS settings: XMP/DOCP, CPU turbo boost, PBO (Precision Boost Overdrive) affect results.
- Wrong settings: slow RAM timings, disabled turbo, or mismatched BIOS options penalize score.

**Variability & RNG:**
- Silicon lottery: identical CPUs/GPUs may score ±3% due to binning variance.
- Thermal paste application: poor application (too much/too little) increases temps, reducing score.
- Component age: used parts may have slight degradation, affecting stability and peak performance.

#### Educational Value

**Tooltips & Insights:**
- After each benchmark, display educational notes: "Your Pentium 4 throttled due to inadequate cooling. Prescott CPUs are notorious for high TDP."
- Historical context: "In 2001, this score would place you in the top 1% of enthusiasts."
- Upgrade suggestions: "Upgrading to 512MB RAM would improve your score by ~18% in this workload."

**Benchmark Encyclopedia:**
- In-game reference: detailed descriptions of each benchmark, what it tests, and historical significance.
- "3DMark 2000 introduced pixel shaders and became the de facto standard for GPU testing in the early 2000s."

---