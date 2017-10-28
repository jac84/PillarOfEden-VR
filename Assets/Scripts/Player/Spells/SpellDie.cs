using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDie : MonoBehaviour {

    [SerializeField] private float dieTime;


    void Update()
    {
        Destroy(gameObject, dieTime);
    }
}
