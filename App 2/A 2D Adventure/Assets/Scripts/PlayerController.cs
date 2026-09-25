using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	private static readonly int MoveX = Animator.StringToHash("Move X");
	private static readonly int MoveY = Animator.StringToHash("Move Y");
	private static readonly int Speed = Animator.StringToHash("Speed");
	private static readonly int Crouch = Animator.StringToHash("Crouch");

	[SerializeField] private StopwatchController stopwatchController;
	[SerializeField] private PauseMenuController pauseMenuController;
	[SerializeField] private InputAction movementAction;
	[SerializeField] private InputAction runAction;
	[SerializeField] private InputAction crawlAction;
	[SerializeField] private InputAction pauseAction;
	[SerializeField] private float speed;
	[SerializeField] private float runMultiplier;
	[SerializeField] private float crawlMultiplier;
	[SerializeField] private Rigidbody2D rigidbody2D;
	[SerializeField] private Animator animator;

	private bool _facingLeft = true;
	private Vector2 _move;
	private bool _timerStarted;

	private void Start()
	{
		movementAction.Enable();
		runAction.Enable();
		crawlAction.Enable();
		pauseAction.Enable();
	}

	private void Update()
	{
		if (pauseAction.triggered) pauseMenuController.Trigger();
		if (pauseMenuController.isPaused) return;

		_move = movementAction.ReadValue<Vector2>();
		_move.Normalize();

		if (!_timerStarted && _move.magnitude != 0)
		{
			_timerStarted = true;
			stopwatchController.StartStopwatch();
		}

		var running = runAction.ReadValue<float>() > 0;
		var crouching = crawlAction.ReadValue<float>() > 0;

		if (running) _move *= runMultiplier;
		else if (crouching) _move *= crawlMultiplier;

		if (_move.x != 0)
		{
			switch (_move.x)
			{
				case > 0 when _facingLeft:
				case < 0 when !_facingLeft:
					Flip();
					break;
			}

			animator.SetFloat(MoveX, _move.x);
			animator.SetFloat(MoveY, _move.y);
		}
		else if (_move.y != 0)
		{
			animator.SetFloat(MoveX, 0f);
			animator.SetFloat(MoveY, _move.y);
		}

		animator.SetFloat(Speed, _move.magnitude);
		animator.SetBool(Crouch, crouching && !running);
	}

	private void FixedUpdate()
	{
		rigidbody2D.linearVelocity = _move * speed;
	}

	private void OnDisable()
	{
		movementAction.Disable();
		runAction.Disable();
		crawlAction.Disable();
		pauseAction.Disable();
	}

	private void Flip()
	{
		_facingLeft = !_facingLeft;

		var currentScale = transform.localScale;
		currentScale.x *= -1;
		transform.localScale = currentScale;
	}
}
