using System;
using TMPro;
using UnityEngine;

public class StopwatchController : MonoBehaviour
{
	[SerializeField] private TMP_Text timerText;
	[SerializeField] private float flashDuration = 0.5f;

	private float _currentTime;
	private float _flashTimer;
	private bool _isTimerActive;

	private void Start()
	{
		_currentTime = 0f;
		_isTimerActive = false;
		DisplayTime(_currentTime);
	}

	private void Update()
	{
		if (_isTimerActive)
		{
			_currentTime += Time.deltaTime;
		}
		else
		{
			_flashTimer += Time.deltaTime;
			if (_flashTimer >= flashDuration)
			{
				_flashTimer = 0f;
				timerText.enabled = !timerText.enabled;
			}
		}

		DisplayTime(_currentTime);
	}

	private void DisplayTime(float timeToDisplay)
	{
		var timeSpan = TimeSpan.FromSeconds(timeToDisplay);

		timerText.text = $"{timeSpan.Minutes:00}:{timeSpan.Seconds:00}.{timeSpan.Milliseconds:000}";
	}

	public void StartStopwatch()
	{
		_isTimerActive = true;
		timerText.enabled = true;
	}

	public void StopStopwatch()
	{
		_isTimerActive = false;
	}

	public void ResetStopwatch()
	{
		_isTimerActive = false;
		_currentTime = 0f;
		DisplayTime(_currentTime);
	}
}
