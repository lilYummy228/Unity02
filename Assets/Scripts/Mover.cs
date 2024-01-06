using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 0.2f;
    [SerializeField] private Transform _target;

    private void Update()
    {
        transform.LookAt(_target);
        transform.position = Vector3.Lerp(transform.position, _target.position, _speed * Time.deltaTime);
    }

    public void SetDirection(Transform target)
    {
        _target = target;        
    }
}
