using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private AlertSystem _alertSystem;

    private bool _isEntered = false;

    private void OnTriggerEnter(Collider other)
    {
        _isEntered = true;
        StopCoroutine(_alertSystem.FadeSound(_isEntered));

        if (other.GetComponent<Rogue>())
            StartCoroutine(_alertSystem.FadeSound(_isEntered));
    }

    private void OnTriggerExit(Collider other)
    {
        _isEntered = false;
        StopCoroutine(_alertSystem.FadeSound(_isEntered));

        if (other.GetComponent<Rogue>())
            StartCoroutine(_alertSystem.FadeSound(_isEntered));
    }
}
