using System.Collections.Generic;
using System.Linq;
using Borisonekenobi.Localization;
using TMPro;
using UnityEngine;

public class ScoreSceneController : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text subtitleText;
    [SerializeField] private TMP_Text scoreLabelText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text leftTitleText;
    [SerializeField] private TMP_Text rightTitleText;
    [SerializeField] private TMP_Text statisticsText;
    [SerializeField] private TMP_Text resultsText;

    private void Start()
    {
        titleText.text = Localization.Get("score_title");
        subtitleText.text = Localization.Get("score_journey_complete");
        scoreLabelText.text = Localization.Get("score_exploration_score");
        leftTitleText.text = Localization.Get("score_statistic");
        rightTitleText.text = Localization.Get("score_result");

        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        var score = CalculateScore();
        var highScore = PlayerPrefs.GetInt("HighScore", 0);
        var newHighScore = score > highScore;
        if (newHighScore) PlayerPrefs.SetInt("HighScore", score);

        scoreText.text = CalculateScore().ToString();
        highScoreText.text = newHighScore
            ? Localization.Get("score_new_high_score")
            : $"{Localization.Get("score_high_score")}: {highScore}";

        statisticsText.text = "";
        resultsText.text = "";

        foreach (var item in GameData.Inventory)
        {
            statisticsText.text += $"{Localization.Get(item.Key.ToString())}\n";
            resultsText.text += $"×{item.Value}\n";
        }

        statisticsText.text += $"\n{Localization.Get("score_objectives_completed")}";
        resultsText.text += $"\n{GameData.Objectives.Count}/{GameData.TotalObjectives}";

        GameData.Reset();
    }

    private static int CalculateScore()
    {
        var points = new Dictionary<Item, int>
        {
            { Item.None, 0 },
            { Item.Daffodil, 10 },
            { Item.Hyacinth, 10 },
            { Item.Note, 50 },
            { Item.OrangeMushroom, 100 },
            { Item.PurpleMushroom, 70 },
            { Item.Ruby, -100_000 }, // Negative points for cheaters
            { Item.Sunflower, 20 }
        };

        return GameData.Inventory.Sum(item => points[item.Key] * item.Value) +
               GameData.Objectives.Sum(objective => objective.points);
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
