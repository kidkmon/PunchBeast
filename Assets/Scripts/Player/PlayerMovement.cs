using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 1f;

    private CharacterController _controller;

    private Animator _animator;
    private int _animIDMoveInput;

    private Vector2 _moveInput;
    private Vector3 _moveDirection;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        AssignAnimationIDs();
    }

    void Update()
    {
        if (!GameManager.Instance.GameStarted) return;

        InputMagnitude();
    }

    void AssignAnimationIDs()
    {
        _animIDMoveInput = Animator.StringToHash("MoveInput");
    }

    void InputMagnitude()
    {
        float inputMagnitude = _moveInput.sqrMagnitude;

        if (inputMagnitude > 0.1f)
        {
            _animator.SetFloat(_animIDMoveInput, inputMagnitude, .1f, Time.deltaTime);
            PlayerMoveAndRotate();
        }
        else
        {
            _animator.SetFloat(_animIDMoveInput, inputMagnitude, .1f, Time.deltaTime);
        }
    }

    void PlayerMoveAndRotate()
    {
        _moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y);
        _moveDirection.Normalize();

        if (_moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }

        Vector3 move = _moveDirection * _moveSpeed * Time.deltaTime;
        _controller.Move(move);
    }


    #region PlayerInput 

    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    #endregion
}
