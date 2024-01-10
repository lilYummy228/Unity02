using System.Collections;
using UnityEngine;

public class AlertSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _alert;   

    public IEnumerator FadeSound(bool isEntered)
    {
        float timeToFade = 1.25f;
        float timeElapsed = 0;

        if (isEntered)
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