# Expelled from Paradise (exl)

A 2D game project built with **Unity 5.1.3f1** (legacy project, binary asset serialization).

[README на русском](README.ru.md)

## Overview

`exl` is a 2D action/adventure game prototype: tile-based levels, custom character
sprites, AI enemies, spell/FX effects and sound. The repository contains the full
Unity project, including third-party plugins.

## Project structure

```
Assets/
├── arts/          # Art assets
├── source/        # Game code and content, split by domain:
│   ├── AI/        # Enemy AI
│   ├── Anim/      # Animation assets/logic
│   ├── Common/    # Shared code and prefabs
│   ├── Control/   # Player/input control
│   ├── Editor/    # Editor scripts
│   ├── FX/        # Visual effects
│   ├── Prop/      # Props/objects
│   ├── Sound/     # Audio
│   └── UI/        # User interface
├── scenes/        # Unity scenes (testbed, CellCopy, CellCopy2)
├── prefab/        # Prefabs
├── Resources/
├── sound/         # Audio assets
├── TileBrushes/   # Tile brushes for level building
└── TextMesh Pro/
```

### Third-party plugins (inside Assets/)

| Plugin | Purpose |
|---|---|
| AstarPathfindingProject | A* pathfinding for AI |
| PlayMaker | Visual scripting (FSM) |
| iTween | Animation/tweening library |
| Rotorz | Tile system |
| Haste | Asset search |
| QHierarchy | Hierarchy improvements |
| StompyRobot | SRDebugger tools |

## Requirements

- Unity **5.1.3f1** (the project uses binary serialization from this era and is
  not guaranteed to open in newer Unity versions without migration)

## Getting started

1. Clone the repository:
   ```bash
   git clone https://github.com/Methelina/Expelled_from_Paradise.git
   ```
2. Open the project folder in Unity 5.1.3f1.
3. Open a scene from `Assets/scenes/` and press Play.

## Notes

- The project was originally versioned with Mercurial (`.hg`); this repository is
  its git mirror. `Library/`, `Temp/` and other generated folders are ignored —
  see [.gitignore](.gitignore).
- `*.meta` files are tracked and required: Unity matches assets to their
  import settings by GUID, so never delete or edit them manually.
