using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDie : MonoBehaviour {

    [SerializeField] private float dieTime;
    [SerializeField] private Prototype protoType;
    [SerializeField] private bool dieOnHit;
    private float startTime;
    private void Start()
    {
        protoType.OnReturnFromPool += Set;
        Set();
    }

    private void Update()
    {
        if(startTime + dieTime < Time.time)
        {
            ReturnToPool();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (dieOnHit)
        {
            protoType.ReturnToPool();
        }
    }
    private void ReturnToPool()
    {
        protoType.ReturnToPool();
    }
    private void Set()
    {
        startTime = Time.time;
    }
    public void SetDieTime(float t)
    {
        dieTime = t;
    }
}
