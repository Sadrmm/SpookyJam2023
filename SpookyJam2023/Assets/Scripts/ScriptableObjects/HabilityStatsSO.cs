using UnityEngine;

[CreateAssetMenu()]
public class HabilityStatsSO : ScriptableObject
{
    [SerializeField] int _damage;
    [SerializeField] float _cooldown;
    [SerializeField] float _range;

    public int Damage => _damage;
    public float Cooldown => _cooldown;
    public float Range => _range;
}
