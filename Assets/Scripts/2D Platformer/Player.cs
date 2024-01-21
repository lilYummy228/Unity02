using UnityEngine;

public class Player : MonoBehaviour
{
    private const string Horizontal = "Horizontal";

    [SerializeField] private Mover _mover;
    [SerializeField] private Jumper _jumper;
    [SerializeField] private DamageManagement _damageManagement;
    [SerializeField] private AnimationController _animationController;
    [SerializeField] private ScoreCounter _scoreCounter;

    private string _runTrigger = "IsMoving";

    private void Update()
    {
        _animationController.Animate(_runTrigger, _mover.Move(Input.GetAxis(Horizontal)));

        _jumper.Jump(Input.GetKeyDown(KeyCode.Space));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Coin>())
        {
            _scoreCounter.AddScore();
            Destroy(other.gameObject);
        }
    }
}
