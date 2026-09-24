using UnityEngine;

public class RoverBuddyActivatorController : MonoBehaviour
{
	[SerializeField] private RoverBuddyController roverBuddyController;
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private Sprite redSprite;
	[SerializeField] private Sprite greenSprite;

	private void Start()
	{
		roverBuddyController.collectiblesToPower++;
		spriteRenderer.sprite = redSprite;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.CompareTag("Player")) return;

		roverBuddyController.collectiblesToPower--;
		spriteRenderer.sprite = greenSprite;
	}
}
