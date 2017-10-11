using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Enemy
{

    public DummyMove dummyMove;
    public override void UpdateEnemyMovement()
    {
        if (dummyMove != null)
        {
            dummyMove.Move();
        }
    }
}
