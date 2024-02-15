using UnityEngine;

public class AttackOnPressedKey : MonoBehaviour
{
    private readonly int IsAttacked = Animator.StringToHash(nameof(IsAttacked));

    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange = 1f;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private int _damage = 20;

    private WaitForSeconds _stun;
    private float _stunningTime = 0.4f;
    private float _attackRate = 0.5f;
    private float _nextAttackTime = 0f;

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

    public bool IsHit()
    {
        if (Time.time >= _nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Hit();

                return true;
            }
        }

        return false;
    }

    private void Hit()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayer);

        foreach (Collider2D collider in hitEnemies)
        {
            if (collider.TryGetComponent(out HealthLevelVisualization damageController))
            {
                damageController.TakeDamage(_damage);

                if (collider.TryGetComponent(out Enemy enemy))
                    StartCoroutine(enemy.Stun());
            }
        }

        _nextAttackTime = Time.time + _attackRate;
    }
}
