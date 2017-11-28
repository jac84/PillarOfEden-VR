using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
public class BaseEnmyBhvr : MonoBehaviour
{

    private float range;
    [SerializeField] private float targetCallibration;
    [SerializeField] private float sightRadius;
    private Transform self;
    private Transform myTarget;
    private Vector3 lastBestPosition;
    private Vector3 queryNewPosition;
    private AIPath myPath;
    private bool pathChange;
    private float distFromMe;
    private float distFromLast;
    [SerializeField] protected BaseEnemyAttack enemyAttack;
    [SerializeField] private InteractableObject interactable;

    private float myStartSpeed;

    public void HoverOver()
    {
        GamManager.singleton.mainVRCamera.GetComponent<Reticle>().ChangeReticleColor(new Color(200, 0, 0));
    }
    public void HoverOut()
    {
        GamManager.singleton.mainVRCamera.GetComponent<Reticle>().ChangeReticleColor(new Color(255, 255, 255));
    }
    // Use this for initialization
    void Start()
    {
        myPath = GetComponent<AIPath>();
        myStartSpeed = myPath.speed;
        pathChange = false;
        myTarget = myPath.target;
        self = GetComponent<Transform>();
        range = enemyAttack.GetAttackRange();
        if (interactable != null)
        {
            interactable.OnOver += HoverOver;
            interactable.OnOut += HoverOut;
        }
    }

    void Update()
    {
        distFromMe = Vector3.Distance(self.position, myTarget.position);
        if (distFromMe < range)
        {
            if (enemyAttack)
                enemyAttack.Attack(myTarget.gameObject);
        }

        if ((distFromMe < sightRadius) && myPath.speed > 0)
        {
            myPath.speed = 0;
        }
        else { myPath.speed = myStartSpeed; }
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
                // myPath.speed = myStartSpeed;
                lastBestPosition = myTarget.position;
                myPath.SearchPath();
            }
        }
        
    }
    public void OnEnterSight(Collider other)
    {
        GameObject bruh = other.gameObject;
        Transform potTarget = other.gameObject.GetComponent<Transform>();
        if (bruh.tag == "Player" && pathChange == false) // We will need to refactor some things, such as setting tags for the player object
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
    public void OnExitSight(Collider other)
    {
        GameObject bruh = other.gameObject;
        Transform potTarget = other.gameObject.GetComponent<Transform>();
        if (bruh.tag == "Player" && pathChange == true)
        {
            myPath.speed = myStartSpeed;
        }
    }
    public void TargetReached()
    {
        myPath.speed = 0;
    }
}
