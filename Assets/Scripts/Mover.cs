using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 1;

    private Vector3 _direction = Vector3.forward;

    private void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime, Space.Self);
    }

    public void SetDirection(int rotationAngle)
    {
        transform.rotation = Quaternion.Euler(0, rotationAngle, 0);
    }
}
