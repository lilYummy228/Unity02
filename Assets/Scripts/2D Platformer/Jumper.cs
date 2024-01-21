using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private LayerMask _ground;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody;
    private float _checkRadius = 0.5f;

    public void Jump(bool isJump)
    {
        if (IsGrounded())
            if (isJump)
                _rigidbody.velocity = Vector2.up * _jumpForce;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundChecker.position, _checkRadius, _ground);
    }
}
