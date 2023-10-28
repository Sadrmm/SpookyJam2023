using UnityEngine;

public class EnemyRunAwayAI : BaseEnemyAI
{
    [SerializeField] float _distanceToPlayer = 15f;

    protected override void Pathfinding()
    {
        Vector3 directionToPlayer = _playerTransform.position - transform.position;

        Vector3 runAway = transform.position - directionToPlayer.normalized * _distanceToPlayer;

        _agent.SetDestination(runAway);
    }
}
