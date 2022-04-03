using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Set in Inspector")]
    public int hp = 10;
    public float speed = 5f;
    public float fallingSpeed = 5f;
    public float rotationSpeed = 15f;
    public float projectileSpeed = 5f;
    public float coolDown = .5f;
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
    private Vector2 _currentMoveValue;
    private Vector2 _smoothInputVelocity;
    [SerializeField]
    private float _smoothInputSpeed;
    private GameObject _engineFlame;
    [SerializeField]
    private Transform _shootingPoint;
    [SerializeField]
    private GameObject _projectilePrefab;

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
        _playerInput = new PlayerInput();
        if (_playerTransform == null)
            _playerTransform = GetComponent<Transform>();
        _camera = Camera.main;
        _engineFlame = transform.Find("EngineFlame").gameObject;
    }

    void Update()
    {
        //Player movement
        _moveValue = _movement.ReadValue<Vector2>();        
        Vector2 _actualMoveValue = Vector2.SmoothDamp(_currentMoveValue, _moveValue, ref _smoothInputVelocity, _smoothInputSpeed);
        _playerTransform.position += _playerTransform.up * speed * Time.deltaTime * _actualMoveValue.y;

        _playerTransform.Rotate(new Vector3(0, 0, -rotationSpeed * _moveValue.x * Time.deltaTime));

        if (_moveValue.y == 0)
            _playerTransform.position -= new Vector3(0, fallingSpeed * Time.deltaTime, 0);
        
        //Canon follows the mouse
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePosition.z = _canonTransform.position.z;

        Vector2 direction = mousePosition - _canonTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _canonTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    
        if (_moveValue.y != 0)
            _engineFlame.SetActive(true);
        else 
            _engineFlame.SetActive(false);

    }

    private void Fire(InputAction.CallbackContext context)
    {
        if (Time.time - lastShotTime < coolDown)        
            return;

        GameObject go = Instantiate(_projectilePrefab);
        Rigidbody2D goRigid = go.GetComponent<Rigidbody2D>();
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = (Vector2) (mousePosition - _shootingPoint.position);

        goRigid.position = _shootingPoint.position;
        goRigid.velocity = direction.normalized * projectileSpeed;

        lastShotTime = Time.time;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        print("Das");
    }
}
