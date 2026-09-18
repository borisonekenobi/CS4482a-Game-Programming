using System;
using Types;
using UnityEditor;
using UnityEngine;

public class EnemyDesignerWindow : EditorWindow
{
    private Texture2D _headerSectionTexture;
    private Texture2D _mageSectionTexture;
    private Texture2D _warriorSectionTexture;
    private Texture2D _rogueSectionTexture;
    private Texture2D _mageTexture;
    private Texture2D _warriorTexture;
    private Texture2D _rogueTexture;

    private readonly Color _headerSectionColor = new(13f/255f, 32f/255f, 44f/255f, 1f);

    private Rect _headerSection;
    private Rect _mageSection;
    private Rect _warriorSection;
    private Rect _rogueSection;
    private Rect _mageIconSection;
    private Rect _warriorIconSection;
    private Rect _rogueIconSection;

    private GUISkin _skin;

    public static MageData MageInfo { get; private set; }
    public static WarriorData WarriorInfo { get; private set; }
    public static RogueData RogueInfo { get; private set; }

    private const float IconSize = 40f;

    [MenuItem("Window/Enemy Designer")]
    private static void OpenWindow()
    {
        var window = GetWindow<EnemyDesignerWindow>();
        window.minSize = new Vector2(600, 300);
        window.Show();
    }

    private void OnEnable()
    {
        InitTextures();
        InitData();
        _skin = Resources.Load<GUISkin>("GUIStyles/EnemyDesignerSkin");
    }

    private static void InitData()
    {
        MageInfo = CreateInstance<MageData>();
        WarriorInfo = CreateInstance<WarriorData>();
        RogueInfo = CreateInstance<RogueData>();
    }

    private void InitTextures()
    {
        _headerSectionTexture = new Texture2D(1, 1);
        _headerSectionTexture.SetPixel(0, 0, _headerSectionColor);
        _headerSectionTexture.Apply();

        _mageSectionTexture = Resources.Load<Texture2D>("icons/editor_mage_gradient");
        _warriorSectionTexture = Resources.Load<Texture2D>("icons/editor_warrior_gradient");
        _rogueSectionTexture = Resources.Load<Texture2D>("icons/editor_rogue_gradient");

        _mageTexture = Resources.Load<Texture2D>("icons/editor_mage_gradient");
        _warriorTexture = Resources.Load<Texture2D>("icons/editor_warrior_gradient");
        _rogueTexture = Resources.Load<Texture2D>("icons/editor_rogue_gradient");
    }

    private void OnGUI()
    {
        DrawLayouts();
        DrawHeader();
        DrawMageSettings();
        DrawWarriorSettings();
        DrawRogueSettings();
    }

    private void DrawLayouts()
    {
        _headerSection.x = 0;
        _headerSection.y = 0;
        _headerSection.width = position.width;
        _headerSection.height = 50;

        _mageSection.x = 0;
        _mageSection.y = 50;
        _mageSection.width = position.width / 3f;
        _mageSection.height = position.height - 50;

        _mageIconSection.x = _mageSection.x + _mageSection.width / 2f - IconSize / 2f;
        _mageIconSection.y = _mageSection.y + 8;
        _mageIconSection.width = IconSize;
        _mageIconSection.height = IconSize;

        _warriorSection.x = position.width / 3f;
        _warriorSection.y = 50;
        _warriorSection.width = position.width / 3f;
        _warriorSection.height = position.height - 50;

        _warriorIconSection.x = _warriorSection.x + _warriorSection.width / 2f - IconSize / 2f;
        _warriorIconSection.y = _warriorSection.y + 8;
        _warriorIconSection.width = IconSize;
        _warriorIconSection.height = IconSize;

        _rogueSection.x = 2 * position.width / 3f;
        _rogueSection.y = 50;
        _rogueSection.width = position.width / 3f;
        _rogueSection.height = position.height - 50;

        _rogueIconSection.x = _rogueSection.x + _rogueSection.width / 2f - IconSize / 2f;
        _rogueIconSection.y = _rogueSection.y + 8;
        _rogueIconSection.width = IconSize;
        _rogueIconSection.height = IconSize;

        GUI.DrawTexture(_headerSection, _headerSectionTexture);
        GUI.DrawTexture(_mageSection, _mageSectionTexture);
        GUI.DrawTexture(_warriorSection, _warriorSectionTexture);
        GUI.DrawTexture(_rogueSection, _rogueSectionTexture);
        GUI.DrawTexture(_mageIconSection, _mageTexture);
        GUI.DrawTexture(_warriorIconSection, _warriorTexture);
        GUI.DrawTexture(_rogueIconSection, _rogueTexture);
    }

    private void DrawHeader()
    {
        GUILayout.BeginArea(_headerSection);

        GUILayout.Label("Enemy Designer", _skin.GetStyle("Header1"));

        GUILayout.EndArea();
    }

    private void DrawMageSettings()
    {
        GUILayout.BeginArea(_mageSection);

        GUILayout.Space(IconSize + 8);

        GUILayout.Label("Mage", _skin.GetStyle("MageHeader"));

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Damage", _skin.GetStyle("MageField"));
        MageInfo.dmgType = (MageDmgType)EditorGUILayout.EnumPopup(MageInfo.dmgType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon", _skin.GetStyle("MageField"));
        MageInfo.wpnType = (MageWpnType)EditorGUILayout.EnumPopup(MageInfo.wpnType);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.Mage);
        }

        GUILayout.EndArea();
    }

    private void DrawWarriorSettings()
    {
        GUILayout.BeginArea(_warriorSection);

        GUILayout.Space(IconSize + 8);

        GUILayout.Label("Warrior", _skin.GetStyle("WarriorHeader"));

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Class", _skin.GetStyle("WarriorField"));
        WarriorInfo.classType = (WarriorClassType)EditorGUILayout.EnumPopup(WarriorInfo.classType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon", _skin.GetStyle("WarriorField"));
        WarriorInfo.wpnType = (WarriorWpnType)EditorGUILayout.EnumPopup(WarriorInfo.wpnType);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.Warrior);
        }

        GUILayout.EndArea();
    }

    private void DrawRogueSettings()
    {
        GUILayout.BeginArea(_rogueSection);

        GUILayout.Space(IconSize + 8);

        GUILayout.Label("Rogue", _skin.GetStyle("RogueHeader"));

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Strategy", _skin.GetStyle("RogueField"));
        RogueInfo.strategyType = (RogueStrategyType)EditorGUILayout.EnumPopup(RogueInfo.strategyType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon", _skin.GetStyle("RogueField"));
        RogueInfo.wpnType = (RogueWpnType)EditorGUILayout.EnumPopup(RogueInfo.wpnType);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.Rogue);
        }

        GUILayout.EndArea();
    }
}

public class GeneralSettings : EditorWindow
{
    public enum SettingsType
    {
        Mage,
        Warrior,
        Rogue
    }
    private static SettingsType _dataSetting;
    private static GeneralSettings _window;

    public static void OpenWindow(SettingsType setting)
    {
        _dataSetting = setting;
        _window = (GeneralSettings)GetWindow(typeof(GeneralSettings));
        _window.minSize = new Vector2(250, 200);
        _window.Show();
    }

    private void OnGUI()
    {
        switch (_dataSetting)
        {
            case SettingsType.Mage:
                DrawSettings(EnemyDesignerWindow.MageInfo);
                break;
            case SettingsType.Warrior:
                DrawSettings(EnemyDesignerWindow.WarriorInfo);
                break;
            case SettingsType.Rogue:
                DrawSettings(EnemyDesignerWindow.RogueInfo);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static void DrawSettings(CharacterData charData)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Prefab");
        charData.prefab = (GameObject)EditorGUILayout.ObjectField(charData.prefab, typeof(GameObject), false);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Max Health");
        charData.maxHealth = EditorGUILayout.FloatField(charData.maxHealth);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Max Energy");
        charData.maxEnergy = EditorGUILayout.FloatField(charData.maxEnergy);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Power");
        charData.power = EditorGUILayout.Slider(charData.power, 0, 100);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("% Crit Chance");
        charData.critChance = EditorGUILayout.Slider(charData.critChance, 0, charData.power);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name");
        charData.name = EditorGUILayout.TextField(charData.name);
        EditorGUILayout.EndHorizontal();

        if (charData.prefab == null)
        {
            EditorGUILayout.HelpBox("This enemy needs a [Prefab] before it can be created", MessageType.Warning);
        }
        else if (string.IsNullOrEmpty(charData.name))
        {
            EditorGUILayout.HelpBox("This enemy needs a [Name] before it can be created", MessageType.Warning);
        }
        else if (GUILayout.Button("Finish and Save", GUILayout.Height(30)))
        {
            SaveCharacterData();
            _window.Close();
        }
    }

    private static void SaveCharacterData()
    {
        string prefabPath; // path to the base prefab
        var newPrefabPath = "Assets/Prefabs/Characters/";
        var dataPath = "Assets/Resources/CharacterData/Data/";

        switch (_dataSetting)
        {
            case SettingsType.Mage:
                dataPath += "Mage/" + EnemyDesignerWindow.MageInfo.name + ".asset";
                AssetDatabase.CreateAsset(EnemyDesignerWindow.MageInfo, dataPath);

                newPrefabPath += "Mage/" + EnemyDesignerWindow.MageInfo.name + ".prefab";
                prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.MageInfo.prefab);
                AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                var magePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(newPrefabPath);
                if (!magePrefab.GetComponent<Mage>())
                    magePrefab.AddComponent(typeof(Mage));
                magePrefab.GetComponent<Mage>().mageData = EnemyDesignerWindow.MageInfo;

                break;

            case SettingsType.Warrior:
                dataPath += "Warrior/" + EnemyDesignerWindow.WarriorInfo.name + ".asset";
                AssetDatabase.CreateAsset(EnemyDesignerWindow.WarriorInfo, dataPath);

                newPrefabPath += "Warrior/" + EnemyDesignerWindow.WarriorInfo.name + ".prefab";
                prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.WarriorInfo.prefab);
                AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                var warriorPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(newPrefabPath);
                if (!warriorPrefab.GetComponent<Warrior>())
                    warriorPrefab.AddComponent(typeof(Warrior));
                warriorPrefab.GetComponent<Warrior>().warriorData = EnemyDesignerWindow.WarriorInfo;

                break;

            case SettingsType.Rogue:
                dataPath += "Rogue/" + EnemyDesignerWindow.RogueInfo.name + ".asset";
                AssetDatabase.CreateAsset(EnemyDesignerWindow.RogueInfo, dataPath);

                newPrefabPath += "Rogue/" + EnemyDesignerWindow.RogueInfo.name + ".prefab";
                prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.RogueInfo.prefab);
                AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                var roguePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(newPrefabPath);
                if (!roguePrefab.GetComponent<Rogue>())
                    roguePrefab.AddComponent(typeof(Rogue));
                roguePrefab.GetComponent<Rogue>().rogueData = EnemyDesignerWindow.RogueInfo;

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
