using UnityEngine;

public enum BagType
{
    Basic,
    Medium,
    Large,
}

[CreateAssetMenu(fileName = "UpgradeBagAssetConfig", menuName = "Scriptable Objects/UpgradeBagAssetConfig")]
public class BagAssetConfig : ScriptableObject
{
    [Header("Bag Settings")]
    [SerializeField] int _upgradeCost;
    [SerializeField] int _bagCapacity;
    [SerializeField] Color _bagColor;

    public int UpgradeCost => _upgradeCost;
    public int BagCapacity => _bagCapacity;
    public Color BagColor => _bagColor;
}
