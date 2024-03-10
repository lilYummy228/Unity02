using UnityEngine;

[RequireComponent(typeof(Health), typeof(Mover), typeof(Jumper))]
[RequireComponent(typeof(AttackOnPressedKey))]
public class Player : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string ScoreCounterTag = "ScoreCounter";

    private readonly int VelocityX = Animator.StringToHash(nameof(VelocityX));
    private readonly int VelocityY = Animator.StringToHash(nameof(VelocityY));
    private readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
    private readonly int IsAttacked = Animator.StringToHash(nameof(IsAttacked));

    [SerializeField] private AnimationPlayer _animationPlayer;

    private Mover _mover;
    private Jumper _jumper;
    private AttackOnPressedKey _attack;
    private ScoreCounter _scoreCounter;
    private Vampirism _vampirism;

    public Health Health { get; private set; }

    private void Start()
    {
        Health = GetComponent<Health>();
        _mover = GetComponent<Mover>();
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _attack = GetComponent<AttackOnPressedKey>();
        _vampirism = GetComponent<Vampirism>();

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
            if (Health.CurrentHealthValue < Health.MaxHealthValue)
            {
                Health.Heal(heart.HealValue);
                Destroy(heart.gameObject);
            }
        }
    }

    private void Update()
    {
        _animationPlayer.SetVelocityX(VelocityX, _mover.Move(Input.GetAxis(Horizontal)));
        _animationPlayer.SetVelocityY(VelocityY, _jumper.Rigidbody.velocity.y);
        _animationPlayer.SetGroundedStatus(IsGrounded, _jumper.Jump(Input.GetKeyDown(KeyCode.Space)));
        _animationPlayer.SetAttackStatus(IsAttacked, _attack.TryHit(Input.GetKeyDown(KeyCode.E)));
        _vampirism.TryStealHealth(Input.GetKeyDown(KeyCode.F));
    }
}
