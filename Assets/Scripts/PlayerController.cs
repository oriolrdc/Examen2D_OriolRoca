using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Componentes
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    //Inputs
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _interactAction;
    private Vector2 _moveInput;
    //Variables
    private float _playerVelocity = 5;
    private float _jumpHeight = 0.3f;
    //GroundSensor
    [SerializeField] private Transform _sensorPosition;
    [SerializeField] private Vector2 _sensorSize = new Vector2(0.5f, 0.5f);
    //SensorEstrella
    [SerializeField] private Vector2 _starSensorSize = new Vector2(0.5f, 0.5f);

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _moveAction = InputSystem.actions["Move"];
        _jumpAction = InputSystem.actions["Jump"];
        _interactAction = InputSystem.actions["Interact"];
    }
    void Update()
    {
        _moveInput = _moveAction.ReadValue<Vector2>();
        Movement();
        if (_jumpAction.WasPressedThisFrame() && IsGrounded())
        {
            Jump();
        }

        _animator.SetBool("IsJumping", !IsGrounded());

        if(_interactAction.WasPerformedThisFrame())
        {
            Interact();
        }
    }

    void FixedUpdate()
    {
        _rigidBody.linearVelocity = new Vector2(_playerVelocity * _moveInput.x, _rigidBody.linearVelocity.y);
    }

    void Movement()
    {
        if (_moveInput.x > 0)
        {
            _animator.SetBool("IsRunning", true);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (_moveInput.x < 0)
        {
            _animator.SetBool("IsRunning", true);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            _animator.SetBool("IsRunning", false);
        }
    }

    void Jump()
    {
        _rigidBody.AddForce(transform.up * Mathf.Sqrt(_jumpHeight * -2 * Physics2D.gravity.y), ForceMode2D.Impulse);
    }

    bool IsGrounded()
    {
        Collider2D[] ground = Physics2D.OverlapBoxAll(_sensorPosition.position, _sensorSize, 0);
        foreach (Collider2D item in ground)
        {
            if (item.gameObject.layer == 3)
            {
                return true;
            }
        }
        return false;
    }

    void Interact()
    {
        Collider2D[] stars = Physics2D.OverlapBoxAll(transform.position, _starSensorSize, 0);
        foreach(Collider2D item in stars)
        {
            if (item.gameObject.CompareTag("Star"))
            {
                Star _starScript = item.gameObject.GetComponent<Star>();
                _starScript.Death();
            }
        }
    }

    void OnDrawGizmos()
    {
        //GroundSensor
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_sensorPosition.position, _sensorSize);
        //StarSensor
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, _starSensorSize);
    }
}
