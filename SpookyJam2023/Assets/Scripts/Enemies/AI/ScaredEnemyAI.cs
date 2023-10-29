using UnityEngine;

public class ScaredEnemyAI : EnemyRunAwayAI
{
    [SerializeField] float _multiplierSpeed = 1.5f;

    private Vector3 _runAwayOrigin;

    private bool _isScared;

    private void OnEnable()
    {
        _isScared = true;
    }

    private void OnDisable()
    {
        _isScared = false;
    }

    protected override void Start()
    {
        base.Start();
        _agent.speed *= _multiplierSpeed;
        _runAwayOrigin = _playerTransform.position;
    }

    protected override void Pathfinding()
    {
        if (_agent.remainingDistance < _agent.stoppingDistance) {
            RunAwayFrom(GetRandomPoint());
        }
        RunAwayFrom(_runAwayOrigin);
    }

    private Vector3 GetRandomPoint() {
        float angle = Random.Range(0, 360f);
        float radius = _distanceToRun;

        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        return new Vector3(x, 0f, z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isScared) {
            return;
        }

        IScareable scareable = collision.gameObject.GetComponent<IScareable>();

        if (scareable == null) {
            return;
        }

        scareable.BeScared();
        _runAwayOrigin = collision.transform.position;
    }
}
