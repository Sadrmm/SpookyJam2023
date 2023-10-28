using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour, IDamageable, ICharacter
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    [SerializeField] private Painter _painter;

    [SerializeField] CharacterStatsSO _statsSO;
    public CharacterStatsSO StatsSO => _statsSO;

    private Rigidbody _rb;
    private Vector2 _dir;
    private int _currentHealth;
    private bool _canMove = true;

    public UnityAction<int> OnHealthChanged { get; set; }
    public UnityAction OnDead { get; set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        GetComponent<PlayerTrigger>().OnPlayerPushed += PlayerTrigger_OnPlayerPushed;
    }

    private void Start()
    {
        _currentHealth = _statsSO.MaxHealth;
    }

    private void Update()
    {
        _dir = GetDirectionNormalized();

        if (_canMove)
        {
            HandleRotation();
        }

        if (Input.GetKeyDown(KeyCode.K))
            Damage(10);
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            HandleMovement();
        }
    }

    private Vector2 GetDirectionNormalized()
    {
        Vector2 dir = Vector2.zero;
        bool keyPressed = false;
        if (Input.GetKey(KeyCode.W))
        {
            dir.y += 1;
            keyPressed = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            dir.y -= 1;
            keyPressed = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            dir.x -= 1;
            keyPressed = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dir.x += 1;
            keyPressed = true;
        }

        if (keyPressed)
        {
            _painter.Paint();
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


    Coroutine coroutine;
    private void PlayerTrigger_OnPlayerPushed()
    {
        _canMove = false;
        if (coroutine != null) 
        { 
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(PushTimer(0.3f));
    }

    private IEnumerator PushTimer(float t)
    {
        yield return new WaitForSeconds(t);
        _canMove = true;
    }
}
