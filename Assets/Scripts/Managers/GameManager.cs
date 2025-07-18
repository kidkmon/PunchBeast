using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Header("Game Settings")]
    [SerializeField] private GameObject _upgradablesArea;

    public bool GameStarted { get; private set; } = false;

    private int _currentAvailableUpgradeAreas;
    void Awake()
    {
        GameStarted = false;
    }

    void Start()
    {
        _currentAvailableUpgradeAreas = _upgradablesArea.transform.childCount;
        WalletSystem.Instance.Initialize();
        PlayerBag.Instance.Initialize();
    }

    void Update()
    {
        if (_currentAvailableUpgradeAreas <= 0 && GameStarted)
        {
            GameStarted = false;
            HUDManager.Instance.ShowEndScreen();
        }
    }

    public void StartGame()
    {
        GameStarted = true;
    }

    public void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RemoveUpgradeArea()
    {
        _currentAvailableUpgradeAreas--;
    }
}