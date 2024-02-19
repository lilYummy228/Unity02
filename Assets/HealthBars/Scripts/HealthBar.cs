using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;

    public Health Health { get; private set; }

    private void Awake()
    {
        if(_gameObject.TryGetComponent(out Health health))
            Health = health;
    }

    private void OnEnable()
    {
        Health.CurrentHealthChanged += ChangeValue;
    }

    private void OnDisable()
    {
        Health.CurrentHealthChanged -= ChangeValue;
    }

    public virtual void ChangeValue() { }
}
