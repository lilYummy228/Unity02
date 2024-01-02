using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 1;

    private void Start()
    {
        SetRotation();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime, Space.Self);
    }

    private void SetRotation()
    {
        int rotationY = Random.Range(0, 360);
        transform.rotation = Quaternion.Euler(0, rotationY, 0);
    }
}
