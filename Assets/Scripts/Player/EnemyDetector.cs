using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDetector : MonoBehaviour
{
    [Header("Enemy Detection Settings")]
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _detectionRange = 5f;

    [Header("Player Stack Settings")]
    [SerializeField] PlayerStack playerStack;

    [Header("Events")]
    public UnityEvent OnEnemyDetected;

    private bool _isEnemyDetected;

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _detectionRange, _enemyLayer))
        {
            if (!_isEnemyDetected && hit.collider.transform.GetComponent<EnemyController>().IsAttackable())
            {
                StartCoroutine(PunchEnemy(hit.collider.gameObject));
            }
        }

    }

    private IEnumerator PunchEnemy(GameObject enemy)
    {
        _isEnemyDetected = true;
        OnEnemyDetected?.Invoke();
        enemy.GetComponent<EnemyController>().TakePunch();
        yield return new WaitForSeconds(1f);
        playerStack.StackCharacter(enemy.transform);
        _isEnemyDetected = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * _detectionRange);
    }
}
