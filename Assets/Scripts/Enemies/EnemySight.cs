using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour {

    [SerializeField] private SphereCollider myCollide;
    [SerializeField] private BaseEnmyBhvr myBehave;

    private void OnTriggerEnter(Collider other)
    {
        myBehave.OnEnterSight(other);
    }
    private void OnTriggerExit(Collider other)
    {
        myBehave.OnExitSight(other);
    }
}
