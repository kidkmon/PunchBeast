using TMPro;
using UnityEngine;

public class HUDManager : Singleton<HUDManager>
{
    [Header("Screen Elements")]
    [SerializeField] private GameObject _startScreen;
    [SerializeField] private GameObject _endScreen;

    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI _bagCapacityText;
    [SerializeField] TextMeshProUGUI _moneyText;

    void OnEnable()
    {
        PlayerBag.Instance.OnBagUpdated.AddListener(UpdateBagCapacityUI);
        WalletSystem.Instance.OnMoneyUpdated.AddListener(UpdateMoneyUI);
    }

    void OnDisable()
    {
        PlayerBag.Instance.OnBagUpdated.AddListener(UpdateBagCapacityUI);
        WalletSystem.Instance.OnMoneyUpdated.RemoveListener(UpdateMoneyUI);
    }

    void Start()
    {
        _startScreen.SetActive(true);
        _endScreen.SetActive(false);
    }

    void UpdateBagCapacityUI(int capacity) => _bagCapacityText.text = capacity.ToString();
    void UpdateMoneyUI(int money) => _moneyText.text = money.ToString();

    #region Screen Management

    public void ShowStartScreen()
    {
        _startScreen.SetActive(true);
        _endScreen.SetActive(false);
    }

    public void ShowEndScreen()
    {
        _startScreen.SetActive(false);
        _endScreen.SetActive(true);
    }
    
    #endregion
}
