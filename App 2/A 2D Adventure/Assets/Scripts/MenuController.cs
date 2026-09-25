using UnityEngine;

public class MenuController : MonoBehaviour
{
	public void Back()
	{
		SceneChanger.Instance.MoveToScene("Start");
	}

	public void Exit()
	{
		Application.Quit();
	}
}
