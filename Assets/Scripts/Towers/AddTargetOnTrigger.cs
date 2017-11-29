using UnityEngine;
using System.Collections;

public class AddTargetOnTrigger : MonoBehaviour
{

    [SerializeField] private Tower tower;
    [SerializeField] private LayerMask targetLayer;
    void OnTriggerStay(Collider other)
    {
        if (((1 << other.gameObject.layer) & targetLayer) != 0)
        {
            if (tower.GetTarget() == null)
            {
                tower.SetTarget(other.gameObject);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & targetLayer) != 0)
        {
            if (tower.GetTarget() == other.gameObject)
            {
                tower.SetTarget(null);
            }
        }
    }
}
