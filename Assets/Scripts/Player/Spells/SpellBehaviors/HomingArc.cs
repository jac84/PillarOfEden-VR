using UnityEngine;
using System.Collections;
using Leap;


public class HomingArc : MonoBehaviour
{
    [SerializeField] private bool isHoming = false;
    [SerializeField] private float arc;
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 start;

    [SerializeField] private Vector3 p0;
    [SerializeField] private Vector3 p2;

    private float journeyLength;
    private float fracJourney;

    private float startTime;
    private int arcDirection;

    private void Start()
    {
        start = transform.position;
        startTime = Time.time;        
        arcDirection = Random.Range(0, 2);
    }
    private void Update()
    {
        if(isHoming)
        {
            if (fracJourney <= 1.0f)
            {
                Vector3 p1 = (target.position + start) / 2;
                if (arcDirection == 0)
                {
                    p1 += transform.right.normalized * -arc;
                }
                else
                {
                    p1 += transform.right.normalized * arc;
                }
                p1.y += arc;

                float distCovered = (Time.time - startTime) * speed;
                journeyLength = Vector3.Distance(start, target.transform.position);
                fracJourney = distCovered / journeyLength;
                p0 = Vector3.Lerp(start, p1, fracJourney);
                p2 = Vector3.Lerp(p1, target.position, fracJourney);
                transform.position = Vector3.Lerp(p0, p2, fracJourney);
                
            }
        }

    }

    public void SeekTarget(GameObject target)
    {
        isHoming = true;
        this.target = target.transform;
    }
}

