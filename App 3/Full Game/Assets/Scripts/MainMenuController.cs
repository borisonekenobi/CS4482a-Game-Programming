using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/Level");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
