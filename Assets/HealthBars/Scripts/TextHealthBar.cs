using TMPro;

public class TextHealthBar : HealthBar
{
    private TextMeshProUGUI _textHealthValue;

    private void Start()
    {
        _textHealthValue = GetComponent<TextMeshProUGUI>();
        _textHealthValue.text = $"{Health.CurrentHealthValue}/{Health.MaxHealthValue}";
    }

    public override void ChangeValue()
    {
        _textHealthValue.text = $"{Health.CurrentHealthValue}/{Health.MaxHealthValue}";
    }
}
