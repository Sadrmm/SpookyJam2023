public class EnemyToPlayerAI : BaseEnemyAI
{
    protected override void Pathfinding()
    {
        _agent.SetDestination(_playerTransform.position);
    }
}
