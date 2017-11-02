using UnityEngine;
using System.Collections;

public abstract class BehaviorAbstract : MonoBehaviour
{
    private float sTime;
    [SerializeField] protected float damage;
    [Header("Projectile LifeSpan")]
    [SerializeField] private float dieTime;
    [SerializeField] protected Prototype protoType;
    [SerializeField] protected bool dieOnHit;

    private void Start()
    {
        protoType.OnReturnFromPool += Set;
        Set();
    }

    private void Update()
    {
        if (sTime + dieTime < Time.time)
        {
            protoType.ReturnToPool();
        }
    }
    /**TODO Redo this*/
    protected virtual void OnTriggerStay(Collider other)
    {
        IHealth h = other.GetComponent<IHealth>();
        if(h != null)
        {
            h.TakeDamage(damage,transform.position);
        }
        if (dieOnHit)
        {
            protoType.ReturnToPool();
        }
    }
    protected virtual void Set()
    {
        sTime = Time.time;
    }
    public void SetDieTime(float t)
    {
        dieTime = t;
    }
    public void SetDamage(float d)
    {
        damage = d;
    }
}
