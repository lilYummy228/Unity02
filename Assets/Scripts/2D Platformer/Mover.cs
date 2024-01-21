using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;

    public float Move(float moveDirection)
    {
        _rigidbody.velocity = new Vector2(moveDirection * _moveSpeed, _rigidbody.velocity.y);

        if (moveDirection != 0)
        {
            FlipToSight(moveDirection);

            if (moveDirection < 0)
                moveDirection = -moveDirection;
        }

        return moveDirection;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FlipToSight(float moveDirection)
    {
        if (moveDirection > 0)
            _spriteRenderer.flipX = false;
        else if (moveDirection < 0)
            _spriteRenderer.flipX = true;
    }
}
