using UnityEngine;

public class Proyectile : MonoBehaviour, IHability
{
    [SerializeField] float _speed = 1.0f;
    [SerializeField] GameObject _particlePrefab;
    private HabilityStatsSO _statsSO;
    public HabilityStatsSO StatsSO => _statsSO;

    private Transform _target;

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

        enemy.Damage(Mathf.RoundToInt(_statsSO.Damage * GameManager.Instance.UpgradesCurve.Evaluate(UpgradeStats.IndexDamage)));
        GameObject temp = Instantiate(_particlePrefab, transform.position, Quaternion.identity);
        Destroy(temp, 3);
        Forces.PushObject(collision.collider, _speed, transform.position, _statsSO.Range);
        Destroy(gameObject);
    }

    public void Init(HabilityStatsSO statsSO, Transform target)
    {
        _statsSO = statsSO;
        _target = target;
    }
}
