using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private static readonly int LaunchId = Animator.StringToHash("Launch");
    private static readonly int LookXId = Animator.StringToHash("Look X");
    private static readonly int LookYId = Animator.StringToHash("Look Y");
    private static readonly int SpeedId = Animator.StringToHash("Speed");
    private static readonly int HitId = Animator.StringToHash("Hit");

    [SerializeField] private InputAction moveAction;
    [SerializeField] private float speed = 3.0f;
    [SerializeField] public int maxHealth = 5;
    [SerializeField] private float timeInvincible = 2.0f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private InputAction launchAction;
    [SerializeField] private InputAction launchManyAction;

    private Animator _animator;
    private float _damageCooldown;
    private bool _isInvincible;
    private Vector2 _move;
    private Vector2 _moveDirection = new(1, 0);
    private Rigidbody2D _rigidbody2D;

    public int Health { get; private set; }

    private void Start()
    {
        moveAction.Enable();
        launchAction.Enable();
        launchManyAction.Enable();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        Health = maxHealth;
    }

    private void Update()
    {
        _move = moveAction.ReadValue<Vector2>();

        if (!Mathf.Approximately(_move.x, 0.0f) || !Mathf.Approximately(_move.y, 0.0f))
        {
            _moveDirection.Set(_move.x, _move.y);
            _moveDirection.Normalize();
        }

        _animator.SetFloat(LookXId, _moveDirection.x);
        _animator.SetFloat(LookYId, _moveDirection.y);
        _animator.SetFloat(SpeedId, _move.magnitude);

        if (_isInvincible)
        {
            _damageCooldown -= Time.deltaTime;
            if (_damageCooldown < 0) _isInvincible = false;
        }

        if (launchAction.WasPressedThisFrame()) Launch();
        if (launchManyAction.WasPressedThisFrame()) LaunchMany();
    }

    private void FixedUpdate()
    {
        var position = _rigidbody2D.position + _move * (speed * Time.deltaTime);
        _rigidbody2D.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (_isInvincible) return;
            _isInvincible = true;
            _damageCooldown = timeInvincible;
            _animator.SetTrigger(HitId);
        }

        Health = Mathf.Clamp(Health + amount, 0, maxHealth);
        Debug.Log(Health + "/" + maxHealth);
    }

    private void Launch()
    {
        var projectileObject =
            Instantiate(projectilePrefab, _rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);
        var projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(_moveDirection, 300);
        _animator.SetTrigger(LaunchId);
    }

    private void LaunchMany()
    {
        Launch();

        var projectileObject =
            Instantiate(projectilePrefab, _rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);
        var projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(Quaternion.Euler(0, 0, 45f) * _moveDirection, 300);

        projectileObject =
            Instantiate(projectilePrefab, _rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);
        projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(Quaternion.Euler(0, 0, -45f) * _moveDirection, 300);
    }
}
