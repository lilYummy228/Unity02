using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField, Range(0, 360)] private int _rotationAngle = 180;

    private float _minOffset = -6;
    private float _maxOffset = 6;
    private int _enemyCount = 20;
    private float _delay = 2;

    private void Start()
    {
        StartCoroutine(SpawnInTime(_delay, _enemyCount));
    }

    private IEnumerator SpawnInTime(float delay, int count)
    {
        var wait = new WaitForSeconds(delay);

        for (int i = 0; i < count; i++)
        {
            Spawn();
            yield return wait;
        }
    }

    private void Spawn()
    {
        Vector3 spawnOffset = SetSpawnPoint();

        _mover.transform.position = transform.position - spawnOffset;
        _mover.SetDirection(_rotationAngle);

        Instantiate(_mover);
    }

    private Vector3 SetSpawnPoint()
    {
        float randomPositionX = Random.Range(_minOffset, _maxOffset);
        float randomPositionZ = Random.Range(_minOffset, _maxOffset);

        Vector3 spawnOffset = new Vector3(randomPositionX, transform.position.y, randomPositionZ);
        return spawnOffset;
    }
}
