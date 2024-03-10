using System.Collections;
using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Vampirism : MonoBehaviour
{
    [SerializeField] private Transform _point;
    [SerializeField] private float _range = 6f;
    [SerializeField] private int _healthValue = 3;
    [SerializeField] private LayerMask _enemiesLayer;

    public Collider2D[] _colliders;

    private Health _health;
    private Coroutine _coroutine;
    private float _recharge = 8f;
    private float _nextActionTime = 0f;
    private float _actionTime = 6f;
    private WaitForSeconds _wait;

    private void Start()
    {
        _health = GetComponent<Health>();
        _wait = new WaitForSeconds(0.5f);
    }

    public void TryStealHealth(bool pressedKey)
    {
        _colliders = Physics2D.OverlapCircleAll(_point.position, _range, _enemiesLayer);

        if (pressedKey && Time.time >= _nextActionTime && _coroutine == null && _colliders.Length > 0)
            _coroutine = StartCoroutine(StealHealth());
    }

    private IEnumerator StealHealth()
    {
        int healthValue = _healthValue;
        _nextActionTime = Time.time + _recharge;

        for (int i = 0; i < _actionTime * 2; i++)
        {
            if (_colliders.Length > 0)
            {
                foreach (Collider2D collider in _colliders)
                {
                    if (collider.TryGetComponent(out Enemy enemy) && enemy.Health.CurrentHealthValue > 0)
                    {
                        if (enemy.Health.CurrentHealthValue < _healthValue)
                            healthValue = enemy.Health.CurrentHealthValue;

                        enemy.Health.TakeDamage(healthValue);
                        _health.Heal(healthValue);

                    }
                }
            }
            else
                break;

            yield return _wait;
        }

        _coroutine = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_point.position, _range);
    }
}
