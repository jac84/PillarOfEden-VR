using UnityEngine;
using System.Collections;

public abstract class Tower : MonoBehaviour, IHealth
{
    protected bool readyToAttack;
    protected float nextAtkTime;
    [Header("Tower Attributes")]
    [SerializeField] protected float damage;
    [SerializeField] protected float maxHp;
    [SerializeField] protected float currentHP;
    [SerializeField] protected float atkDelay;
    [SerializeField] protected Prototype projectile;
    [SerializeField] protected GameObject target;
    [SerializeField] protected Transform projectileStart;
    [SerializeField] protected float bulletSpeed;


    private void Awake()
    {
        currentHP = maxHp;
    }
    public void TakeDamage(float amount)
    {
        currentHP -= amount;
        //Check If Dead
        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected void Update()
    {
        StartAttack();
    }
    protected void StartAttack()
    {
        if (target)
        {
            if (readyToAttack)
            {
                readyToAttack = false;
                Attack();
                nextAtkTime = Time.time + atkDelay;
            }
        }
        if (!readyToAttack)
        {
            if (Time.time >= nextAtkTime)
            {
                readyToAttack = true;
            }
        }
    }
    protected abstract void Attack();

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
    public GameObject GetTarget()
    {
        return target;
    }

}
