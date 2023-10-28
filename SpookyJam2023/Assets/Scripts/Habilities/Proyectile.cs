using UnityEngine;

public class Proyectile : MonoBehaviour
{
    [SerializeField] float _speed = 1.0f;

    private HabilityStatsSO _statsSO;
    private Transform _target;

    private void Update()
    {
        // Move our position a step closer to the target.
        var step = _speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, _target.position, step);
        transform.LookAt(_target);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"collision with: {collision}");
        IDamageable enemy = collision.gameObject.GetComponent<IDamageable>();

        if (enemy == null) {
            return;
        }

        enemy.Damage(_statsSO.Damage);
        Destroy(gameObject);
    }

    public void Init(HabilityStatsSO statsSO, Transform target)
    {
        _statsSO = statsSO;
        _target = target;
    }
}
