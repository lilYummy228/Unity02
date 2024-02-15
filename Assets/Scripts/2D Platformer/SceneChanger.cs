using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private WaitForSeconds _wait;
    private float _waitingTime = 0.5f;

    private void Start()
    {
        _wait = new WaitForSeconds(_waitingTime);
    }

    private IEnumerator StartScene(int scene)
    {
        yield return _wait;
        SceneManager.LoadScene(scene);
    }

    public void ChangeScene(int scene)
    {
        StartCoroutine(StartScene(scene));
    }
}
