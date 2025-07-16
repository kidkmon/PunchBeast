using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.enabled = true;
    }
    
    public bool IsAttackable()
    {
        return _animator != null && _animator.enabled;
    }

    public void TakePunch()
    {
        if (IsAttackable())
        {
            _animator.SetTrigger("Stop");
            _animator.enabled = false;
        }
    }

}
