using UnityEngine;

public class Scare : MonoBehaviour, IHability
{
    [SerializeField] HabilityStatsSO _statsSO;
    public HabilityStatsSO StatsSO => _statsSO;
    [SerializeField] LayerMask _enemyLayerMask;
    [SerializeField] float _explosionForce = 10.0f;

    public void Attack()
    {
        float range = _statsSO.Range * GameManager.Instance.UpgradesCurve.Evaluate(UpgradeStats.IndexRange);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, _enemyLayerMask);
        foreach (Collider collider in hitColliders) {
            IDamageable enemy = collider.GetComponent<IDamageable>();

            if (enemy == null) {
                continue;
            }

            enemy.Damage(Mathf.RoundToInt(_statsSO.Damage * GameManager.Instance.UpgradesCurve.Evaluate(UpgradeStats.IndexDamage)));
            //Push(collider);
            Forces.PushObject(collider, _explosionForce, transform.position, range);

            IScareable scareable = enemy as IScareable;

            if (scareable == null) {
                continue;
            }

            scareable.BeScared();
        }
    }
}
