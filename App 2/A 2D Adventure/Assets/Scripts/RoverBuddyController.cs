using TMPro;
using UnityEngine;

public class RoverBuddyController : MonoBehaviour
{
	private const string DisabledMessage = "RoverBuddy is disabled! Collect all items to open.";

	[SerializeField] private string nextSceneName;
	[SerializeField] private StopwatchController stopwatchController;
	[SerializeField] private ParticleSystem particles;
	[SerializeField] private TMP_Text errorText;

	[HideInInspector] public int collectiblesToPower;

	private void Update()
	{
		if (collectiblesToPower == 0) particles.Stop();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.CompareTag("Player")) return;
		if (collectiblesToPower > 0)
		{
			errorText.SetText(DisabledMessage);
			return;
		}

		stopwatchController.StopStopwatch();
		SceneChanger.Instance.MoveToScene(nextSceneName);
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		errorText.SetText(string.Empty);
	}
}
