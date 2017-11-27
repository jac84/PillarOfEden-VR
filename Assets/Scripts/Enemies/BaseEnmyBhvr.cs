using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnmyBhvr : MonoBehaviour {

    public bool rangedAtks;
    public float range;
    public float meleeRange;
    public int damage;

    private Transform self;
    private Transform myTarget;  
    private AIPath myPath;
    [SerializeField] private SphereCollider myCollide;
    private bool pathChange;
    private float distFromMe;
	// Use this for initialization
	void Start () {
        myPath = GetComponent<AIPath>();
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
    }
    public void SetTarget(Transform t)
    {
        myPath.target = t;
        myTarget = t;
        myPath.repathRate = 1.0f; // Because the player can teleport, our enemies tracking the player will need to track them faster
        pathChange = true;

    }
    public AIPath GetAIPath()
    {
        return myPath;
    }
    public bool GetPathChange()
    {
        return pathChange;
    }
    public void SetPathChange(bool b)
    {
        pathChange = b;
    }
}
