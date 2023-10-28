using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TestEnemy : MonoBehaviour, ICharacter, IDamageable, IScareable
{

    [SerializeField] CharacterStatsSO _statsSO;
    public CharacterStatsSO StatsSO => _statsSO;

    private float _currentHealth;

    [SerializeField] float _maxTimeScared;
    private bool _isScared;

    public UnityAction<float> OnHealthChanged { get ; set; }
    public UnityAction OnDead { get; set; }

    public bool IsScared => _isScared;

    private void Start()
    {
        _isScared = false;
        _currentHealth = _statsSO.MaxHealth;
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
        IScareable.OnUnscared?.Invoke(this);
    }

    #region IDamageable
    public void Damage(int damage)
    {
        _currentHealth -= damage;
        OnHealthChanged?.Invoke(_currentHealth);
    }

    public void Dead()
    {
        Debug.Log($"{gameObject} morido");
        OnDead?.Invoke();
    }
    #endregion

    #region IScareable
    public void BeScared()
    {
        StopAllCoroutines();
        
        _isScared = true;
        IScareable.OnScared?.Invoke(this);

        StartCoroutine(StopBeingScared());
    }
    #endregion
}
