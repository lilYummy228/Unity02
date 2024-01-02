using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float _speed = 1;

    void Update()
    {
        transform.Rotate(0, _speed, 0);
    }
}
