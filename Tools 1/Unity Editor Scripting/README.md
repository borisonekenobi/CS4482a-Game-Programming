# Unity Editor Scripting

This project is a Unity editor scripting demo that adds a custom Enemy Designer window to the Unity editor. Instead of being a playable game, it focuses on creating reusable enemy data and prefabs from a user-friendly interface.

## How it works

- Open the custom editor window from `Window > Enemy Designer`.
- Choose one of three enemy types: Mage, Warrior, or Rogue.
- Configure the type-specific settings and shared stats such as health, power, crit chance, name, collider, and whether a Rigidbody should be added.
- Assign a base prefab and save the generated enemy as a ScriptableObject asset.
- The editor copies the selected prefab into the project, attaches the correct runtime component (`Mage`, `Warrior`, or `Rogue`), and stores the generated data on the prefab.

## Main editor and data files

- `Assets/Editor/EnemyDesignerWindow.cs`
- `Assets/Resources/CharacterData/Scripts/CharacterData.cs`
- `Assets/Resources/CharacterData/Scripts/MageData.cs`
- `Assets/Resources/CharacterData/Scripts/WarriorData.cs`
- `Assets/Resources/CharacterData/Scripts/RogueData.cs`
- `Assets/Scripts/Mage.cs`
- `Assets/Scripts/Warrior.cs`
- `Assets/Scripts/Rogue.cs`
- `Assets/Scripts/Types.cs`

## Tutorial used

- Unity Editor Scripting playlist: https://www.youtube.com/playlist?list=PL4CCSwmU04MiCnps1DRmwIEEH7gP9X3qq
