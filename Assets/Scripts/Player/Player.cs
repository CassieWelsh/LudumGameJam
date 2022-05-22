using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player S;
    
    [Header("Set in Inspector")]
    public int maxHp = 10;
    public int hp;
    public int projectileDamage = 1;
    public float velocity = 5f;
    public float fallingVelocity = 5f;
    public float rotationVelocity = 15f;
    public float projectileVelocity = 5f;
    public float weaponCoolDown = .5f;
    public float damageSplashTime = 2f; 
    [HideInInspector]
    public float invincibleTill = 0;
    private SpriteRenderer[] _spriteRenderer;
    public Vector2 fallingLimit = new Vector2(-5, -5);
    public Vector2 accelerationLimit = new Vector2(5, 5); 
    private float lastShotTime = 0f;
    private Vector2 _moveValue = Vector2.zero;
    private Transform _playerTransform;
    [SerializeField]
    private Transform _canonTransform;
    private Camera _camera;
    [SerializeField]
    private PlayerInput _playerInput;
    private InputAction _movement;
    private InputAction _fire;
    private GameObject _engineFlame;
    [SerializeField]
    private Transform _shootingPoint;
    [SerializeField]
    private GameObject _projectilePrefab;
    private Rigidbody2D _rigid;

    void OnEnable()
    {
        _movement = _playerInput.Player.Move;
        _movement.Enable();

        _fire = _playerInput.Player.Fire;
        _fire.Enable();
        _fire.performed += Fire;
    }    

    void OnDisable()
    {
        _movement.Disable();
        _fire.Disable();
    }    

    void Awake()
    {
        if (S == null) S = this;
        else Debug.LogError("Tried to create another instance of Player");
        
        _playerInput = new PlayerInput();
        if (_playerTransform == null)
            _playerTransform = GetComponent<Transform>();
        _camera = Camera.main;
        _engineFlame = transform.Find("EngineFlame").gameObject;
        _rigid = GetComponent<Rigidbody2D>();
        hp = maxHp;

        _spriteRenderer = GetComponentsInChildren<SpriteRenderer>();
    }

    void Update()
    {
        Move();
        LimitAcceleration();
        ApplyFallVelocity();

        TwistCanon();
        CheckDamagedState();
    }

    private void Move()
    {
        _moveValue = _movement.ReadValue<Vector2>();        
        _rigid.velocity += (Vector2) _playerTransform.up * (velocity * Time.deltaTime * _moveValue.y);
        _playerTransform.Rotate(new Vector3(0, 0, -rotationVelocity * _moveValue.x * Time.deltaTime));

        Ignite();
    }

    private void Ignite()
    {
        if (_moveValue.y != 0)
            _engineFlame.SetActive(true);
        else 
            _engineFlame.SetActive(false);
    }

    private void LimitAcceleration()
    {
        if (_rigid.velocity.y <= fallingLimit.y)
        {
            Vector2 limit = new Vector2(_rigid.velocity.x, fallingLimit.y); 
            _rigid.velocity = limit;
        }

        if (_rigid.velocity.x <= fallingLimit.x)
        {
            Vector2 limit = new Vector2(fallingLimit.x, _rigid.velocity.y); 
            _rigid.velocity = limit;
        }

        if (_rigid.velocity.y >= accelerationLimit.y)
        {
            Vector2 limit = new Vector2(_rigid.velocity.x, accelerationLimit.y); 
            _rigid.velocity = limit;
        }

        if (_rigid.velocity.x >= accelerationLimit.x)
        {
            Vector2 limit = new Vector2(accelerationLimit.x, _rigid.velocity.y); 
            _rigid.velocity = limit;
        }
    }

    private void ApplyFallVelocity()
    {
        if (_moveValue.y == 0)
            // _playerTransform.position -= new Vector3(0, fallingSpeed * Time.deltaTime, 0);
            _rigid.velocity -= new Vector2(0, fallingVelocity) * Time.deltaTime;
    }

    private void TwistCanon()
    {
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePosition.z = _canonTransform.position.z;

        Vector2 direction = mousePosition - _canonTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _canonTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    
    private void CheckDamagedState()
    {
        if (Time.time < invincibleTill)    
            foreach (var mat in _spriteRenderer)
                mat.material.color = Color.red;
        else
            foreach (var mat in _spriteRenderer)
                mat.material.color = Color.white;
    }

    private void Fire(InputAction.CallbackContext context)
    {
        if (Time.time - lastShotTime < weaponCoolDown)        
            return;

        GameObject go = Instantiate(_projectilePrefab);
        Rigidbody2D goRigid = go.GetComponent<Rigidbody2D>();
        Projectile projectile = go.GetComponent<Projectile>();
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = (Vector2) (mousePosition - _shootingPoint.position);

        projectile.damage = projectileDamage;
        goRigid.position = _shootingPoint.position;
        goRigid.velocity = direction.normalized * projectileVelocity;

        lastShotTime = Time.time;
    }
}
