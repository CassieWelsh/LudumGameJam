using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float speed = 5f;
    public float fallingSpeed = 5f;
    public float rotationSpeed = 15f;
    private Vector2 _moveValue = Vector2.zero;
    private Transform _playerTransform;
    [SerializeField]
    private Transform _canonTransform;
    private Camera _camera;
    [SerializeField]
    private PlayerInput _playerInput;
    private InputAction _movement;
    private InputAction _fire;

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
    }

    void Update()
    {
        //Player movement
        _moveValue = _movement.ReadValue<Vector2>();        
        _playerTransform.position += _playerTransform.up * speed * Time.deltaTime * _moveValue.y;

        _playerTransform.Rotate(new Vector3(0, 0, -rotationSpeed * _moveValue.x * Time.deltaTime));

        if (_moveValue.y == 0)
            _playerTransform.position -= new Vector3(0, fallingSpeed * Time.deltaTime, 0);
        
        //Canon follows the mouse
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePosition.z = _canonTransform.position.z;

        Vector2 direction = mousePosition - _canonTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _canonTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void LateUpdate()
    {
        _canonTransform.localEulerAngles = new Vector3(0, 0, _canonTransform.localEulerAngles.z);
    }

    private void Fire(InputAction.CallbackContext context)
    {
        print("Pew");
    }
}
