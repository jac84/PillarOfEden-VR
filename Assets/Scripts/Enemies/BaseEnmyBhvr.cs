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
    private Vector3 lastBestPosition;
    private Vector3 queryNewPosition;
    private AIPath myPath;
    private SphereCollider myCollide;
    private bool pathChange;
    private float distFromMe;
    private float distFromLast;
    private float myStartSpeed;

	// Use this for initialization
	void Start () {
        myPath = GetComponent<AIPath>();
        myStartSpeed = myPath.speed;
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
        // Turns out I completely overlooked this, since this will continually run infinitley if VR-Player gets too far away.
        if (pathChange && (distFromMe > targetCallibration))
        {
            queryNewPosition = myTarget.position;
            distFromLast = Vector3.Distance(lastBestPosition, queryNewPosition);
            // New fix, should be what Zak suggested and compare the last known position of the target and the current location
            // if that is greater than the threshold along with being further away from the enemy, then repath. So more conditions 
            // should allow it to play out smoother. Ran a quick test with more enemies, and it seemed to not get stuck for me.
            if (distFromLast > targetCallibration)
            {
                lastBestPosition = myTarget.position;
                myPath.SearchPath();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject bruh = other.gameObject;       
        Transform potTarget = other.gameObject.GetComponent<Transform>();
        if (bruh.name == "Player" && pathChange == false) // We will need to refactor some things, such as setting tags for the player object
        {                           // and also tags for the tower object it should make the 
            myPath.target = potTarget;
            myTarget = potTarget;
            lastBestPosition = myTarget.position;
            myPath.repathRate = 1.0f; // Because the player can teleport, our enemies tracking the player will need to track them faster
            myPath.speed = 0;
            pathChange = true;
            myPath.SearchPath();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        GameObject bruh = other.gameObject;
        Transform potTarget = other.gameObject.GetComponent<Transform>();
        if (bruh.name == "Player" && pathChange == true) 
        {
            myPath.speed = myStartSpeed;
        }

}
