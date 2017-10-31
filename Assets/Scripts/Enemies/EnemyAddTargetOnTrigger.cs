using UnityEngine;
using System.Collections;

public class EnemyAddTargetOnTrigger : MonoBehaviour
{
    /**Need to redo Entire Enemy structure*/
    [SerializeField] private Enemy Enemy;
    [SerializeField] private float distanceUntilStopAttacking;
    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<VRPlayer>() || other.GetComponent<Pillar>())
        {
            if (Enemy.GetTarget() == null)
            {
                Enemy.SetTarget(other.gameObject);
            }
        }
    }
    private void Update()
    {
        GameObject target = Enemy.GetTarget();
        if (target)
        {
            if((target.transform.position - transform.position).sqrMagnitude > distanceUntilStopAttacking)
            {
                Enemy.SetTarget(null);
            }
        }
    }
}
