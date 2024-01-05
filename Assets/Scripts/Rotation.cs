using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float _spinSpeed = 1;

    void Update()
    {
        transform.Rotate(0, _spinSpeed, 0);
    }
}
