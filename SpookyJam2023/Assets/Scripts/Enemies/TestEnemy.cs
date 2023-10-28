using UnityEngine;
using UnityEngine.Events;

public class TestEnemy : MonoBehaviour, IDamageable, IScareable
{

    [SerializeField] CharacterStatsSO _statsSO;
    private float _currentHealth;

    private bool _isScared;

    public UnityAction<float> OnHealthChanged { get ; set; }
    public UnityAction OnDead { get; set; }
    
    public float MaxTimeScared { get; set; }
    public bool IsScared => _isScared;

    private void Start()
    {
        _isScared = false;
        _currentHealth = _statsSO.MaxHealth;
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
        _isScared = true;
        IScareable.OnScared?.Invoke(this);
    }
    #endregion
}
