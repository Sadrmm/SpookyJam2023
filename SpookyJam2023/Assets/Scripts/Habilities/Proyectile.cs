using UnityEngine;
using DG.Tweening;

public class Proyectile : MonoBehaviour, IHability
{
    [SerializeField] float _speed = 1.0f;
    [SerializeField] GameObject _particlePrefab;
    private HabilityStatsSO _statsSO;
    public HabilityStatsSO StatsSO => _statsSO;

    [SerializeField]
    private AudioComponent m_AudioAttackComponent;

    private Transform _target;

    private void Start()
    {
        m_AudioAttackComponent.PlayAudio();
    }

    private void Update()
    {
        if (_target == null) {
            Destroy(gameObject);
        }
        else { 
            // Move our position a step closer to the target.
            var step = _speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, _target.position, step);
            transform.LookAt(_target);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable enemy = collision.gameObject.GetComponent<IDamageable>();

        if (enemy == null) {
            return;
        }

        Vector3 enemyPos = collision.transform.position;
        Vector3 dir = (enemyPos - transform.position).normalized;

        Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();

        enemy.Damage(Mathf.RoundToInt(_statsSO.Damage * GameManager.Instance.UpgradesCurve.Evaluate(UpgradeStats.IndexDamage)));
        GameObject temp = Instantiate(_particlePrefab, transform.position, Quaternion.identity);
        Destroy(temp, 3);

        enemyRb.velocity = Vector3.zero;
        enemyRb.AddForce(dir * 5, ForceMode.Impulse);

        DOVirtual.Vector3(enemyRb.velocity, Vector3.zero, 1, (Vector3 v) => enemyRb.velocity = v);

        Destroy(gameObject);
    }

    public void Init(HabilityStatsSO statsSO, Transform target)
    {
        _statsSO = statsSO;
        _target = target;
    }
}
