using UnityEngine;
using System.Collections;

public class Attacker : MonoBehaviour
{
    private readonly int IsAttacked = Animator.StringToHash(nameof(IsAttacked));

    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private int _damage;

    private Coroutine _hitCoroutine;
    private WaitForSeconds _stun;
    private float _stunningTime = 0.4f;
    private float _attackRate = .5f;
    private float _nextAttackTime = 0f;
    private bool _isAttack;

    private void Start()
    {
        _stun = new WaitForSeconds(_stunningTime);
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;

        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

    public bool IsAttack(bool isAttack)
    {
        _isAttack = isAttack;

        RechargeAttack();

        return _isAttack;
    }

    private void RechargeAttack()
    {
        if (Time.time >= _nextAttackTime)
        {
            if (_isAttack)
            {
                WeaponAttack();
                _nextAttackTime = Time.time + _attackRate;
            }
        }
    }

    private void WeaponAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayer);

        foreach (Collider2D collider in hitEnemies)
        {
            if (collider.TryGetComponent(out HealthIndicator damageController))
            {
                damageController.TakeDamage(_damage);

                if (collider.TryGetComponent(out Enemy enemy))
                    StartCoroutine(enemy.Stun());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.TryGetComponent(out HealthIndicator health) && collision.collider.TryGetComponent(out Player player))
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

            player.DamageController.TakeDamage(_damage);

            yield return _stun;

            animationController.SetAttackState(IsAttacked, false);

            yield return _stun;
        }

        if (_hitCoroutine != null)
            StopCoroutine(_hitCoroutine);
    }
}
