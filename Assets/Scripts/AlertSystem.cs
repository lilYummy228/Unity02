using System.Collections;
using UnityEngine;

public class AlertSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _alert;

    private bool _isEntered = false;

    private void OnTriggerEnter(Collider other)
    {
        StopCoroutine(FadeSound());

        if (other.GetComponent<Rogue>())
        {
            _isEntered = true;

            StartCoroutine(FadeSound());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(FadeSound());

        if (other.GetComponent<Rogue>())
        {
            _isEntered = false;

            StartCoroutine(FadeSound());
        }
    }

    private IEnumerator FadeSound()
    {
        float timeToFade = 1.25f;
        float timeElapsed = 0;

        if (_isEntered)
        {
            _alert.Play();

            while (timeElapsed < timeToFade)
            {
                _alert.volume = Mathf.MoveTowards(0, 1, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            while (timeElapsed < timeToFade)
            {
                _alert.volume = Mathf.MoveTowards(1, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            _alert.Stop();
        }
    }
}