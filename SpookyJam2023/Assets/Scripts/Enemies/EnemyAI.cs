using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    private ICharacter _enemyCharacter;
    private NavMeshAgent _agent;
    private Transform _playerTransform;

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
        Debug.Log("inicializado");
    }

    private void Update()
    {
        if (_playerTransform == null) {
            return;
        }

        _agent.SetDestination(_playerTransform.position);
    }
}
