using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorManager : MonoBehaviour
{
    private const string ATTACKING = "Attacking";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void AttackAnimationEnded()
    {
        _animator.SetBool(ATTACKING, false);
    }
}
