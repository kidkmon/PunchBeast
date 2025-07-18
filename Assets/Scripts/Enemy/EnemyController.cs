using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody[] _ragdollRigidbodies;
    private Collider[] _ragdollColliders;

    private EnemyMovement _enemyMovement;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        _ragdollColliders = GetComponentsInChildren<Collider>();
        _enemyMovement = GetComponent<EnemyMovement>();

        SetRagdollEnabled(false, true);
    }

    public bool IsAttackable()
    {
        return _animator != null && _animator.enabled;
    }

    public void TakePunch()
    {
        if (IsAttackable())
        {
            SetRagdollEnabled(true, false);
            _enemyMovement.SetMovement(false);
        }
    }
    
    public void SetRagdollEnabled(bool isEnabled, bool enableAnimator)
    {
        _ragdollRigidbodies[0].isKinematic = isEnabled;
        _ragdollColliders[0].enabled = !isEnabled;
        _animator.enabled = enableAnimator;

        for (int i = 1; i < _ragdollRigidbodies.Length; i++)
        {
            _ragdollRigidbodies[i].isKinematic = !isEnabled;
        }
        for (int i = 1; i < _ragdollColliders.Length; i++)
        {
            _ragdollColliders[i].enabled = isEnabled;
        }
    }
}
