using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{
    private HabilityStatsSO _statsSO;
    private void OnCollisionEnter(Collision collision)
    {

        IDamageable enemy = collision.gameObject.GetComponent<IDamageable>();

        if (enemy == null)
        {
            return;
        }
        enemy.Damage(_statsSO.Damage);
    }

    public void Init(HabilityStatsSO statsSO)
    {
        _statsSO = statsSO;
    }

}
