using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneFader.Instance.FadeToScene("Scenes/Level");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
