using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DeathZone : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private SpawnPoint _spawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            Destroy(collision.gameObject);
            _spawnPoint.Spawn();
        }
    }
}
