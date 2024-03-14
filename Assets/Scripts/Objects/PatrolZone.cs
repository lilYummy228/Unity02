using UnityEngine;

public class PatrolZone : MonoBehaviour
{
    private Coroutine _moveDirectionCoroutine;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
            _moveDirectionCoroutine = StartCoroutine(enemy.ChangeMoveDirection());
    }
}
