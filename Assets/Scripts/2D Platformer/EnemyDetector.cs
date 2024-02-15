using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    private const string PlayerTag = "Player";

    [SerializeField] BoxCollider2D _detectionZone;

    public Coroutine DetectorCoroutine { get; private set; }
    public Transform Player { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PlayerTag) && gameObject.TryGetComponent(out Enemy enemy))
        {
            Player = collision.gameObject.transform;
            DetectorCoroutine = StartCoroutine(enemy.DetectPlayer());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(PlayerTag))
            Player = null;
    }
}
