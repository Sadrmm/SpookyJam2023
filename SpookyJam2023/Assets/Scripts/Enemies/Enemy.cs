using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ScaredEnemyAI))]
[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour, ICharacter, IDamageable, IScareable
{
    [Header("AI")]
    [SerializeField] BaseEnemyAI _defaulAI;
    [SerializeField] ScaredEnemyAI _scaredAI;

    [Header("Scared")]
    [SerializeField] ScaredUIComponent _scaredComp;
    [SerializeField] float _maxTimeScared;

    [Header("Character")]
    [SerializeField] CharacterStatsSO _statsSO;
    [SerializeField] GameObject _particlesPrefab;
    public CharacterStatsSO StatsSO => _statsSO;

    private int _currentHealth;

    private bool _isScared;

    public UnityAction<int> OnHealthChanged { get ; set; }
    public UnityAction OnDead { get; set; }

    public bool IsScared => _isScared;

    private void Start()
    {
        _isScared = false;
        _currentHealth = _statsSO.MaxHealth;
        _scaredAI.enabled = false;
    }

    IEnumerator StopBeingScared()
    {
        float timeScared = Time.time;
        float scaredStarted = Time.time;

        while (timeScared < _maxTimeScared + scaredStarted) {
            yield return new WaitForSeconds(0.1f);

            timeScared = Time.time;
        }

        _isScared = false;
        _scaredComp.Hide();

        _defaulAI.enabled = true;
        _scaredAI.enabled = false;

        IScareable.OnUnscared?.Invoke(this);
    }

    #region IDamageable
    public void Damage(int damage)
    {
        _currentHealth -= damage;
        OnHealthChanged?.Invoke(_currentHealth);

        if (_currentHealth <= 0) {
            Dead();
        }
    }

    public void Dead()
    {
        OnDead?.Invoke();
        GameObject temp = Instantiate(_particlesPrefab, transform.position, Quaternion.identity);
        Destroy(temp, 3);
        Destroy(gameObject);
    }
    #endregion

    #region IScareable
    public void BeScared()
    {
        StopAllCoroutines();
        
        _isScared = true;
        _scaredComp.Show(_maxTimeScared);

        _defaulAI.enabled = false;
        _scaredAI.enabled = true;
        _scaredAI.Init(_defaulAI.PlayerTransform);

        IScareable.OnScared?.Invoke(this);

        StartCoroutine(StopBeingScared());
    }

    internal void DisableEnemy()
    {
        _defaulAI.enabled = false;
        _scaredAI.enabled = false;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        _defaulAI.Agent.enabled = false;
        this.enabled = false;
    }
    #endregion
}
