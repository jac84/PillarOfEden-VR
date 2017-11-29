using UnityEngine;
using System.Collections;

public class AoEGrow : BehaviorAbstract
{
    private Vector3 startSize;
    private BoxCollider bc;

    [Space(10)]
    [Header("AoE Behavior")]
    [SerializeField] private Transform Center;
    [SerializeField] private float ExpansionRate;
    [SerializeField] private Vector3 ExpansionVector;

    private void Awake()
    {
        protoType.OnReturnFromPool += Set;
        bc = GetComponent<BoxCollider>();
        transform.parent = null;
    }
    private void Start()
    {
        transform.parent = GamManager.singleton.poolManager.transform;
        startSize = transform.localScale;
        if (Center)
            transform.position = new Vector3(Center.position.x,
                Center.position.y - (Center.localScale.y / 2),
                Center.position.z);
    }
    private void FixedUpdate()
    {
        //Collider only
        /*
        bc.size = new Vector3(bc.size.x + ExpansionRate * Time.deltaTime, 
            bc.size.y + ExpansionRate * Time.deltaTime, 
            bc.size.z + ExpansionRate * Time.deltaTime);
        bc.center = new Vector3(bc.center.x,
            bc.center.y + ExpansionRate/2 * Time.deltaTime,
            bc.center.z);
        */
        //The Mesh
        transform.localScale = new Vector3(transform.localScale.x + ExpansionVector.x * Time.deltaTime * ExpansionRate,
            transform.localScale.y + ExpansionVector.y * Time.deltaTime * ExpansionRate,
            transform.localScale.z + ExpansionVector.z * Time.deltaTime * ExpansionRate);

    }
    public void SetExpansionRate(float rate)
    {
        ExpansionRate = rate;
    }
    public void SetExapnsionVector(Vector3 vec)
    {
        ExpansionVector = vec;
    }
    public void SetCenter(Transform t)
    {
        Center = t;
    }
    protected override void Set()
    {
        base.Set();
        transform.localScale = startSize;
    }
}
