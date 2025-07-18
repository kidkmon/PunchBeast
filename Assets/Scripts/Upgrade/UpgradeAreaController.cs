using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeAreaController : MonoBehaviour
{
    [Header("Upgrade Settings")]
    [SerializeField] private BagAssetConfig _upgradeBagConfig;
    [SerializeField] private float _timeToUpgrade = 2f;

    [Header("Upgrade References")]
    [SerializeField] private Image _fillBorderBackground;
    [SerializeField] private TextMeshProUGUI _costText;

    private float _stayTime;

    void Start()
    {
        _costText.text = _upgradeBagConfig.UpgradeCost.ToString();
        _fillBorderBackground.fillAmount = 0f;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _stayTime += Time.deltaTime;
            _fillBorderBackground.fillAmount = _stayTime / _timeToUpgrade;

            if (_stayTime >= _timeToUpgrade)
            {
                if (WalletSystem.Instance.TryDeductMoney(_upgradeBagConfig.UpgradeCost))
                {
                    UpgradePlayerBag();
                }
                else
                {
                    ToastMessage.Instance.Show("Not enough money to upgrade!\nPunch more Enemies!");
                }

            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _stayTime = 0f;
            _fillBorderBackground.fillAmount = 0f;
        }
    }

    void UpgradePlayerBag()
    {
        _stayTime = 0f;
        gameObject.SetActive(false);

        if (PlayerStack.Instance.StackedEnemies <= 0) return;

        PlayerStack.Instance.RemoveEnemies(_upgradeBagConfig.UpgradeCost);
        PlayerBag.Instance.UpgradeBag(_upgradeBagConfig);
    }
}
