using UnityEngine;
using System.Collections;

public class AoE : MonoBehaviour
{
    [SerializeField] private Transform Center;
    [SerializeField] private float ExpansionRate;

    private BoxCollider bc;

    private void Awake()
    {
        bc = GetComponent<BoxCollider>();
    }
    private void Start()
    {
        if(Center)
            transform.position = new Vector3 (Center.position.x,
                Center.position.y - (Center.localScale.y/2),
                Center.position.z);
    }
    private void Update()
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
        transform.localScale = new Vector3(transform.localScale.x + ExpansionRate * Time.deltaTime,
            transform.localScale.y + ExpansionRate * Time.deltaTime,
            transform.localScale.z + ExpansionRate * Time.deltaTime);
        transform.position = new Vector3(transform.position.x,
            transform.position.y + ExpansionRate / 2 * Time.deltaTime,
            transform.position.z);
    }
}
