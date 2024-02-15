using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private int HealthValue;

    public int CurrentHealthValue { get; private set; }
    public int MaxHealthValue { get; private set; }

    public event Action CurrentHealthChanged;

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

        CurrentHealthChanged?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        CurrentHealthValue -= damage;

        if (CurrentHealthValue <= 0)
            CurrentHealthValue = 0;

        CurrentHealthChanged?.Invoke();
    }
}
