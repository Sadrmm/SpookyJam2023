using UnityEngine.Events;

public interface IDamageable
{
    public static UnityAction<IDamageable> OnDead { get; set; }
    public UnityAction<int> OnHealthChanged { get; set; }

    public void Damage(int damage);
    public void Dead();
}
