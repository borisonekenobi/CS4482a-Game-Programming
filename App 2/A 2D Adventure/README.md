# A 2D Adventure

This project is a Unity 2D top-down adventure game. Explore each level, collect the items needed to unlock doors, activate RoverBuddy, and reach the exit as quickly as possible. Your completion time can be saved to a local leaderboard.

## How to play

- Start a new game from the main menu.
- Explore the level and collect the required items.
- Use door remotes and RoverBuddy activators to unlock paths and progress through the levels.
- Reach RoverBuddy after collecting all required items to complete the level.
- Save your completion time to the leaderboard when prompted.

## Controls

- Movement: `WASD` or Arrow Keys
- Run: Hold `Shift`
- Crawl: Hold `C`
- Pause/Resume: `Escape`

## Main game logic files

- `Assets/Scripts/PlayerController.cs`
- `Assets/Scripts/SceneChanger.cs`
- `Assets/Scripts/StopwatchController.cs`
- `Assets/Scripts/DoorRemoteController.cs`
- `Assets/Scripts/HorizontalDoorController.cs`
- `Assets/Scripts/VerticalDoorController.cs`
- `Assets/Scripts/RoverBuddyActivatorController.cs`
- `Assets/Scripts/RoverBuddyController.cs`
- `Assets/Scripts/PauseMenuController.cs`
- `Assets/Scripts/LeaderboardController.cs`
- `Assets/Scripts/LeaderboardManager.cs`

## Scenes

- `Assets/Scenes/Start.unity` - Main menu
- `Assets/Scenes/Level0_0.unity` through `Assets/Scenes/Level0_4.unity` - Tutorial adventure levels
- `Assets/Scenes/Level1_0.unity` - Main adventure level
- `Assets/Scenes/Leaderboard.unity` - Completion leaderboard

## Project information

- Engine: Unity `6000.6.0f1`
- The leaderboard is saved as `leaderboard.json` in Unity's persistent data path.
- Scene transitions use a fade effect, and the timer carries across levels.

## Assets used

- Game Assets: https://assetstore.unity.com/packages/2d/characters/2d-character-astronaut-182650
- UI Assets: https://assetstore.unity.com/packages/2d/gui/icons/strategic-warfare-sci-fi-ui-starter-pack-391120
