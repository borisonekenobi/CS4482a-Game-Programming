## Localized Game

This is the localized version of The Ruby Valley. This combines the original App 3 game with the original Tools 3
project. The game is localized in Bulgarian,English, and French. The player can select the language from the main menu
or the puase menu.

The game is a first-person exploration and collection game set in a forest valley. Explore the environment, collect
plants and hidden notes, track progress in the journal, find the hidden ruby that, and bring it to the campfire to save
yourself. Completing the level leads to a score screen showing collected items and completed objectives.

## How to play

1. Start from the main menu and enter the valley.
2. Explore the valley and interact with nearby collectibles. The interaction prompt appears when an interactable object
   is in front of the player.
3. Collect daffodils, hyacinths, orange mushrooms, purple mushrooms, sunflowers, notes, and the ruby. Collected items
   are tracked in the inventory.
4. Open the journal to review inventory, objective progress, and any notes found. Objectives unlock and complete as
   collection requirements and prerequisites are met.
5. Find the ruby and take it to the campfire. Interacting with the campfire consumes the ruby and completes the game.
6. Review the final score, item statistics, and completed-objective count. From there, return to the main menu or
   restart the level.

The objective system includes exploring the valley, collecting increasing amounts of orange and purple mushrooms,
finding nine hidden notes, finding the ruby, and saving yourself. Objective points and item values contribute to the
final score.

## Controls

- Movement: `WASD` or Arrow Keys
- Look Around: Mouse
- Jump: `Space`
- Run: Hold `Shift`
- Crouch: Hold `Ctrl`
- Flashlight: `F`
- Interact: `E`
- Open/Close Journal: `B`
- Previous Journal Page: `Z`
- Next Journal Page: `X`
- Pause/Resume: `Esc` or `P`

## Main game logic files

- `Assets/Scripts/ButtonController.cs`
- `Assets/Scripts/CampfireController.cs`
- `Assets/Scripts/Collectible.cs`
- `Assets/Scripts/Daffodil.cs`
- `Assets/Scripts/FinishSceneController.cs`
- `Assets/Scripts/FlashlightController.cs`
- `Assets/Scripts/FoliageAudio.cs`
- `Assets/Scripts/GameData.cs`
- `Assets/Scripts/Hyacinth.cs`
- `Assets/Scripts/IInteractable.cs`
- `Assets/Scripts/IJournalPage.cs`
- `Assets/Scripts/Inventory.cs`
- `Assets/Scripts/Item.cs`
- `Assets/Scripts/JournalController.cs`
- `Assets/Scripts/LightController.cs`
- `Assets/Scripts/MainMenuController.cs`
- `Assets/Scripts/Note.cs`
- `Assets/Scripts/ObjectiveDefinition.cs`
- `Assets/Scripts/ObjectiveManager.cs`
- `Assets/Scripts/OrangeMushroom.cs`
- `Assets/Scripts/PauseMenuController.cs`
- `Assets/Scripts/PlayerFootsteps.cs`
- `Assets/Scripts/PlayerInteraction.cs`
- `Assets/Scripts/PurpleMushroom.cs`
- `Assets/Scripts/Ruby.cs`
- `Assets/Scripts/RubySpawner.cs`
- `Assets/Scripts/SceneFader.cs`
- `Assets/Scripts/ScoreSceneController.cs`
- `Assets/Scripts/StreamAudio.cs`
- `Assets/Scripts/Sunflower.cs`

## Scenes

- `Assets/Scenes/MainMenu.unity` - Start and quit menu.
- `Assets/Scenes/Level.unity` - Main forest-valley exploration level.
- `Assets/Scenes/Finish.unity` - Short completion transition.
- `Assets/Scenes/Score.unity` - Final score, inventory statistics, and objective results.

## Project information

- Engine: Unity `6000.6.0f1`
- Course: Western University CS 4482A - Game Programming
- Assignment: App 3 - Write a Full Game
- Team size: Solo project

## Assets used

- Game Assets:
    - https://assetstore.unity.com/packages/3d/environments/embersstorm-free-nature-pack-369910
    - https://assetstore.unity.com/packages/3d/environments/fantasy/willowwood-free-low-poly-nature-pack-386152
    - https://assetstore.unity.com/packages/3d/environments/landscapes/free-low-poly-nature-forest-205742
    - https://assetstore.unity.com/packages/tools/physics/easy-peasy-first-person-controller-317073
    - https://kenney-assets.itch.io/ui-pack
    - https://www.flaticon.com/free-icon/crosshair_2659018?term=crosshair&page=1&position=1&origin=tag&related_id=2659018
    - https://www.flaticon.com/free-icon/language-skills_9583765?term=language&page=1&position=65&origin=search&related_id=9583765
- Audio Assets:
    - https://opengameart.org/content/nature-theme-sketch
    - https://opengameart.org/content/nature-sounds-pack
    - https://pixabay.com/sound-effects/film-special-effects-fire-whoosh-flame-ignite-sound-561959/
    - https://pixabay.com/sound-effects/nature-fire-crackling-356123/
    - https://opengameart.org/content/gem-collect-sfx
- Other Assets:
    - https://www.1001fonts.com/monospace-font.html
