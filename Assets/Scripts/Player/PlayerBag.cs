using UnityEngine;
using UnityEngine.Events;

public class PlayerBag : Singleton<PlayerBag>
{
    [Header("Bag Settings")]
    [SerializeField] private BagAssetConfig _starterBagConfig;
    [SerializeField] private Material _bagMaterial;

    [HideInInspector] public UnityEvent<int> OnBagUpdated;
    private BagAssetConfig _currentBagConfig;

    public void Initialize()
    {
        _currentBagConfig = _starterBagConfig;
        UpgradeBag(_currentBagConfig);
    }

    void UpdateBagVisuals()
    {
        if (_currentBagConfig != null)
        {
            _bagMaterial.color = _currentBagConfig.BagColor;
        }
    }

    #region Public Methods

    public int BagCapacity => _currentBagConfig.BagCapacity;

    public void UpgradeBag(BagAssetConfig newBagConfig)
    {
        if (newBagConfig == null) return;

        _currentBagConfig = newBagConfig;
        UpdateBagVisuals();
        OnBagUpdated?.Invoke(_currentBagConfig.BagCapacity);
    }

    #endregion
}
