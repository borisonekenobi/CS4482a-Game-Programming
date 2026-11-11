using EasyPeasyFirstPersonController;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private Canvas pauseMenuCanvas;
    [SerializeField] private InputAction pauseAction;

    private bool paused;

    private void Start()
    {
        pauseAction.Enable();
        Resume();
    }

    private void Update()
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
}
