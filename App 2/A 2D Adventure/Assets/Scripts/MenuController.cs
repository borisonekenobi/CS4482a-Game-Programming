using UnityEngine;

public class MenuController : MonoBehaviour
{
	public void StartGame()
	{
		PlayerPrefs.SetFloat("Time", 0.0f);
		SceneChanger.Instance.MoveToScene("Level0_0");
	}

	public void Leaderboard()
	{
		SceneChanger.Instance.MoveToScene("Leaderboard");
	}

	public void Back()
	{
		SceneChanger.Instance.MoveToScene("Start");
	}

	public void Exit()
	{
		Application.Quit();
	}
}
