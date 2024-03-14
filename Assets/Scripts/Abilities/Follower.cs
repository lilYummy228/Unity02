using UnityEngine;

public class Follower: MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void Update()
    {
        if (_target != null)
            transform.position = new Vector3(_target.position.x, _target.position.y + 1.5f, _target.position.z - 10);
    }
}
