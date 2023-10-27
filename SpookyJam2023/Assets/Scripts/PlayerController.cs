using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    [SerializeField] StatsSO _statsSO;
    private Rigidbody _rb;

    private Vector2 _movXZ;

    private float _currentHealth;

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
        _movXZ = new Vector2(Input.GetAxis(HORIZONTAL),
            Input.GetAxis(VERTICAL));

        HandleRotation();

        if (Input.GetKeyDown(KeyCode.K))
            Damage(10);
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleRotation()
    {
        Vector3 desiredRotation = new Vector3(_movXZ.x, 0f, _movXZ.y);

        transform.forward = Vector3.Slerp(transform.forward, desiredRotation, Time.deltaTime * _statsSO.RotSpeed);
    }

    private void HandleMovement()
    {
        Vector3 velocity = new Vector3(_movXZ.x, 0f, _movXZ.y);
        velocity.Normalize();
        velocity *= _statsSO.MoveSpeed;

        _rb.velocity = velocity;
    }

    #region IDamageable
    public void Damage(int damage)
    {
        _currentHealth -= damage;
        Debug.Log($"Vida: {_currentHealth}");

        if (_currentHealth <= 0) {
            Dead();
        }
    }

    public void Dead()
    {
        Debug.Log("Morido");
    }
    #endregion
}
