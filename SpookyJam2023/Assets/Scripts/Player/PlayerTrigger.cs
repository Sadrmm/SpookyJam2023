using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    bool canTrigger = true;

    [SerializeField] private float pushStrenght = 0.1f;

    [SerializeField] private int _damage = 5;

    private Rigidbody _rigidbody;

    public event Action OnPlayerPushed;

    public ParticleSystem explosionPrefab;

    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            canTrigger = false;

            DOVirtual.DelayedCall(1, () => canTrigger = true);

            Vector3 enemyPos = enemy.transform.position;

            Vector3 dir = (transform.position - enemyPos).normalized;

            float distance = (transform.position - enemyPos).magnitude;

            explosionPrefab.transform.position = transform.position + dir * distance;
            explosionPrefab.Play();

            _playerController.Damage(_damage);

            //_rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(dir * pushStrenght, ForceMode.Impulse);

            OnPlayerPushed?.Invoke();
        }
    }
}
