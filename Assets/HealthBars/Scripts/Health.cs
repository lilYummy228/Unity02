using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action GetHealed;
    public event Action GetHurt;
    public event Action Died;

    [SerializeField, Range(0, 100)] private int HealthValue;

    private float _clearTime = 3f;

    public int CurrentHealthValue { get; private set; }
    public int MaxHealthValue { get; private set; }

    private void Awake()
    {
        CurrentHealthValue = HealthValue;
        MaxHealthValue = CurrentHealthValue;
    }

    public void Heal(int healValue)
    {
        CurrentHealthValue += healValue;

        if (CurrentHealthValue > MaxHealthValue)
            CurrentHealthValue = MaxHealthValue;

        GetHealed?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        CurrentHealthValue -= damage;

        if (CurrentHealthValue <= 0)
            CurrentHealthValue = 0;

        GetHurt?.Invoke();

        if (CurrentHealthValue <= 0)
            Dead();
    }

    private void Dead()
    {
        if (gameObject.TryGetComponent(out Enemy enemy))
            enemy.enabled = false;
        else if (gameObject.TryGetComponent(out Player player))
            player.enabled = false;

        gameObject.GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject.GetComponent<Rigidbody2D>());

        Invoke(nameof(ClearDeadBody), _clearTime);

        Died?.Invoke();
    }

    private void ClearDeadBody()
    {
        Destroy(gameObject);
    }
}
