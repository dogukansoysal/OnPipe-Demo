# OnPipe — Hyper-Casual Prototype

A one-touch arcade prototype in Unity. A ring slides down a pipe: hold to squeeze it and burst the pieces around the pipe, release to let it open again.

> Built in May 2021 (commit history: 3 days). Archived as a finished prototype.

## Gameplay

- The ring moves along the pipe on its own.
- **Hold** to shrink the ring and burst the pieces stuck to the pipe.
- **Release** to let the ring grow back.
- The ring breaks if it hits an obstacle or is squeezed onto a part of the pipe that is too thick.
- Burst enough pieces before the finish line to clear the level. Coins are saved between sessions.

Inspired by the mechanic of the mobile game *OnPipe*. The code is my own, apart from the third-party plugins.

## Technical highlights

- **Size-aware squeeze:** `PlayerController` checks the pipe segment under the ring and uses its size as the limit for shrinking. Going under the limit breaks the ring.
- **Data-driven levels:** each level is a `ScriptableObject` with its required score. `StageManager` builds the stage from it, and `LevelManager` handles progression.
- **Small, single-purpose managers:** game state, input, coins, saving, sound, UI and stages are separate singletons (`Singleton<T>`), so each can be read and changed on its own.
- **Game feel and tooling:** DOTween handles animation, NiceVibrations adds haptics, and NaughtyAttributes adds inspector tooling.

## Project layout

```
Assets/GameSource/Scripts/
  Controllers/        PlayerController.cs
  Managers/           Game, Input, Level, Stage, Coin, Save, Sound, UI
  ScriptableObjects/  Level.cs
  Utilities/          Singleton, GameConstants, helpers
```

## Run it

Open the project in Unity **2020.2.3f1** and play the scene in `Assets/GameSource`. Portrait orientation.

## Tech

Unity 2020.2 · C# · DOTween · NiceVibrations · NaughtyAttributes · TextMesh Pro
