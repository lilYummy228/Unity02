using UnityEngine;

public class AttackOnPressedKey : MonoBehaviour
{
    private readonly int IsAttacked = Animator.StringToHash(nameof(IsAttacked));

    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange = 1f;
    [SerializeField] private LayerMask _enemiesLayer;
    [SerializeField] private int _damage = 20;

    private float _attackRate = 0.5f;
    private float _nextAttackTime = 0f;

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;

        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

    public bool TryHit(bool pressedKey)
    {
        if (Time.time >= _nextAttackTime && pressedKey)
        {
            Hit();

            return true;
        }

        return false;
    }

    private void Hit()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemiesLayer);

        foreach (Collider2D collider in hitEnemies)
        {
            if (collider.TryGetComponent(out Health health))
            {
                health.TakeDamage(_damage);

                if (collider.TryGetComponent(out Enemy enemy))
                    StartCoroutine(enemy.Stun());
            }
        }

        _nextAttackTime = Time.time + _attackRate;
    }
}
