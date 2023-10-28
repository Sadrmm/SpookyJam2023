using UnityEngine;

public class Scare : MonoBehaviour, IHability
{
    [SerializeField] HabilityStatsSO _statsSO;
    public HabilityStatsSO StatsSO => _statsSO;
    [SerializeField] LayerMask _enemyLayerMask;
    [SerializeField] float _explosionForce = 10.0f;

    public void Attack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _statsSO.Range, _enemyLayerMask);
        foreach (Collider collider in hitColliders) {
            IDamageable enemy = collider.GetComponent<IDamageable>();

            if (enemy == null) {
                continue;
            }

            enemy.Damage(_statsSO.Damage);
            //Push(collider);
            Forces.PushObject(collider, _explosionForce, transform.position, _statsSO.Range);

            IScareable scareable = enemy as IScareable;

            if (scareable == null) {
                continue;
            }

            scareable.BeScared();
        }
    }
}
