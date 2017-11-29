using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnTriggerAoE : BehaviorAbstract
{
    [Header("AoE Behavior")]
    [SerializeField] private Prototype aoeObject;
    [SerializeField] private Vector3 expansionVector;
    [SerializeField] private float expansionRate;
    [SerializeField] private float AoEDieTime;
    [SerializeField] private float splashDamage;

    void OnTriggerEnter(Collider other)
    {
        AoEGrow a = aoeObject.Instantiate<AoEGrow>();
        a.SetDamage(splashDamage);
        a.SetExapnsionVector(expansionVector);
        a.SetExpansionRate(expansionRate);
        a.gameObject.transform.position = transform.position;
        a.SetDieTime(AoEDieTime);
    }

}
