using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health), typeof(Mover), typeof(AttackOnCollision))]
[RequireComponent(typeof(EnemyDetector))]
public class Enemy : MonoBehaviour
{
    private readonly int VelocityX = Animator.StringToHash(nameof(VelocityX));

    [SerializeField] private AnimationPlayer _animationPlayer;

    private EnemyDetector _detector;
    private AttackOnCollision _attack;
    private WaitForSeconds _wait;
    private WaitForSecondsRealtime _stun;
    private Mover _mover;
    private float _moveDirection = 1f;
    private float _waitingTime = 2f;
    private float _stunningTime = 0.4f;

    public Health Health { get; private set; }

    private void Start()
    {
        Health = GetComponent<Health>();
        _attack = GetComponent<AttackOnCollision>();
        _mover = GetComponent<Mover>();
        _detector = GetComponent<EnemyDetector>();

        _wait = new WaitForSeconds(_waitingTime);
        _stun = new WaitForSecondsRealtime(_stunningTime);
    }

    private void Update()
    {
        _animationPlayer.SetVelocityX(VelocityX, _mover.Move(_moveDirection));
    }

    public IEnumerator Stun()
    {
        float moveDirection = _moveDirection;
        _moveDirection = 0;

        yield return _stun;

        _moveDirection = moveDirection;
    }

    public IEnumerator ChangeMoveDirection()
    {
        float moveDirection = _moveDirection;
        _moveDirection = 0f;

        yield return _wait;

        _moveDirection = -moveDirection;
    }

    public void DetectPlayer()
    {
        if (_detector.Player != null)
        {
            _moveDirection = -(transform.position.x - _detector.Player.transform.position.x);

            if (_moveDirection > 0)
                _moveDirection = 1;
            else
                _moveDirection = -1;
        }
    }
}
