using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>())
            collision.gameObject.GetComponent<Enemy>().GetDirection();
    }
}
