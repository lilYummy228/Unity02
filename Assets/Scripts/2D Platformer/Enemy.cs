using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(AnimationPlayer))]
[RequireComponent(typeof(EnemyDetector))]
public class Enemy : MonoBehaviour
{
    private readonly int VelocityX = Animator.StringToHash(nameof(VelocityX));

    public HealthIndicator DamageController { get; private set; }

    private EnemyDetector _playerDetector;
    private Attacker _attacker;
    private WaitForSeconds _wait;
    private WaitForSecondsRealtime _stun;
    private Mover _mover;
    private AnimationPlayer _animationController;
    private float _moveDirection = 1f;
    private float _waitingTime = 2f;
    private float _stunningTime = 0.4f;

    private void Start()
    {
        _wait = new WaitForSeconds(_waitingTime);
        _stun = new WaitForSecondsRealtime(_stunningTime);
        DamageController = GetComponent<HealthIndicator>();
        _attacker= GetComponent<Attacker>();
        _mover = GetComponent<Mover>();
        _animationController = GetComponent<AnimationPlayer>();
        _playerDetector = GetComponent<EnemyDetector>();
    }    

    private void Update()
    {
        _animationController.SetVelocityX(VelocityX, _mover.Move(_moveDirection));
    }

    public IEnumerator DetectPlayer()
    {
        if (_playerDetector.Player != null)
        {
            _moveDirection = -(transform.position.x - _playerDetector.Player.transform.position.x);

            if (_moveDirection > 0)
                _moveDirection = 1;
            else
                _moveDirection = -1;
        }

        yield return null;        
    }     

    public IEnumerator Stun()
    {
        if (DetectPlayer() != null)
            StopCoroutine(DetectPlayer());

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
}
