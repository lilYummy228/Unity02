using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(AnimationPlayer))]
public class Player : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string ScoreCounterTag = "ScoreCounter";

    public HealthIndicator DamageController { get; private set; }

    private readonly int VelocityX = Animator.StringToHash(nameof(VelocityX));
    private readonly int VelocityY = Animator.StringToHash(nameof(VelocityY));
    private readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
    private readonly int IsAttacked = Animator.StringToHash(nameof(IsAttacked));

    private Mover _mover;
    private Jumper _jumper;
    private Attacker _attacker;
    private AnimationPlayer _animationController;
    private ScoreCounter _scoreCounter;

    private void Start()
    {
        DamageController = GetComponent<HealthIndicator>();
        _mover = GetComponent<Mover>();
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _attacker = GetComponent<Attacker>();
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
            if (DamageController.CurrentHealthValue < DamageController.MaxHealthValue)
            {
                DamageController.Heal(heart.HealValue);
                Destroy(heart.gameObject);
            }
        }
    }

    private void Update()
    {
        _animationController.SetVelocityX(VelocityX, _mover.Move(Input.GetAxis(Horizontal)));
        _animationController.SetVelocityY(VelocityY, _jumper.Rigidbody.velocity.y);
        _animationController.SetGroundedState(IsGrounded, _jumper.Jump(Input.GetKeyDown(KeyCode.Space)));
        _animationController.SetAttackState(IsAttacked, _attacker.IsAttack(Input.GetKeyDown(KeyCode.E)));
    }    
}
