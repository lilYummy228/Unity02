using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private PlayerController _player;

    private Vector2 _position;
    private Quaternion _rotation;

    private void Start()
    {
        _position = transform.position;

        Spawn();
    }

    public void Spawn()
    {
        if (_player != null)
            Instantiate(_player, _position, _rotation);
    }
}
