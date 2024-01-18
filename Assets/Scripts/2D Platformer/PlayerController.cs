using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string Horizontal = "Horizontal";

    [SerializeField] private LayerMask _ground;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private Material _blink;
    [SerializeField] private SpawnPoint _spawnPoint;
    [SerializeField] private float _movingSpeed;
    [SerializeField] private float _jumpingForce;

    private SpriteRenderer _spriteRenderer;
    private Material _default;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private float _moveInput;
    private float _checkRadius = 0.5f;
    private float _blinkTime = 0.2f;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _default = _spriteRenderer.material;
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        _moveInput = Input.GetAxis(Horizontal);

        _rigidbody.velocity = new Vector2(_moveInput * _movingSpeed, _rigidbody.velocity.y);

        if (_moveInput != 0)
        {
            FlipToSight();
            _animator.SetBool("IsMoving", true);
        }
        else
        {
            _animator.SetBool("IsMoving", false);
        }
    }

    private void Jump()
    {
        if (IsGrounded())
            if (Input.GetKeyDown(KeyCode.Space))
                _rigidbody.velocity = Vector2.up * _jumpingForce;
    }

    private void FlipToSight()
    {
        if (_moveInput > 0)
            _spriteRenderer.flipX = false;
        else if (_moveInput < 0)
            _spriteRenderer.flipX = true;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundChecker.position, _checkRadius, _ground);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyController>())
        {
            _spriteRenderer.material = _blink;

            Invoke(nameof(ResetMaterial), _blinkTime);
        }
    }

    private void ResetMaterial()
    {
        _spriteRenderer.material = _default;
    }
}
