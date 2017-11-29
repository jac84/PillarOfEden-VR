using UnityEngine;
using System.Collections;

public class Cannon : Tower
{
    [Header("Projectile Behavior")]
    [SerializeField] private float shootAngle;
    public static Vector3 BallisticVel(Transform target, float angle, Transform transform, float bulletSpeed)
    {
        Vector3 dir = target.position - transform.position;  
        float h = dir.y;  
        dir.y = 0;  
        float dist = dir.magnitude;  
        float a = angle * Mathf.Deg2Rad;  
        dir.y = dist * Mathf.Tan(a); 
        dist += h / Mathf.Tan(a);  
        float vel = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        Vector3 v = vel * dir.normalized;
        if(float.IsNaN(v.x))
        {
            dir.y = a;
            return (new Vector3(dir.normalized.x * bulletSpeed, dir.y * bulletSpeed,dir.normalized.z * bulletSpeed));
        }
        return v;
    }
    protected override void Attack()
    {
        OnTriggerAoE cannon = projectile.Instantiate<OnTriggerAoE>();
        cannon.transform.position = projectileStart.position;
        cannon.SetDamage(damage);
        cannon.gameObject.GetComponent<Rigidbody>().velocity = BallisticVel(target.transform, shootAngle,transform,bulletSpeed);
    }
}
