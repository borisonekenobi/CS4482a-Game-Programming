using System.Collections;
using UnityEngine;

public class VerticalDoorController : DoorController
{
	private const float DoorOpenDistance = 1.28f;

	[SerializeField] private SpriteRenderer topDoor;
	[SerializeField] private SpriteRenderer bottomDoor;
	[SerializeField] private float speed = 5.0f;

	private Coroutine _doorCoroutine;

	private Vector3 _topClosedPos;
	private Vector3 _topOpenPos;
	private Vector3 _bottomClosedPos;
	private Vector3 _bottomOpenPos;

	private void Start()
	{
		_topClosedPos = topDoor.transform.position;
		_bottomClosedPos = bottomDoor.transform.position;

		_topOpenPos = _topClosedPos + new Vector3(0, DoorOpenDistance, 0);
		_bottomOpenPos = _bottomClosedPos + new Vector3(0, -DoorOpenDistance, 0);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (CollectiblesToOpen > 0)
		{
			errorText.SetText(LockedMessage);
			return;
		}

		if (_doorCoroutine != null) StopCoroutine(_doorCoroutine);

		_doorCoroutine = StartCoroutine(MoveDoors(_topOpenPos, _bottomOpenPos));
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		errorText.SetText("");
		if (CollectiblesToOpen > 0) return;
		if (_doorCoroutine != null) StopCoroutine(_doorCoroutine);

		_doorCoroutine = StartCoroutine(MoveDoors(_topClosedPos, _bottomClosedPos));
	}

	private IEnumerator MoveDoors(Vector3 topTarget, Vector3 bottomTarget)
	{
		while (Vector3.Distance(topDoor.transform.position, topTarget) > 0.001f)
		{
			topDoor.transform.position = Vector3.MoveTowards(
				topDoor.transform.position,
				topTarget,
				speed * Time.deltaTime
			);

			bottomDoor.transform.position = Vector3.MoveTowards(
				bottomDoor.transform.position,
				bottomTarget,
				speed * Time.deltaTime
			);

			yield return null;
		}

		topDoor.transform.position = topTarget;
		bottomDoor.transform.position = bottomTarget;
	}
}
