using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimatorManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyGO;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PostDeathAnimation()
    {
        Destroy(_enemyGO);
    }
}
