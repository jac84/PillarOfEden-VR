using UnityEngine;
using System.Collections;

public class ProjectileArc : BehaviorAbstract
{    
    private Vector3 start;
    private Vector3 p0;
    private Vector3 p1;
    private Vector3 p2;
    private Vector3 targetPos;
    private float journeyLength;
    private float fracJourney;
    private float startTime;
    private int arcDirection;
    private Vector3 origin;
    private float arcAngle;
    private float arcHeight;
    private bool SeekTarget = false;

    [Header("Projectile Behavior")]
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private bool IsHoming;

    private void Awake()
    {
        protoType.OnReturnToPool += Die;
    }
    private void FixedUpdate()
    {
        if (SeekTarget)
        {
            if (IsHoming && target != null)
                targetPos = target.position;
            float distCovered = (Time.time - startTime) * speed;
            journeyLength = Vector3.Distance(start, targetPos);
            fracJourney = distCovered / journeyLength;
            p0 = Vector3.Lerp(start, p1, fracJourney);
            p2 = Vector3.Lerp(p1, targetPos, fracJourney);
            transform.position = Vector3.Lerp(p0, p2, fracJourney);
            if (fracJourney >= 1)
                GetComponent<Prototype>().ReturnToPool();
        }
    }

    public void SeekTargetArc(GameObject target, float ArcHeight, float ArcAngle,bool isHoming)
    {
        SeekTarget = true;
        this.target = target.transform;
        targetPos = this.target.position;
        arcAngle = ArcAngle;
        arcHeight = ArcHeight;
        this.IsHoming = isHoming;
        SetVariables();
    }
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    private void SetVariables()
    {
        start = transform.position;
        startTime = Time.time;
        arcDirection = Random.Range(0, 2);
        origin = (target.position + start) / 2;
        p1 = new Vector3(origin.x, origin.y + arcHeight, origin.z);
        //Solve for Angle of Rotation
        p1.x = ((p1.x - origin.x) * Mathf.Cos(arcAngle) - ((p1.y - origin.y) * Mathf.Sin(arcAngle))) + origin.x;
        p1.y = ((p1.y - origin.y) * Mathf.Cos(arcAngle) + ((p1.x - origin.x) * Mathf.Sin(arcAngle))) + origin.y;
    }
    private void Die()
    {
        SeekTarget = false;
        arcAngle = 0;
        arcHeight = 0;
        target = null;
        targetPos = Vector3.zero;
        p0 = Vector3.zero;
        p2 = Vector3.zero;
    }
}
