using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _spawnedObject;
    [SerializeField] private float _minOffset = -6;
    [SerializeField] private float _maxOffset = 6;

    private Vector3 _initialPosition;
    private int _delay = 1;
    private int _repeatRate = 2;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), _delay, _repeatRate);
    }

    private void Spawn()
    {
        Vector3 spawnOffset = SetSpawnPoint();

        _spawnedObject.transform.position = transform.position - spawnOffset;

        Instantiate(_spawnedObject);
    }

    private Vector3 SetSpawnPoint()
    {
        float randomPositionX = Random.Range(_minOffset, _maxOffset);
        float randomPositionZ = Random.Range(_minOffset, _maxOffset);

        Vector3 spawnOffset = new Vector3(randomPositionX, transform.position.y, randomPositionZ);
        return spawnOffset;
    }    
}
