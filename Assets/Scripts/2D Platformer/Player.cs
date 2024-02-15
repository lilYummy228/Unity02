using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(HealthLevelVisualization))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(AttackOnPressedKey))]
[RequireComponent(typeof(AnimationPlayer))]
public class Player : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string ScoreCounterTag = "ScoreCounter";

    public HealthLevelVisualization HealthLevelVisualization { get; private set; }

    private readonly int VelocityX = Animator.StringToHash(nameof(VelocityX));
    private readonly int VelocityY = Animator.StringToHash(nameof(VelocityY));
    private readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
    private readonly int IsAttacked = Animator.StringToHash(nameof(IsAttacked));

    private Health _health;
    private Mover _mover;
    private Jumper _jumper;
    private AttackOnPressedKey _attack;
    private AnimationPlayer _animationController;
    private ScoreCounter _scoreCounter;

    private void Start()
    {
        HealthLevelVisualization = GetComponent<HealthLevelVisualization>();
        _health = GetComponent<Health>();
        _mover = GetComponent<Mover>();
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _attack = GetComponent<AttackOnPressedKey>();
        _animationController = GetComponent<AnimationPlayer>();

        if (GameObject.FindWithTag(ScoreCounterTag).TryGetComponent(out ScoreCounter scoreCounter))
            _scoreCounter = scoreCounter;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Coin coin))
        {
            _scoreCounter.AddScore();
            Destroy(coin.gameObject);
        }

        if (collider.TryGetComponent(out Heart heart))
        {
            if (_health.CurrentHealthValue < _health.MaxHealthValue)
            {
                HealthLevelVisualization.Heal(heart.HealValue);
                Destroy(heart.gameObject);
            }
        }
    }

    private void Update()
    {
        _animationController.SetVelocityX(VelocityX, _mover.Move(Input.GetAxis(Horizontal)));
        _animationController.SetVelocityY(VelocityY, _jumper.Rigidbody.velocity.y);
        _animationController.SetGroundedState(IsGrounded, _jumper.Jump(Input.GetKeyDown(KeyCode.Space)));
        _animationController.SetAttackState(IsAttacked, _attack.IsHit());
    }    
}
