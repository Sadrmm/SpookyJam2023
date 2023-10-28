using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour, IDamageable
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    [SerializeField] CharacterStatsSO _statsSO;
    private Rigidbody _rb;

    private Vector2 _dir;

    private float _currentHealth;

    public UnityAction<float> OnHealthChanged { get; set; }
    public UnityAction OnDead { get; set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _currentHealth = _statsSO.MaxHealth;
    }
    private void Update()
    {
        _dir = GetDirectionNormalized();

        HandleRotation();

        if (Input.GetKeyDown(KeyCode.K))
            Damage(10);
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private Vector2 GetDirectionNormalized()
    {
        Vector2 dir = Vector2.zero; // -> NEW INPUT SYSTEM
        if (Input.GetKey(KeyCode.W))
        {
            dir.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            dir.y -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            dir.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dir.x += 1;
        }

        return dir.normalized;
    }

    private void HandleRotation()
    {
        Vector3 desiredRotation = new Vector3(_dir.x, 0f, _dir.y);

        transform.forward = Vector3.Slerp(transform.forward, desiredRotation, Time.deltaTime * _statsSO.RotSpeed);
    }

    private void HandleMovement()
    {
        Vector3 velocity = new Vector3(_dir.x, 0f, _dir.y);
        velocity.Normalize();
        velocity *= _statsSO.MoveSpeed;

        _rb.velocity = velocity;
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
    }
    #endregion
}
