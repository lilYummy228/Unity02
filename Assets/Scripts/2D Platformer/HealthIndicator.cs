using System.Collections;
using UnityEngine;

public class HealthIndicator : Health
{
    private readonly int IsHurt = Animator.StringToHash(nameof(IsHurt));
    private readonly int IsDead = Animator.StringToHash(nameof(IsDead));

    [SerializeField] private Material _hurtBlinkMaterial;
    [SerializeField] private Material _healBlinkMaterial;

    private AnimationPlayer _animationPlayer;
    private SpriteRenderer _spriteRenderer;
    private WaitForSeconds _wait;
    private Material _defaultMaterial;
    private float _blinkTime = 0.2f;
    private float _clearTime = 5f;

    private void Start()
    {
        if (gameObject.TryGetComponent(out AnimationPlayer animationPlayer))
            _animationPlayer = animationPlayer;

        if (gameObject.TryGetComponent(out SpriteRenderer spriteRenderer))
            _spriteRenderer = spriteRenderer;

        _wait = new WaitForSeconds(_blinkTime);

        _defaultMaterial = _spriteRenderer.material;
    }

    public override void TakeDamage(int damage)
    {      
        StartCoroutine(HurtBlink());
        base.TakeDamage(damage);

        if (CurrentHealthValue <= 0)
            Dead();
    }

    public override void Heal(int healValue)
    {
        StartCoroutine(Blink(_healBlinkMaterial));
        base.Heal(healValue);
    }

    private IEnumerator Blink(Material material)
    {
        _spriteRenderer.material = material;        

        yield return _wait;

        _spriteRenderer.material = _defaultMaterial;        
    }

    private IEnumerator HurtBlink()
    {
        StartCoroutine(Blink(_hurtBlinkMaterial));

        _animationPlayer.SetHurtState(IsHurt, true);

        yield return _wait;

        _animationPlayer.SetHurtState(IsHurt, false);
    }

    private void Dead()
    {
        _animationPlayer.SetDeathState(IsDead, true);

        if (gameObject.TryGetComponent(out Enemy enemy))
            enemy.enabled = false;

        if (gameObject.TryGetComponent(out Player player))
            player.enabled = false;               

        gameObject.GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject.GetComponent<Rigidbody2D>());

        Invoke(nameof(ClearDeadBody), _clearTime);
    }

    private void ClearDeadBody()
    {
        Destroy(gameObject);
    }
}
