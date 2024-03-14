using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Vampirism : MonoBehaviour
{
    [SerializeField] private Transform _point;
    [SerializeField] private float _range = 6f;
    [SerializeField] private int _vampirismValue = 4;
    [SerializeField] private LayerMask _enemiesLayer;

    private Collider2D[] _colliders;
    private Health _health;
    private float _recharge = 8f;
    private float _nextActionTime = 0f;
    private float _actionTime = 6f;
    private WaitForSeconds _wait;

    private void Start()
    {
        _health = GetComponent<Health>();
        _wait = new WaitForSeconds(1f);
    }

    public void TryStealHealth(bool pressedKey)
    {
        if (pressedKey && Time.time >= _nextActionTime)
            StartCoroutine(StealHealth());
    }

    private IEnumerator StealHealth()
    {
        int vampirismValue = _vampirismValue;
        _nextActionTime = Time.time + _recharge;

        for (int i = 0; i < _actionTime; i++)
        {
            _colliders = Physics2D.OverlapCircleAll(_point.position, _range, _enemiesLayer);

            foreach (Collider2D collider in _colliders)
            {
                if (collider.TryGetComponent(out Enemy enemy) && enemy.Health.CurrentHealthValue > 0)
                {
                    if (enemy.Health.CurrentHealthValue < _vampirismValue)
                        vampirismValue = enemy.Health.CurrentHealthValue;

                    enemy.Health.TakeDamage(vampirismValue);
                    _health.Heal(vampirismValue);
                }

                vampirismValue = _vampirismValue;
            }

            yield return _wait;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_point.position, _range);
    }
}
