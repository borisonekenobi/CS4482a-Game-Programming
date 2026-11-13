using Borisonekenobi.Localization;
using TMPro;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [Header("Menu Canvases")]
    [SerializeField] private Canvas mainMenuCanvas;
    [SerializeField] private Canvas languageMenuCanvas;

    [Header("Text Localization")]
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text startButtonText;
    [SerializeField] private TMP_Text quitButtonText;

    private string currentLanguage;

    private void Update()
    {
        if (Settings.Instance.Language == currentLanguage) return;

        titleText.text = Localization.Get("main_menu_title");
        startButtonText.text = Localization.Get("main_menu_start");
        quitButtonText.text = Localization.Get("main_menu_quit");

        currentLanguage = Settings.Instance.Language;
    }

    public void StartGame()
    {
        SceneFader.Instance.FadeToScene("Scenes/Level");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenLanguageMenu()
    {
        mainMenuCanvas.enabled = false;
        languageMenuCanvas.enabled = true;
    }

    public void CloseLanguageMenu()
    {
        mainMenuCanvas.enabled = true;
        languageMenuCanvas.enabled = false;
    }
}
