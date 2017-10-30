using UnityEngine;
using System.Collections;

public class AoE : BehaviorAbstract
{
    public void SetSize(Vector3 s)
    {
        transform.localScale = s;
    }
}
