using System;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    public event Action RotationChanged;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        if (gameObject.TryGetComponent(out Rigidbody2D rigidbody))
            _rigidbody = rigidbody;
    }

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

    private void FlipToSight(float moveDirection)
    {
        if (moveDirection > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (moveDirection < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);

        RotationChanged?.Invoke();
    }
}
