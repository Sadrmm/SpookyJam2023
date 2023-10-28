using UnityEngine;
using UnityEngine.Events;

public class PlayerHabilities : MonoBehaviour
{
    public static UnityAction<float, HabilityStatsSO> OnTimerUpdated;

    [SerializeField] LayerMask _enemyLayerMask;

    [Header("Projectil AutoAttack")]
    [SerializeField] HabilityStatsSO _projectileStatsSO;
    [SerializeField] GameObject _projectilePrefab;
    [SerializeField] Transform _projectileSpawn;
    private float _lastShotTime;

    [Header("Scare Attack")]
    [SerializeField] Scare _scare;
    private float _lastScareAttack;

    private void Update()
    {
        OnTimerUpdated?.Invoke(Time.time - _lastShotTime, _projectileStatsSO);
        HandleProjectilAutoAttack();
        OnTimerUpdated?.Invoke(Time.time - _lastScareAttack, _scare.StatsSO);
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
        if (Time.time - _lastScareAttack < _scare.StatsSO.Cooldown) {
            return;
        }

        if (Input.GetKeyDown(_scare.StatsSO.KeyCode)) {
            _scare.Attack();
            _lastScareAttack = Time.time;
        }
    }
}
