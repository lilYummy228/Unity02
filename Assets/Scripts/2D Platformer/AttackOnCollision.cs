using System.Collections;
using UnityEngine;

public class AttackOnCollision : MonoBehaviour
{
    private readonly int IsAttacked = Animator.StringToHash(nameof(IsAttacked));

    [SerializeField] private int _damage = 20;

    private Coroutine _hitCoroutine;
    private WaitForSeconds _stun;
    private float _stunningTime = 0.4f;

    private void Start()
    {
        _stun = new WaitForSeconds(_stunningTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.TryGetComponent(out Health health) && collision.collider.TryGetComponent(out Player player))
            _hitCoroutine = StartCoroutine(Hit(player));
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player player) && _hitCoroutine != null && gameObject.TryGetComponent(out AnimationPlayer animationController))
        {
            StopCoroutine(_hitCoroutine);
            animationController.SetAttackState(IsAttacked, false);
        }
    }

    private IEnumerator Hit(Player player)
    {
        while (player.enabled && gameObject.TryGetComponent(out AnimationPlayer animationController))
        {
            animationController.SetAttackState(IsAttacked, true);

            player.HealthLevelVisualization.TakeDamage(_damage);

            yield return _stun;

            animationController.SetAttackState(IsAttacked, false);

            yield return _stun;
        }

        if (_hitCoroutine != null)
            StopCoroutine(_hitCoroutine);
    }
}
