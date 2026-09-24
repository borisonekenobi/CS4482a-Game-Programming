using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
	[SerializeField] private Canvas pauseMenuUI;
	public bool isPaused;

	public void Trigger()
	{
		if (isPaused)
		{
			Resume();
		}
		else
		{
			Pause();
		}
	}

	public void Resume()
	{
		pauseMenuUI.gameObject.SetActive(false);
		Time.timeScale = 1f;
		isPaused = false;
	}

	public void Pause()
	{
		pauseMenuUI.gameObject.SetActive(true);
		Time.timeScale = 0f;
		isPaused = true;
	}

	public void QuitLevel()
	{
		Time.timeScale = 1f;
		SceneChanger.Instance.MoveToScene("Start");
	}

	// public void QuitGame()
	// {
	// 	Debug.Log("Quitting game...");
	// 	Application.Quit();
	// }
}
