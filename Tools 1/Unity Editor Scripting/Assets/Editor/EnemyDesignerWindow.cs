using UnityEditor;
using UnityEngine;

public class EnemyDesignerWindow : EditorWindow
{
    Texture2D headerSectionTexture;
    Texture2D mageSectionTexture;
    Texture2D warriorSectionTexture;
    Texture2D rogueSectionTexture;

    Color headerSectionColor = new Color(13f/255f, 32f/255f, 44f/255f, 1f);

    Rect headerSection;
    Rect mageSection;
    Rect warriorSection;
    Rect rogueSection;

    [MenuItem("Window/Enemy Designer")]
    static void OpenWindow()
    {
        EnemyDesignerWindow window = (EnemyDesignerWindow)GetWindow(typeof(EnemyDesignerWindow));
        window.minSize = new Vector2(600, 300);
        window.Show();
    }

    void OnEnable()
    {
        InitTextures();
    }

    void InitTextures()
    {
        headerSectionTexture = new Texture2D(1, 1);
        headerSectionTexture.SetPixel(0, 0, headerSectionColor);
        headerSectionTexture.Apply();

        mageSectionTexture = Resources.Load<Texture2D>("icons/editor_mage_gradient");
        warriorSectionTexture = Resources.Load<Texture2D>("icons/editor_warrior_gradient");
        rogueSectionTexture = Resources.Load<Texture2D>("icons/editor_rogue_gradient");
    }

    void OnGUI()
    {
        DrawLayouts();
        DrawHeader();
        DrawMageSettings();
        DrawWarriorSettings();
        DrawRogueSettings();
    }

    void DrawLayouts()
    {
        headerSection.x = 0;
        headerSection.y = 0;
        headerSection.width = position.width;
        headerSection.height = 50;

        mageSection.x = 0;
        mageSection.y = 50;
        mageSection.width = position.width / 3f;
        mageSection.height = position.height - 50;

        warriorSection.x = position.width / 3f;
        warriorSection.y = 50;
        warriorSection.width = position.width / 3f;
        warriorSection.height = position.height - 50;

        rogueSection.x = 2 * position.width / 3f;
        rogueSection.y = 50;
        rogueSection.width = position.width / 3f;
        rogueSection.height = position.height - 50;

        GUI.DrawTexture(headerSection, headerSectionTexture);
        GUI.DrawTexture(mageSection, mageSectionTexture);
        GUI.DrawTexture(warriorSection, warriorSectionTexture);
        GUI.DrawTexture(rogueSection, rogueSectionTexture);
    }

    void DrawHeader()
    {
        GUILayout.BeginArea(headerSection);

        

        GUILayout.EndArea();
    }

    void DrawMageSettings()
    {
        GUILayout.BeginArea(mageSection);

        

        GUILayout.EndArea();
    }

    void DrawWarriorSettings()
    {
        GUILayout.BeginArea(warriorSection);

        

        GUILayout.EndArea();
    }

    void DrawRogueSettings()
    {
        GUILayout.BeginArea(rogueSection);

        

        GUILayout.EndArea();
    }
}
