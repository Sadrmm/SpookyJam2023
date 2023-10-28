using UnityEngine;

public class Scare : MonoBehaviour
{
    [SerializeField] HabilityStatsSO _statsSO;
    [SerializeField] LayerMask _enemyLayerMask;

    private float _nextAttackTime;

    private void Start()
    {
        _nextAttackTime = Time.time + _statsSO.Cooldown;
    }

    public void Attack()
    {
        if (_nextAttackTime < Time.time) {
            return;
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _statsSO.Range, _enemyLayerMask);
        foreach (Collider collider in hitColliders) {
            IDamageable enemy = collider.GetComponent<IDamageable>();

            if (enemy == null) {
                continue;
            }

            enemy.Damage(_statsSO.Damage);

            IScareable scareable = enemy as IScareable;

            if (scareable == null) {
                continue;
            }

            scareable.GetScared();
        }

        _nextAttackTime = Time.time + _statsSO.Cooldown;
    }
}
