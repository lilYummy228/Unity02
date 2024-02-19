using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AnimationPlayer))]
[RequireComponent(typeof(SpriteRenderer))]
public class HealthLevelVisualization : MonoBehaviour
{
    private readonly int IsHurt = Animator.StringToHash(nameof(IsHurt));
    private readonly int IsDead = Animator.StringToHash(nameof(IsDead));

    [SerializeField] private GameObject _gameObject;
    [SerializeField] private Material _hurtBlinkMaterial;
    [SerializeField] private Material _healBlinkMaterial;
    [SerializeField] private Health _health;

    private AnimationPlayer _animationPlayer;
    private SpriteRenderer _spriteRenderer;
    private WaitForSeconds _wait;
    private Material _defaultMaterial;
    private float _blinkTime = 0.2f;
    private float _clearTime = 5f;
    private float _currentHealthValue;

    private void Start()
    {
        _animationPlayer = GetComponent<AnimationPlayer>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _wait = new WaitForSeconds(_blinkTime);
        _defaultMaterial = _spriteRenderer.material;

        _currentHealthValue = _health.CurrentHealthValue;
    }

    private void OnEnable()
    {
        _health.CurrentHealthChanged += UpdateHealthLevel;
    }

    private void OnDisable()
    {
        _health.CurrentHealthChanged -= UpdateHealthLevel;
    }

    private void UpdateHealthLevel()
    {
        if (_currentHealthValue <= _health.CurrentHealthValue)
        {
            StartCoroutine(Blink(_healBlinkMaterial));
            _currentHealthValue = _health.CurrentHealthValue;
        }
        else if (_currentHealthValue >= _health.CurrentHealthValue)
        {
            StartCoroutine(HurtBlink());

            if (_health.CurrentHealthValue <= 0)
                Dead();

            _currentHealthValue = _health.CurrentHealthValue;
        }
    }

    private IEnumerator HurtBlink()
    {
        StartCoroutine(Blink(_hurtBlinkMaterial));

        _animationPlayer.SetHurtStatus(IsHurt, true);

        yield return _wait;

        _animationPlayer.SetHurtStatus(IsHurt, false);
    }

    private IEnumerator Blink(Material material)
    {
        _spriteRenderer.material = material;

        yield return _wait;

        _spriteRenderer.material = _defaultMaterial;
    }

    private void Dead()
    {
        _animationPlayer.SetDeathStatus(IsDead, true);

        if (_gameObject.TryGetComponent(out Enemy enemy))
            enemy.enabled = false;
        else if (_gameObject.TryGetComponent(out Player player))
            player.enabled = false;

        _gameObject.GetComponent<Collider2D>().enabled = false;
        Destroy(_gameObject.GetComponent<Rigidbody2D>());

        Invoke(nameof(ClearDeadBody), _clearTime);
    }

    private void ClearDeadBody()
    {
        Destroy(_gameObject);
    }
}
