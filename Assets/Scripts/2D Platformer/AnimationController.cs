using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;

    public void Animate(string triggerName, bool isMoving)
    {
        _animator.SetBool(triggerName, isMoving);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
}
