using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnmyBhvr : MonoBehaviour {

    public bool rangedAtks;
    public float range;
    public float meleeRange;
    public int damage;
    public float targetCallibration;

    private Transform self;
    private Transform myTarget;  
    private AIPath myPath;
    private SphereCollider myCollide;
    private bool pathChange;
    private float distFromMe;
	// Use this for initialization
	void Start () {
        myPath = GetComponent<AIPath>();
        myCollide = GetComponent<SphereCollider>();
        pathChange = false;
        myTarget = myPath.target;
        self = GetComponent<Transform>();
	}

    void Update()
    {
        distFromMe = Vector3.Distance(self.position, myTarget.position);
        if ((rangedAtks && distFromMe < range) || distFromMe < meleeRange )
        {
            // Deal damage to the target equal to damage var
            // Something like, myTarget.GetComponent<SomeScript>().takeDamage(damage) or whatever will work
        }

        // Once the pathChange happens, it will then compare the distance to a targetCallibratoin variable for when to
        // repath by itself, essentially a distance threshold to repath when passed.
        if (pathChange && (distFromMe > targetCallibration))
        {
            myPath.SearchPath();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject bruh = other.gameObject;       
        Transform potTarget = other.gameObject.GetComponent<Transform>();
        if (bruh.tag == "Player" && pathChange == false) // We will need to refactor some things, such as setting tags for the player object
        {                           // and also tags for the tower object it should make the 
            Debug.Log("ASD");
            myPath.target = potTarget;
            myTarget = potTarget;
            myPath.repathRate = 1.0f; // Because the player can teleport, our enemies tracking the player will need to track them faster
            pathChange = true;
            myPath.SearchPath();
        }
    }
    
}
