using System.Linq;
using TMPro;
using UnityEngine;

public class ScoreSceneController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text statisticsText;
    [SerializeField] private TMP_Text resultsText;

    private void Start()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        var totalItems = GameData.Inventory.Sum(item => item.Value);
        var score = totalItems * 10;

        scoreText.text = score.ToString();
        statisticsText.text = "";
        resultsText.text = "";
        foreach (var item in GameData.Inventory)
        {
            statisticsText.text += $"{item.Key}\n";
            resultsText.text += $"×{item.Value}\n";
        }

        GameData.Reset();
    }

    public void GoToMainMenu()
    {
        SceneFader.Instance.FadeToScene("Scenes/MainMenu");
    }

    public void RestartGame()
    {
        SceneFader.Instance.FadeToScene("Scenes/Level");
    }
}
