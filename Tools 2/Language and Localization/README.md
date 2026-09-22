# Language and Localization

This project is a Unity localization demo that stores translated text in a `LocalizationDatabase` asset and exposes the selected language through an editor menu. The sample scene displays translated TextMeshPro text while the language can be changed from `Window > Languages`.

## How it works

- Open `Window > Localization...` and assign `Assets/Resources/LocalizationDatabase.asset` to the database field.
- Add or remove localization key rows and edit the key/value for each language. The current database includes English, Spanish, French, German, Chinese (Simplified), Japanese, Korean, Russian, Bulgarian, and Polish.
- Click `Save` or `Save and Close` to save the asset and regenerate the language menu in `Assets/Scripts/GeneratedLanguages.cs`.
- Select a language from `Window > Languages > <language>`. The selection updates the current language in `Settings`.
- Open `Assets/Scenes/SampleScene.unity` and run it to see the `TextSwitcher` component cycle through `key_ok`, `key_cancel`, `key_save`, `key_back`, and `key_next` every two seconds.
- Runtime lookups use `Localization.Get(key)`. If the database, language, or key is missing, the requested key is returned as a fallback.
- You can add new languages by adding a new row in the inspector window for the LocalizationDatabase asset.
- Switching languages doesn't require a scene reload, and the `TextSwitcher` component will update automatically.

## Main editor, data, and runtime files

- `Assets/Scripts/LocalizationTool.cs`
- `Assets/Scripts/DynamicLanguageGenerator.cs`
- `Assets/Scripts/GeneratedLanguages.cs`
- `Assets/Scripts/LocalizationData.cs`
- `Assets/Scripts/Localization.cs`
- `Assets/Scripts/LanguageMenu.cs`
- `Assets/Scripts/Settings.cs`
- `Assets/Scripts/TextSwitcher.cs`
- `Assets/Resources/LocalizationDatabase.asset`
- `Assets/Scenes/SampleScene.unity`
