using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Transform _creature;

    private Vector2 _position;
    private Quaternion _rotation;

    public void Teleport(GameObject creature)
    {
        creature.transform.position = _position;
    }

    public void Spawn()
    {
        if (_creature != null)
            Instantiate(_creature, _position, _rotation);
    }

    private void Start()
    {
        _position = transform.position;

        Spawn();
    }    
}
