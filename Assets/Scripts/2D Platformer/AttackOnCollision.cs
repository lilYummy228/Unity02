using System.Collections;
using UnityEngine;

public class AttackOnCollision : MonoBehaviour
{
    private readonly int IsAttacked = Animator.StringToHash(nameof(IsAttacked));

    [SerializeField] private AnimationPlayer _animationPlayer;
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
        if (collision.collider.TryGetComponent(out Player player))
            _hitCoroutine = StartCoroutine(Hit(player));
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player player) && _hitCoroutine != null)
        {
            StopCoroutine(_hitCoroutine);
            _animationPlayer.SetAttackStatus(IsAttacked, false);
        }
    }

    private IEnumerator Hit(Player player)
    {
        while (player.enabled)
        {
            _animationPlayer.SetAttackStatus(IsAttacked, true);

            player.Health.TakeDamage(_damage);

            yield return _stun;

            _animationPlayer.SetAttackStatus(IsAttacked, false);

            yield return _stun;
        }

        if (_hitCoroutine != null)
            StopCoroutine(_hitCoroutine);
    }
}
