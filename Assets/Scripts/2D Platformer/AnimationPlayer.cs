using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationPlayer : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetGroundedStatus(int parameter, bool isGrounded)
    {
        _animator.SetBool(parameter, isGrounded);
    }

    public void SetAttackStatus(int parameter, bool isAttack)
    {
        _animator.SetBool(parameter, isAttack);
    }

    public void SetHurtStatus(int parameter, bool isHurt)
    {
        _animator.SetBool(parameter, isHurt);
    }

    public void SetDeathStatus(int parameter, bool isDead)
    {
        _animator.SetBool(parameter, isDead);
    }

    public void SetVelocityX(int parameter, float speed)
    {
        _animator.SetFloat(parameter, speed);
    }

    public void SetVelocityY(int parameter, float velocityY)
    {
        _animator.SetFloat(parameter, velocityY);
    }
}
