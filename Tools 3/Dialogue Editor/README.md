# Dialogue Editor

This project is a Unity dialogue editor and runtime demo. Dialogue is authored as a connected graph of start, choice, and end nodes, saved in a `DialogueGraphAsset`, and displayed at runtime as Unity UI buttons. Dialogue text and choice labels are localization keys, so the language can be changed from the Unity editor menu.

## How it works

- Open `Window > Dialogue Editor` to open the graph editor, or double-click `Assets/Resources/DialogueGraph.asset` to open it directly.
- Assign a `DialogueGraphAsset` in the editor's `Graph Asset` field. An empty asset is initialized with a start node and an end node.
- Click `Create Dialogue Node` to add a dialogue node. Enter the speaker's localization key in `Speaker Localization Key`, then use `+ Add Choice` to add choice output ports.
- Connect the `Start Node` to the first dialogue node, connect each choice port to its next node, and connect the final choice to the `End Node`. Each choice port contains the localization key used for its button label.
- Click `Save Graph Data` or press `Ctrl+S` (`Cmd+S` on macOS) to serialize node positions, speaker keys, choice keys, and graph connections to the selected asset.
- Quick guide to the localization system ([Full README](../../Tools%202/Language%20and%20Localization/README.md)):
  - Open `Window > Localization...` and assign `Assets/Resources/LocalizationDatabase.asset` to edit the keys used by the graph. Add a row for each dialogue line or choice, enter translations for each language, and click `Save`.
  - Saving the localization database regenerates `Assets/Scripts/GeneratedLanguages.cs`. Select a language from `Window > Languages > <language>` before running the scene.
- Open `Assets/Scenes/SampleScene.unity` and run it to see `DialogueSystemController` create the dialogue text and choice buttons on the assigned Canvas. Selecting a button advances to its connected node; reaching the end node clears the dialogue UI.
- Runtime lookups use `Localization.Get(key)`. If the database, selected language, or key is missing, the key itself is displayed as the fallback.

## Main editor, data, localization, and runtime files

- `Assets/Scripts/Editor/DialogueEditorWindow.cs`
- `Assets/Scripts/Editor/DialogueGraphView.cs`
- `Assets/Scripts/Editor/NodeExtensions.cs`
- `Assets/Scripts/DialogueGraphAsset.cs`
- `Assets/Scripts/DialogueNodeData.cs`
- `Assets/Scripts/DialogueSystemController.cs`
- `Assets/Scripts/GeneratedLanguages.cs`
- `Assets/Resources/DialogueGraph.asset`
- `Assets/Resources/LocalizationDatabase.asset`
- `Assets/Scenes/SampleScene.unity`
- The following files are part of the localization package, which is included in this project as a local package:
  - `Packages/com.borisonekenobi.localization/Localization.cs`
  - `Packages/com.borisonekenobi.localization/LocalizationData.cs`
  - `Packages/com.borisonekenobi.localization/Settings.cs`
  - `Packages/com.borisonekenobi.localization/Editor/LocalizationTool.cs`
  - `Packages/com.borisonekenobi.localization/Editor/DynamicLanguageGenerator.cs`
  - `Packages/com.borisonekenobi.localization/Editor/LanguageMenu.cs`