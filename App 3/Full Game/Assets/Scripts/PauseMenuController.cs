using EasyPeasyFirstPersonController;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private Canvas pauseMenuCanvas;
    [SerializeField] private InputAction pauseAction;

    private bool _paused;

    private void Start()
    {
        pauseAction.Enable();
        Resume();
    }

    private void Update()
    {
        if (!pauseAction.triggered) return;

        if (_paused) Resume();
        else Pause();
    }

    private void Pause()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuCanvas.enabled = true;
        firstPersonController.paused = true;
        _paused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuCanvas.enabled = false;
        firstPersonController.paused = false;
        _paused = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Scenes/Level");
    }

    public void Quit()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }
}
