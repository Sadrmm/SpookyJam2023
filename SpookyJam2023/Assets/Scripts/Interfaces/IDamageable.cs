using UnityEngine.Events;

public interface IDamageable
{
    public UnityAction<int> OnHealthChanged { get; set; }
    public UnityAction OnDead { get; set; }

    public void Damage(int damage);
    public void Dead();
}
