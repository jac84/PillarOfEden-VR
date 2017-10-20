using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : IEnemy
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
