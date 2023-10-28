using UnityEngine;

public class Scare : MonoBehaviour
{
    [SerializeField] HabilityStatsSO _statsSO;
    [SerializeField] LayerMask _enemyLayerMask;
    [SerializeField] float _explosionForce = 10.0f;

    private float _nextAttackTime;

    private void Start()
    {
        _nextAttackTime = Time.time + _statsSO.Cooldown;
    }

    public void Attack()
    {
        if (_nextAttackTime > Time.time) {
            return;
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _statsSO.Range, _enemyLayerMask);
        foreach (Collider collider in hitColliders) {
            IDamageable enemy = collider.GetComponent<IDamageable>();

            if (enemy == null) {
                continue;
            }

            enemy.Damage(_statsSO.Damage);
            Push(collider);

            IScareable scareable = enemy as IScareable;

            if (scareable == null) {
                continue;
            }

            scareable.BeScared();
        }

        _nextAttackTime = Time.time + _statsSO.Cooldown;
    }

    private void Push(Collider collider)
    {
        Rigidbody rb = collider.GetComponent<Rigidbody>();

        if (rb == null) {
            return;
        }

        rb.AddExplosionForce(_explosionForce, transform.position, _statsSO.Range);
    }
}
