using UnityEngine;
using UnityEngine.SceneManagement;

public class GroundController : MonoBehaviour
{
	private void OnTriggerExit2D(Collider2D other)
	{
		if (!other.CompareTag("Player")) return;

		SceneChanger.Instance.MoveToScene(SceneManager.GetActiveScene().name);
	}
}
