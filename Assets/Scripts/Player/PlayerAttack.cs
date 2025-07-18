using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator _animator;
    private int _animIDPunch;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        AssignAnimationIDs();
    }

    void AssignAnimationIDs()
    {
        _animIDPunch = Animator.StringToHash("Punch");
    }

    public void Attack()
    {
        _animator.SetTrigger(_animIDPunch);
        AudioManager.Instance.PlayPunchSound();
    }
}
