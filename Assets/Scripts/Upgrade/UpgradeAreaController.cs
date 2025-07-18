using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeAreaController : MonoBehaviour
{
    [Header("Upgrade Settings")]
    [SerializeField] private int _upgradeCost;
    [SerializeField] private float _timeToUpgrade = 2f;

    [Header("Upgrade References")]
    [SerializeField] private Image _fillBorderBackground;
    [SerializeField] private TextMeshProUGUI _costText;

    private float _stayTime;

    void Start()
    {
        _costText.text = _upgradeCost.ToString();
        _fillBorderBackground.fillAmount = 0f;  
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _stayTime += Time.deltaTime;
            _fillBorderBackground.fillAmount = _stayTime / _timeToUpgrade;

            if (_stayTime >= _timeToUpgrade && WalletSystem.Instance.TryDeductMoney(_upgradeCost))
            {
                UpgradePlayer();
                _stayTime = 0f;
                gameObject.SetActive(false);
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

    void UpgradePlayer()
    {
        if (PlayerStack.Instance.StackCapacity <= 0) return;

        PlayerStack.Instance.RemoveEnemies(_upgradeCost);
    }
}
