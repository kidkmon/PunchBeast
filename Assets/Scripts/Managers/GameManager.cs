using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Game Settings")]
    [SerializeField] private GameObject _upgradablesArea;

    public bool GameStarted = false;

    private int _currentAvailableUpgradeAreas;

    void Start()
    {
        GameStarted = false;
        _currentAvailableUpgradeAreas = _upgradablesArea.transform.childCount;
        WalletSystem.Instance.Initialize();
        PlayerBag.Instance.Initialize();
    }

    void Update()
    {
        if (_currentAvailableUpgradeAreas <= 0 && GameStarted)
        {
            EndGame();
            HUDManager.Instance.ShowEndScreen();
        }
    }

    public void StartGame()
    {
        GameStarted = true;
    }

    public void EndGame()
    {
        GameStarted = false;
        AudioManager.Instance.PlayWinSound();
    }

    public void RemoveUpgradeArea()
    {
        _currentAvailableUpgradeAreas--;
    }
}