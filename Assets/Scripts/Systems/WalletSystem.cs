using UnityEngine;
using UnityEngine.Events;

public class WalletSystem : Singleton<WalletSystem>
{
    [HideInInspector] public UnityEvent<int> OnMoneyUpdated;

    private int _money;

    public void Initialize()
    {
        SetMoney(0);
    }

    void SetMoney(int value)
    {
        _money = value;
        OnMoneyUpdated?.Invoke(_money);
    }

    #region Public Methods

    public int Money => _money;
    public void AddMoney(int value) => SetMoney(_money + value);

    public bool TryDeductMoney(int deductValue)
    {
        if (_money - deductValue < 0) return false;
        SetMoney(_money - deductValue);
        return true;
    }

    #endregion
}
