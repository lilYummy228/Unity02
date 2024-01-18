using UnityEngine;

public class Coin : MonoBehaviour
{
    private float _timeToDisappear = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            transform.Translate(Vector2.up);
            Invoke(nameof(TakeCoin), _timeToDisappear);
        }
    }

    private void TakeCoin()
    {
        Destroy(gameObject);
    }
}
