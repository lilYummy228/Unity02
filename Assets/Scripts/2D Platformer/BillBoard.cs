using UnityEngine;

public class BillBoard : MonoBehaviour
{
    [SerializeField] private Mover _mover;

    private void OnEnable()
    {
        _mover.RotationChanged += ChangeRotationSide;
    }
    private void OnDisable()
    {
        _mover.RotationChanged -= ChangeRotationSide;
    }

    private void ChangeRotationSide()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
