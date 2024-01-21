using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Mover _mover;

    private float _moveDirection = 1;

    public void GetDirection()
    {
        _moveDirection = -_moveDirection;
    }

    private void Update()
    {
        _mover.Move(_moveDirection);
    }
}
