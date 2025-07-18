using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Enemy Path Settings")]
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private float _moveSpeed = 3f;

    private Animator _animator;

    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private Vector3 _currentTarget;
    private Vector3 _targetPosition;
    private bool _isMoving;


    void Start()
    {
        _animator = GetComponent<Animator>();
        _startPosition = _startPoint.position;
        _endPosition = _endPoint.position;
        _currentTarget = _endPosition;
        _isMoving = true;

        _animator.SetBool("isMoving", _isMoving);
    }

    void Update()
    {
        if (!_isMoving || !GameManager.Instance.GameStarted) return;

        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        _targetPosition = new Vector3(_currentTarget.x, transform.position.y, _currentTarget.z);
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _moveSpeed * Time.deltaTime);
        transform.LookAt(_targetPosition);

        if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
        {
            _currentTarget = _currentTarget == _endPosition ? _startPosition : _endPosition;
        }
    }

    public void StopMovement()
    {
        _isMoving = false;
        _animator.SetBool("isMoving", _isMoving);
    }
}
