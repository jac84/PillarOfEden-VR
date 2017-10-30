using UnityEngine;
using System.Collections;

public class AddTargetOnTrigger : MonoBehaviour
{

    [SerializeField] private Tower tower;
    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            if (tower.GetTarget() == null)
            {
                tower.SetTarget(other.gameObject);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            if (tower.GetTarget() == other.gameObject)
            {
                tower.SetTarget(null);
            }
        }
    }
}
