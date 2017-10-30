using UnityEngine;
using System.Collections;

public class Smite : Tower
{
    [Header("Projectile Behavior")]
    [SerializeField] private Vector3 expansionVector;
    protected override void Attack()
    {
        AoE aoe = projectile.Instantiate<AoE>();
        aoe.SetSize(expansionVector);
        aoe.gameObject.transform.position = projectileStart.position;
        aoe.SetDieTime(bulletSpeed);
        aoe.SetDamage(damage);
    }

}
