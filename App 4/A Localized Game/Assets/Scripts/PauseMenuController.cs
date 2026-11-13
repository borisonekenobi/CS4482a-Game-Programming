using Borisonekenobi.Localization;
using EasyPeasyFirstPersonController;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private Canvas pauseMenuCanvas;
    [SerializeField] private Canvas languageMenuCanvas;
    [SerializeField] private InputAction pauseAction;

    [Header("Text Localization")]
    [SerializeField] private TMP_Text resumeButtonText;
    [SerializeField] private TMP_Text restartButtonText;
    [SerializeField] private TMP_Text quitButtonText;

    private bool paused;
    private string currentLanguage;

    private void Start()
    {
        pauseAction.Enable();
        Resume();
    }

    private void Update()
    {
        HandleLocalization();
        HandlePause();
    }

    private void HandleLocalization()
    {
        if (Settings.Instance.Language == currentLanguage) return;

        resumeButtonText.text = Localization.Get("pause_menu_resume");
        restartButtonText.text = Localization.Get("pause_menu_restart");
        quitButtonText.text = Localization.Get("pause_menu_quit");

        currentLanguage = Settings.Instance.Language;
    }

    private void HandlePause()
    {
        if (!pauseAction.triggered) return;

        if (paused) Resume();
        else Pause();
    }

    private void Pause()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuCanvas.enabled = true;
        firstPersonController.paused = true;
        paused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuCanvas.enabled = false;
        firstPersonController.paused = false;
        paused = false;
    }

    public void Restart()
    {
        SceneFader.Instance.FadeToScene("Scenes/Level");
    }

    public void Quit()
    {
        SceneFader.Instance.FadeToScene("Scenes/MainMenu");
    }

    public void OpenLanguageMenu()
    {
        pauseMenuCanvas.enabled = false;
        languageMenuCanvas.enabled = true;
    }

    public void CloseLanguageMenu()
    {
        pauseMenuCanvas.enabled = true;
        languageMenuCanvas.enabled = false;
    }
}
