using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{

    [SerializeField] private float pushStrenght = 30f;

    private Rigidbody _rigidbody;

    public event Action OnPlayerPushed;

    public ParticleSystem explosionPrefab;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            Vector3 enemyPos = enemy.transform.position;

            Vector3 dir = (transform.position - enemyPos).normalized;

            float distance = (transform.position - enemyPos).magnitude;

            explosionPrefab.transform.position = transform.position + dir * distance;
            explosionPrefab.Play();

            Debug.DrawLine(enemyPos, dir, Color.red, 5f);

            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(dir * pushStrenght, ForceMode.Impulse);

            OnPlayerPushed?.Invoke();
        }
    }
}
