using UnityEngine;
using System.Collections;

public class Sentry : Tower
{

    protected override void Attack()
    {
        ProjectileArc ha = projectile.Instantiate<ProjectileArc>();
        ha.gameObject.transform.position = projectileStart.position;
        ha.SeekTargetArc(target,0,0,true);
        ha.SetSpeed(bulletSpeed);
        ha.SetDamage(damage);
    }
}
