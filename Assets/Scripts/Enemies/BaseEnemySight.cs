using UnityEngine;
using System.Collections;

public class BaseEnemySight : MonoBehaviour
{
    [SerializeField] private BaseEnmyBhvr baseEnemyBehavior;
    private void OnTriggerEnter(Collider other)
    {
        if (GamManager.singleton.GetVRPlayer().gameObject == other.gameObject && baseEnemyBehavior.GetPathChange() == false) // We will need to refactor some things, such as setting tags for the player object
        {                                                                               // and also tags for the tower object it should make the 
            Debug.Log("Enemy Found Player");
            baseEnemyBehavior.SetTarget(GamManager.singleton.GetVRPlayer().transform);
        }
    }
}
