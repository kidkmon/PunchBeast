using UnityEngine;

public class PlayerBag : Singleton<PlayerBag>
{
    [Header("Bag Settings")]
    [SerializeField] private BagAssetConfig _starterBagConfig;
    [SerializeField] private Material _bagMaterial;

    private BagAssetConfig _currentBagConfig;

    void Awake()
    {
        _currentBagConfig = _starterBagConfig;
        UpdateBagVisuals();
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
    }

    #endregion
}
