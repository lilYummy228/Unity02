using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private bool _isStatic;

    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;

    private bool _isLookingRight = true;
    private readonly float _distance = 0.2f;
    private int _randomWaypoint;

    private void Start()
    {
        _randomWaypoint = Random.Range(0, _waypoints.Length);
    }

    private void Update()
    {
        if (_isStatic == false)
            Patrol();
    }

    private void Patrol()
    {
        if (_isLookingRight != true)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            transform.rotation = Quaternion.Euler(0, 0, 0);

        transform.position = Vector2.MoveTowards(transform.position, _waypoints[_randomWaypoint].position, _speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, _waypoints[_randomWaypoint].position) < _distance)
        {
            _randomWaypoint = Random.Range(0, _waypoints.Length);
        }

        if (_waypoints[_randomWaypoint].position.x > transform.position.x)
            _isLookingRight = true;
        else
            _isLookingRight = false;
    }
}
