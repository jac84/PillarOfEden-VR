using UnityEngine;
using System.Collections;

public class SimpleEnemy : Enemy
{

    public override void Attack()
    {
        IHealth h = null;
        h = currentTarget.GetComponent<IHealth>();
        if (h != null)
        {
            h.TakeDamage(damage);
            Debug.Log("Enemy Attacked");
        }
    }
    public override void UpdateEnemyMovement()
    {
        //Put Enemy Movement Update Here

    }

}
