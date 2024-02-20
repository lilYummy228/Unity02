using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AnimationPlayer))]
[RequireComponent(typeof(SpriteRenderer))]
public class HealthLevelVisualization : MonoBehaviour
{
    private readonly int IsHurt = Animator.StringToHash(nameof(IsHurt));
    private readonly int IsDead = Animator.StringToHash(nameof(IsDead));

    [SerializeField] private Material _hurtBlinkMaterial;
    [SerializeField] private Material _healBlinkMaterial;
    [SerializeField] private Health _health;

    private AnimationPlayer _animationPlayer;
    private SpriteRenderer _spriteRenderer;
    private WaitForSeconds _wait;
    private Material _defaultMaterial;
    private float _blinkTime = 0.2f;


    private void Start()
    {
        _animationPlayer = GetComponent<AnimationPlayer>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _wait = new WaitForSeconds(_blinkTime);
        _defaultMaterial = _spriteRenderer.material;
    }

    private void OnEnable()
    {
        _health.GetHealed += GetHeal;
        _health.GetHurt += GetHurt;
        _health.Died += Dead;
    }

    private void OnDisable()
    {
        _health.GetHealed -= GetHeal;
        _health.GetHurt -= GetHurt;
        _health.Died -= Dead;
    }

    private void GetHurt()
    {
        StartCoroutine(HurtBlink());
    }

    private void GetHeal()
    {
        StartCoroutine(Blink(_healBlinkMaterial));
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
    }        
}
