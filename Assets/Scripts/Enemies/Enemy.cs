using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using Photon;
public abstract class Enemy : Photon.MonoBehaviour, IEnemyAttack, IHealth
{
    /**Need to redo Entire Enemy structure*/
    [SerializeField] protected float damage;
    [SerializeField] protected float maxHp;
    [SerializeField] protected float currentHp;
    [SerializeField] protected float atkDelay;
    [SerializeField] protected GameObject currentTarget;
    [SerializeField] private InteractableObject interactable;
    [SerializeField] private AIPath path;

    protected bool readyToAttack;
    protected float nextAtkTime;
    //On HoverOver and HoverOut manipulate reticle
    public void HoverOver()
    {
        GamManager.singleton.mainVRCamera.GetComponent<Reticle>().ChangeReticleColor(new Color(200,0,0));
    }
    public void HoverOut()
    {
        GamManager.singleton.mainVRCamera.GetComponent<Reticle>().ChangeReticleColor(new Color(255,255,255));
    }
    private void Start()
    {
        if (interactable != null)
        {
            interactable.OnOver += HoverOver;
            interactable.OnOut += HoverOut;
        }
    }
    public void TakeDamage(float amount,Vector3 origin)
    {
        currentHp -= amount;
        //Check If Dead
        if (currentHp <= 0)
        {
            GamManager.singleton.GetEnemyManager().DespawnEnemy(gameObject);
        }
    }
    public GameObject GetTarget()
    {
        return currentTarget;
    }
    public void SetTarget(GameObject t)
    {
        currentTarget = t;
    }
    public abstract void Attack();
    public abstract void UpdateEnemyMovement();

}
