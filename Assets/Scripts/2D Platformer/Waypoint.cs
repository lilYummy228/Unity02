using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
            collision.gameObject.GetComponent<Enemy>().GetDirection();
    }
}
