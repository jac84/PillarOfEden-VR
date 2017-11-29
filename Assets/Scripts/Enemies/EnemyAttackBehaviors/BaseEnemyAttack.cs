using UnityEngine;
using System.Collections;

public class BaseEnemyAttack : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float attackRange;
    [SerializeField] protected Prototype projectile;
    [SerializeField] protected Transform projectileStart;
    [SerializeField] protected float shootAngle;
    [SerializeField] private float bulletSpeed;

    private float lastTimeAttacked;
    private void Start()
    {
        lastTimeAttacked = Time.time;

    }
    public virtual void Attack(GameObject target)
    {
        if (lastTimeAttacked < Time.time)
        {
            OnTriggerAoE proj = projectile.Instantiate<OnTriggerAoE>();
            proj.transform.parent = GamManager.singleton.poolManager.transform;
            proj.transform.position = projectileStart.position;
            proj.SetDamage(damage);
            proj.gameObject.GetComponent<Rigidbody>().velocity = Cannon.BallisticVel(target.transform, shootAngle, transform, bulletSpeed);
            /*
            IHealth hp = null;
            hp = target.GetComponent<IHealth>();
            if (hp != null)
            {
                hp.TakeDamage(damage,transform.position);
            }
            */

            lastTimeAttacked = Time.time + attackSpeed;
        }
    }
    public float GetAttackRange()
    {
        return attackRange;
    }
}
