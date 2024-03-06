using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Lifesteal : MonoBehaviour
{
    [SerializeField] private Transform _lifestealPoint;
    [SerializeField] private float _range = 3f;
    [SerializeField] private int _lifestealValue = 3;

    private Health _health;
    private Collider2D[] _colliders;
    private Coroutine _coroutine;
    private float _lifestealRate = 10f;
    private float _nextLifestealTime = 0f;
    private float _lifestealTime = 12f;
    private WaitForSeconds _wait;

    private void Start()
    {
        _health = GetComponent<Health>();
        _wait = new WaitForSeconds(0.5f);
    }        

    public void TryStealHealth()
    {
        _colliders = Physics2D.OverlapCircleAll(_lifestealPoint.position, _range);

        foreach (Collider2D collider in _colliders)
            if (Time.time >= _nextLifestealTime && Input.GetKeyDown(KeyCode.F) && collider.TryGetComponent(out Enemy enemy) && _coroutine == null)
                _coroutine = StartCoroutine(StealHealth(enemy));
    }

    private IEnumerator StealHealth(Enemy enemy)
    {
        for (int i = 0; i < _lifestealTime; i++)
        {
            if (Array.Find(_colliders, enemyInRange => enemyInRange.TryGetComponent(out enemy)))
            {
                if (enemy.TryGetComponent(out Health health) && health.CurrentHealthValue > 0)
                {
                    health.TakeDamage(_lifestealValue);
                    _health.Heal(_lifestealValue);

                    yield return _wait;
                }
            }
            else
            {
                break;
            }
        }

        _nextLifestealTime = Time.time + _lifestealRate;
        _coroutine = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_lifestealPoint.position, _range);
    }
}
