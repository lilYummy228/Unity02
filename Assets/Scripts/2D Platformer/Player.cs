using UnityEngine;

public class Player : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);

    [SerializeField] private Mover _mover;
    [SerializeField] private Jumper _jumper;
    [SerializeField] private DamageManagement _damageManagement;
    [SerializeField] private AnimationController _animationController;
    [SerializeField] private ScoreCounter _scoreCounter;

    private readonly int Speed = Animator.StringToHash(nameof(Speed));

    private void Update()
    {
        _animationController.Animate(Speed, _mover.Move(Input.GetAxis(Horizontal)));

        _jumper.Jump(Input.GetKeyDown(KeyCode.Space));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            _scoreCounter.AddScore();
            Destroy(other.gameObject);
        }
    }
}
