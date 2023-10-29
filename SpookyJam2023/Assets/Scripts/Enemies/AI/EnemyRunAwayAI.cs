using UnityEngine;

public class EnemyRunAwayAI : BaseEnemyAI
{
    [SerializeField] protected float _distanceToRun = 15f;

    protected override void Pathfinding()
    {
        Vector3 runAway = RunAwayFrom(_playerTransform.position);

        _agent.SetDestination(runAway);
    }

    protected Vector3 RunAwayFrom(Vector3 from)
    {
        Vector3 dirToFrom = from - transform.position;

        Vector3 runAway = transform.position - dirToFrom.normalized * _distanceToRun;

        return runAway;
    }
}
