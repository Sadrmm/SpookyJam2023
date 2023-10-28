using UnityEngine;

[CreateAssetMenu()]
public class HabilityStatsSO : ScriptableObject
{
    [SerializeField] int _damage;
    [SerializeField] float _cooldown;
    [SerializeField] float _range;
    [SerializeField] Sprite _sprite;
    [SerializeField] KeyCode _keyCode;

    public int Damage => _damage;
    public float Cooldown => _cooldown;
    public float Range => _range;
    public Sprite Sprite => _sprite;
    public KeyCode KeyCode => _keyCode;
}
