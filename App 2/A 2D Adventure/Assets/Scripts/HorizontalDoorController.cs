using System.Collections;
using UnityEngine;

public class HorizontalDoorController : DoorController
{
	private const float DoorOpenDistance = 1.28f;

	[SerializeField] private SpriteRenderer leftDoor;
	[SerializeField] private SpriteRenderer rightDoor;
	[SerializeField] private float speed = 5.0f;

	private Coroutine _doorCoroutine;

	private Vector3 _leftClosedPos;
	private Vector3 _leftOpenPos;
	private Vector3 _rightClosedPos;
	private Vector3 _rightOpenPos;

	private void Start()
	{
		_leftClosedPos = leftDoor.transform.position;
		_rightClosedPos = rightDoor.transform.position;

		_leftOpenPos = _leftClosedPos + new Vector3(-DoorOpenDistance, 0, 0);
		_rightOpenPos = _rightClosedPos + new Vector3(DoorOpenDistance, 0, 0);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (CollectiblesToOpen > 0)
		{
			errorText.SetText(LockedMessage);
			return;
		}

		if (_doorCoroutine != null) StopCoroutine(_doorCoroutine);

		_doorCoroutine = StartCoroutine(MoveDoors(_leftOpenPos, _rightOpenPos));
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		errorText.SetText("");
		if (CollectiblesToOpen > 0) return;
		if (_doorCoroutine != null) StopCoroutine(_doorCoroutine);

		_doorCoroutine = StartCoroutine(MoveDoors(_leftClosedPos, _rightClosedPos));
	}

	private IEnumerator MoveDoors(Vector3 leftTarget, Vector3 rightTarget)
	{
		while (Vector3.Distance(leftDoor.transform.position, leftTarget) > 0.001f)
		{
			leftDoor.transform.position = Vector3.MoveTowards(
				leftDoor.transform.position,
				leftTarget,
				speed * Time.deltaTime
			);

			rightDoor.transform.position = Vector3.MoveTowards(
				rightDoor.transform.position,
				rightTarget,
				speed * Time.deltaTime
			);

			yield return null;
		}

		leftDoor.transform.position = leftTarget;
		rightDoor.transform.position = rightTarget;
	}
}
