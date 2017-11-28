using UnityEngine;
using System.Collections;

public class BaseEnemyAttack : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float attackRange;

    private float lastTimeAttacked;
    private void Start()
    {
        lastTimeAttacked = Time.time;
    }
    public virtual void Attack(GameObject target)
    {
        if (lastTimeAttacked < Time.time)
        {
            Debug.Log("Enemy is Attacking Player");
            IHealth hp = null;
            hp = target.GetComponent<IHealth>();
            if (hp != null)
            {
                hp.TakeDamage(damage);
            }
            lastTimeAttacked = Time.time + attackSpeed;
        }
    }
    public float GetAttackRange()
    {
        return attackRange;
    }
}
