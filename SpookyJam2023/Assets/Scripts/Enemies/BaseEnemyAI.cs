using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class BaseEnemyAI : MonoBehaviour
{
    private ICharacter _enemyCharacter;
    protected NavMeshAgent _agent;
    protected Transform _playerTransform;

    private void Awake()
    {
        _enemyCharacter = GetComponent<ICharacter>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _agent.speed = _enemyCharacter.StatsSO.MoveSpeed;
        _agent.angularSpeed = _enemyCharacter.StatsSO.RotSpeed;
    }

    public void Init(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

    private void Update()
    {
        if (_playerTransform == null) {
            return;
        }

        Pathfinding();
        //_agent.SetDestination(_playerTransform.position);
    }

    protected abstract void Pathfinding();
}
