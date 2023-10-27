using UnityEngine.Events;

public interface IDamageable
{
    public UnityAction<float> OnHealthChanged { get; set; }
    public UnityAction OnDead { get; set; }

    public void Damage(int damage);
    public void Dead();
}
