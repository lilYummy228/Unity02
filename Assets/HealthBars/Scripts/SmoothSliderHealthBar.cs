using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothSliderHealthBar : HealthBar
{
    [SerializeField] private float _reduceSpeed = 10f;

    private Slider _healthSlider;

    private void Start()
    {
        _healthSlider = GetComponent<Slider>();
        _healthSlider.value = Health.CurrentHealthValue;
    }

    public override void ChangeValue()
    {
        StartCoroutine(ChangeHealth());
    }

    private IEnumerator ChangeHealth()
    {
        while (_healthSlider.value != Health.CurrentHealthValue)
        {
            _healthSlider.value = Mathf.MoveTowards(_healthSlider.value, Health.CurrentHealthValue, _reduceSpeed * Time.deltaTime);
            yield return new WaitWhile(() => _healthSlider.value == Health.CurrentHealthValue);
        }
    }
}
