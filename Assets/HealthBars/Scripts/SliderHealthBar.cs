using UnityEngine.UI;

public class SliderHealthBar : HealthBar
{
    private Slider _healthSlider;

    private void Start()
    {
        _healthSlider = GetComponent<Slider>();
        _healthSlider.value = Health.CurrentHealthValue;
    }

    public override void ChangeValue()
    {
        _healthSlider.value = Health.CurrentHealthValue;
    }
}
