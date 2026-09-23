using UnityEngine;

public class DoorRemoteController : MonoBehaviour
{
	[SerializeField] private DoorController[] affectedDoors;

	private void Start()
	{
		if (affectedDoors.Length == 0) Debug.LogWarning("No doors assigned to the remote.");

		foreach (var door in affectedDoors) door.CollectiblesToOpen++;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.CompareTag("Player")) return;

		foreach (var door in affectedDoors) door.CollectiblesToOpen--;
		Destroy(gameObject);
	}
}
