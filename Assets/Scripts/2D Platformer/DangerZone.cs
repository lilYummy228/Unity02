using UnityEngine;

public class DangerZone : MonoBehaviour
{
    [SerializeField] private SpawnPoint _spawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            _spawnPoint.Teleport(collision.gameObject);
        }
    }
}
