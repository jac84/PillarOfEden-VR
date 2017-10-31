using UnityEngine;
using System.Collections;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float force;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Enemy>())
        {
            other.GetComponent<Rigidbody>().AddForce((other.transform.position - transform.position).normalized * force, ForceMode.Impulse);
        }
    }
}
