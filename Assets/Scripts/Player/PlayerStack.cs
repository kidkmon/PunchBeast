using System.Collections.Generic;
using UnityEngine;

public class PlayerStack : Singleton<PlayerStack>
{
    [Header("Stack Settings")]
    [SerializeField] private Transform _stackBag;
    [SerializeField] private string _stackLayerName = "Stack";
    [SerializeField] private int _stackCapacity = 5;
    [SerializeField] private float _stackHeightOffset = 1f;
    [SerializeField] private float _moveThreshold = 0.01f;
    [SerializeField] private float _inertiaSpeed = 8f;

    private int _stackLayer;
    private List<Transform> _stackedEnemies;

    private Quaternion _targetUpRotation;
    private Transform _currentStackedEnemy;
    private Vector3 _targetPosition;

    void Awake()
    {
        _stackedEnemies = new List<Transform>();
        _stackLayer = LayerMask.NameToLayer(_stackLayerName);
        _targetUpRotation = Quaternion.Euler(-90, transform.eulerAngles.y, 0);
    }

    void LateUpdate()
    {
        UpdateStack();
    }

    void UpdateStack()
    {
        if (_stackedEnemies.Count == 0) return;

        for (int i = 0; i < _stackedEnemies.Count; i++)
        {
            _currentStackedEnemy = _stackedEnemies[i];

            if (i == 0)
            {
                _targetPosition = _stackBag.position;
            }
            else
            {
                Transform itemBelow = _stackedEnemies[i - 1];
                _targetPosition = itemBelow.position + (Vector3.up * _stackHeightOffset);
            }

            // Avoid Jittering
            if (Vector3.Distance(_currentStackedEnemy.position, _targetPosition) > _moveThreshold)
            {
                _currentStackedEnemy.position = Vector3.Lerp(_currentStackedEnemy.position, _targetPosition, Time.deltaTime * _inertiaSpeed);
            }

            _currentStackedEnemy.rotation = Quaternion.Lerp(_currentStackedEnemy.rotation, _targetUpRotation, Time.deltaTime * _inertiaSpeed);
        }
    }

    #region  Public Methods

    public void StackEnemy(Transform enemy)
    {
        if (_stackedEnemies.Count >= _stackCapacity)
        {
            Debug.Log("Full Stack!");
            return;
        }

        enemy.GetComponent<EnemyController>().SetRagdollEnabled(false, false);
        enemy.tag = "Untagged";
        enemy.gameObject.layer = _stackLayer;

        _stackedEnemies.Add(enemy);
        WalletSystem.Instance.AddMoney(1);
    }

    public void ClearStack()
    {
        foreach (var t in _stackedEnemies)
        {
            Destroy(t.gameObject);
        }
        _stackedEnemies.Clear();
    }

    public int StackCapacity => _stackCapacity;

    public void SetStackCapacity(int capacity)
    {
        _stackCapacity = capacity;
    }
    
    #endregion
}
