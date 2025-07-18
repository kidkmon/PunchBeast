using TMPro;
using UnityEngine;

public class HUDManager : Singleton<HUDManager>
{
    [SerializeField] TextMeshProUGUI _moneyText;

    void OnEnable() {  
        WalletSystem.Instance.OnMoneyUpdated.AddListener(UpdateMoneyUI);
    }

    void OnDisable() {
        WalletSystem.Instance.OnMoneyUpdated.RemoveListener(UpdateMoneyUI);
    }

    void UpdateMoneyUI(int money) => _moneyText.text = money.ToString();
}
