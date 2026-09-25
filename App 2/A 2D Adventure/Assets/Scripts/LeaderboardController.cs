using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardController : MonoBehaviour
{
	[SerializeField] private Canvas leaderboardCanvas;
	[SerializeField] private Canvas saveScoreCanvas;
	[SerializeField] private TMP_InputField nameInputField;
	[SerializeField] private LeaderboardManager leaderboardManager;
	[SerializeField] private GameObject leaderboard;

	private float _time;
	private bool HasTime => _time > 0.0f;

	private void Start()
	{
		_time = PlayerPrefs.GetFloat("Time");
		ShowCanvases();
	}

	public void SaveLeaderboard()
	{
		var playerName = nameInputField.text;
		var time = _time;

		leaderboardManager.AddEntry(playerName, time);

		_time = 0.0f;
		PlayerPrefs.SetFloat("Time", 0.0f);
		ShowCanvases();
	}

	private void ShowCanvases()
	{
		saveScoreCanvas.gameObject.SetActive(HasTime);
		leaderboardCanvas.gameObject.SetActive(!HasTime);

		if (HasTime) return;

		var entries = leaderboardManager.GetEntries();
		foreach (Transform child in leaderboard.transform) Destroy(child.gameObject);
		foreach (var entry in entries)
		{
			var rowObject = new GameObject("LeaderboardRow");
			rowObject.transform.SetParent(leaderboard.transform, false);

			var horizontalLayout = rowObject.AddComponent<HorizontalLayoutGroup>();
			horizontalLayout.childControlWidth = true;
			horizontalLayout.childControlHeight = true;
			horizontalLayout.childForceExpandWidth = true;
			horizontalLayout.childForceExpandHeight = false;

			horizontalLayout.padding = new RectOffset(10, 10, 5, 5);

			var nameObject = new GameObject("NameText");
			nameObject.transform.SetParent(rowObject.transform, false);
			var nameText = nameObject.AddComponent<TextMeshProUGUI>();
			nameText.text = entry.name;
			nameText.fontSize = 24;
			nameText.color = Color.black;
			nameText.alignment = TextAlignmentOptions.Left;

			var timeObject = new GameObject("TimeText");
			timeObject.transform.SetParent(rowObject.transform, false);
			var timeText = timeObject.AddComponent<TextMeshProUGUI>();
			var timeSpan = TimeSpan.FromSeconds(entry.time);
			timeText.text = $"{timeSpan.Minutes:00}:{timeSpan.Seconds:00}.{timeSpan.Milliseconds:000}";
			timeText.fontSize = 24;
			timeText.color = Color.black;
			timeText.alignment = TextAlignmentOptions.Right;
		}
	}
}
