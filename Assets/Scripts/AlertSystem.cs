using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class AlertSystem : MonoBehaviour
{
    private const int MinVolume = 0;
    private const int MaxVolume = 1;

    [SerializeField] private AudioSource _alert;
    [SerializeField] private BoxCollider _alertZone;

    private bool _isEntered = false;
    private IEnumerator _fadeSound;

    private void OnTriggerEnter(Collider other)
    {
        _isEntered = true;

        ReplayCoroutine(other);
    }

    private void OnTriggerExit(Collider other)
    {
        _isEntered = false;

        ReplayCoroutine(other);
    }

    private IEnumerator FadeSound()
    {
        if (_isEntered)
        {
            _alert.Play();

            yield return FadeSound(MinVolume, MaxVolume);            
        }
        else
        {
            yield return FadeSound(MaxVolume, MinVolume);

            _alert.Stop();
        }
    }

    private IEnumerator FadeSound(int initialVolume, int finalVolume)
    {
        float timeToFade = 1.5f;
        float timeElapsed = 0;

        while (timeElapsed < timeToFade)
        {
            _alert.volume = Mathf.MoveTowards(initialVolume, finalVolume, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    private void ReplayCoroutine(Collider other)
    {
        if (_fadeSound != null)
            StopCoroutine(_fadeSound);

        _fadeSound = FadeSound();

        if (other.GetComponent<Rogue>())
            StartCoroutine(_fadeSound);
    }
}