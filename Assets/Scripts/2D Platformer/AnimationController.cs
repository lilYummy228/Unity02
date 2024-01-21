using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    public Animator Animator;

    public void Animate(int parameter, float speed)
    {
        Animator.SetFloat(parameter, speed);
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }
}
