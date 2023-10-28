using UnityEngine;

public class PlayerHabilities : MonoBehaviour
{
    [SerializeField] LayerMask _enemyLayerMask;

    [Header("Projectil AutoAttack")]
    [SerializeField] HabilityStatsSO _projectileStatsSO;
    [SerializeField] GameObject _projectilePrefab;
    [SerializeField] Transform _projectileSpawn;
    private float _lastShotTime;

    [Header("Scare Attack")]
    [SerializeField] Scare _scare;

    private void Update()
    {
        HandleProjectilAutoAttack();
        HandleScareAttack();
    }

    private void HandleProjectilAutoAttack()
    {
        if (Time.time - _lastShotTime < _projectileStatsSO.Cooldown) {
            return;
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _projectileStatsSO.Range, _enemyLayerMask);

        if (hitColliders.Length > 0) {
            foreach (Collider collider in hitColliders) {
                IDamageable enemy = collider.GetComponent<IDamageable>();

                if (enemy == null) {
                    continue;
                }

                GameObject proyectileGO = Instantiate(_projectilePrefab, _projectileSpawn.position, _projectileSpawn.rotation);
                Proyectile proyectile = proyectileGO.GetComponent<Proyectile>();

                if (proyectile == null) {
                    Debug.LogError($"Bad reference of ProjectilePrefab!!!");
                    return;
                }

                // shoot projectile to target and stop searching
                proyectile.Init(_projectileStatsSO, collider.transform);
                _lastShotTime = Time.time;
                return;
            }
        }
    }

    private void HandleScareAttack()
    {
        if (Input.GetKeyDown(_scare.StatsSO.KeyCode)) {
            _scare.Attack();
        }
    }
}
