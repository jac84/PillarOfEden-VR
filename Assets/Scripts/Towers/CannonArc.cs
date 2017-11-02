using UnityEngine;
using System.Collections;

public class CannonArc : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    private Vector3 BallisticVel(Transform target, float angle)
    {
        var dir = target.position - transform.position;  // get target direction
        var h = dir.y;  // get height difference
        dir.y = 0;  // retain only the horizontal direction
        var dist = dir.magnitude;  // get horizontal distance
        var a = angle * Mathf.Deg2Rad;  // convert angle to radians
        dir.y = dist * Mathf.Tan(a);  // set dir to the elevation angle
        dist += h / Mathf.Tan(a);  // correct for small height differences
                                   // calculate the velocity magnitude
        var vel = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return vel * dir.normalized;
    }

    Transform myTarget;  // drag the target here
    Prototype cannonball; // drag the cannonball prefab here
    float shootAngle = 30;  // elevation angle
 
    private void Update()
    {
        if (Input.GetKeyDown("b"))
        {  // press b to shoot
            CannonArc cannon = cannonball.Instantiate<CannonArc>();
            cannon.gameObject.GetComponent<Rigidbody>().velocity = BallisticVel(myTarget, shootAngle);
            Destroy(cannon, 10);
        }
    }
}
