using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
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
