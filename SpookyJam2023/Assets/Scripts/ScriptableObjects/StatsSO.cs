using UnityEngine;

[CreateAssetMenu()]
public class StatsSO : ScriptableObject
{
    [SerializeField] float _moveSpeed;
    [SerializeField] float _rotSpeed;
    [SerializeField] int _maxHealth;

    public float MoveSpeed => _moveSpeed;
    public float RotSpeed => _rotSpeed;
    public int MaxHealth => _maxHealth;

    public override string ToString()
    {
        return $"MoveSpeed: {MoveSpeed}, RotSpeed: {RotSpeed}, MaxHealth: {MaxHealth}";
    }
}
