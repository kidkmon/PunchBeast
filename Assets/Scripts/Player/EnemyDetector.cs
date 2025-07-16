using UnityEngine;
using UnityEngine.Events;

public class EnemyDetector : MonoBehaviour
{
    [Header("Enemy Detection Settings")]
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _detectionRange = 5f;

    public UnityEvent OnEnemyDetected;

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _detectionRange, _enemyLayer))
        {
            if (hit.collider.transform.GetComponent<EnemyController>().IsAttackable())
            {
                hit.collider.transform.GetComponent<EnemyController>().TakePunch();
                OnEnemyDetected?.Invoke();
            }
        }

    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * _detectionRange);
    }
}
