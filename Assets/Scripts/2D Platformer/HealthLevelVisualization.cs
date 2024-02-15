using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(AnimationPlayer))]
[RequireComponent(typeof(SpriteRenderer))]
public class HealthLevelVisualization : MonoBehaviour
{
    private readonly int IsHurt = Animator.StringToHash(nameof(IsHurt));
    private readonly int IsDead = Animator.StringToHash(nameof(IsDead));

    [SerializeField] private Material _hurtBlinkMaterial;
    [SerializeField] private Material _healBlinkMaterial;

    private Health _health;
    private AnimationPlayer _animationPlayer;
    private SpriteRenderer _spriteRenderer;
    private WaitForSeconds _wait;
    private Material _defaultMaterial;
    private float _blinkTime = 0.2f;
    private float _clearTime = 5f;

    private void Start()
    {
        _health= GetComponent<Health>();
        _animationPlayer= GetComponent<AnimationPlayer>();
        _spriteRenderer= GetComponent<SpriteRenderer>();

        _wait = new WaitForSeconds(_blinkTime);
        _defaultMaterial = _spriteRenderer.material;
    }

    private IEnumerator HurtBlink()
    {
        StartCoroutine(Blink(_hurtBlinkMaterial));

        _animationPlayer.SetHurtState(IsHurt, true);

        yield return _wait;

        _animationPlayer.SetHurtState(IsHurt, false);
    }

    private IEnumerator Blink(Material material)
    {
        _spriteRenderer.material = material;        

        yield return _wait;

        _spriteRenderer.material = _defaultMaterial;        
    }

    public void TakeDamage(int damage)
    {      
        _health.TakeDamage(damage);

        StartCoroutine(HurtBlink());

        if (_health.CurrentHealthValue <= 0)
            Dead();
    }

    public void Heal(int healValue)
    {
        _health.Heal(healValue);

        StartCoroutine(Blink(_healBlinkMaterial));
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
