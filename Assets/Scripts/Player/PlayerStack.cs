using System.Collections.Generic;
using UnityEngine;

public class PlayerStack : Singleton<PlayerStack>
{
    [Header("Stack Settings")]
    [SerializeField] private Transform _stackBag;
    [SerializeField] private string _stackLayerName = "Stack";
    [SerializeField] private float _stackHeightOffset = 1f;
    [SerializeField] private float _moveThreshold = 0.01f;
    [SerializeField] private float _inertiaSpeed = 8f;

    private int _stackLayer;
    private List<Transform> _stackedEnemies;

    private Quaternion _targetUpRotation;
    private Transform _currentStackedEnemy;
    private Vector3 _targetPosition;

    void Start()
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
        if (_stackedEnemies.Count >= PlayerBag.Instance.BagCapacity)
        {
            ToastMessage.Instance.Show("Bag is full!\nUpgrade your bag to carry more enemies!");
            enemy.GetComponent<EnemyController>().SetRagdollEnabled(false, true);
            enemy.GetComponent<EnemyMovement>().SetMovement(true);
            return;
        }

        enemy.GetComponent<EnemyController>().SetRagdollEnabled(false, false);
        enemy.tag = "Untagged";
        enemy.gameObject.layer = _stackLayer;

        _stackedEnemies.Add(enemy);
        WalletSystem.Instance.AddMoney(1);
    }

    public void RemoveEnemies(int quantity)
    {
        if (quantity <= 0 || _stackedEnemies.Count == 0) return;

        int removeCount = Mathf.Min(quantity, _stackedEnemies.Count);
        for (int i = 0; i < removeCount; i++)
        {
            Transform enemyToRemove = _stackedEnemies[_stackedEnemies.Count - 1];
            Destroy(enemyToRemove.gameObject);
            _stackedEnemies.RemoveAt(_stackedEnemies.Count - 1);
        }
    }

    public int StackedEnemies => _stackedEnemies.Count;
    
    #endregion
}
