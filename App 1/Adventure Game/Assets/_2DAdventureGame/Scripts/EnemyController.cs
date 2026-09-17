using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private static readonly int MoveXId = Animator.StringToHash("Move X");
    private static readonly int MoveYId = Animator.StringToHash("Move Y");

    [SerializeField] private float speed;
    [SerializeField] private bool vertical;
    [SerializeField] private float changeTime = 3.0f;

    private Animator _animator;
    private bool _broken = true;
    private int _direction = 1;
    private Rigidbody2D _rigidbody2D;
    private float _timer;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _timer = changeTime;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer >= 0) return;

        _direction = -_direction;
        _timer = changeTime;
    }

    private void FixedUpdate()
    {
        if (!_broken) return;

        var position = _rigidbody2D.position;

        if (vertical)
        {
            position.y += speed * _direction * Time.deltaTime;
            _animator.SetFloat(MoveXId, 0);
            _animator.SetFloat(MoveYId, _direction);
        }
        else
        {
            position.x += speed * _direction * Time.deltaTime;
            _animator.SetFloat(MoveXId, _direction);
            _animator.SetFloat(MoveYId, 0);
        }

        _rigidbody2D.MovePosition(position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<PlayerController>();

        if (player != null) player.ChangeHealth(-1);
    }

    public void Fix()
    {
        _broken = false;
        _rigidbody2D.simulated = false;
    }
}
