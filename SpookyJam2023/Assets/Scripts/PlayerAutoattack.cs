using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoattack : MonoBehaviour
{
    [SerializeField] HabilityStatsSO _statsSO;
    [SerializeField] LayerMask _enemyLayerMask;
    public GameObject projectilePrefab;
    private float lastShotTime;
  


    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastShotTime < _statsSO.Cooldown)
        {
            return;
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _statsSO.Range,_enemyLayerMask);

        if (hitColliders.Length > 0)
        {
            foreach(Collider collider in hitColliders)
            {
               IDamageable enemy = collider.GetComponent<IDamageable>();

                if (enemy == null) {
                    continue;
                }
                GameObject proyectile = Instantiate(projectilePrefab,transform.position,transform.rotation );
                proyectile.GetComponent<Proyectile>();
            }           
        }
    }

   /* private Transform FindNearestEnemy(Collider[] enemies)
    {
        Transform nearestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }

        return nearestEnemy;
    }*/

    /*private void ShootAtEnemy(Transform enemy)
    {
        // Calcula la direcci�n hacia el enemigo
        Vector3 direction = (enemy.position - shootingPoint.position).normalized;

        // Crea y dispara el proyectil
        GameObject projectile = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = direction * Vel;
    }*/
}
