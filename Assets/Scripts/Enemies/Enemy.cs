using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
public abstract class Enemy : MonoBehaviour
{

    [SerializeField] private InteractableObject interactable;
    void Awake()
    {
        interactable.OnOver += HoverOver;
        interactable.OnOut += HoverOut;
    }
    public abstract void UpdateEnemyMovement();

    //On HoverOver and HoverOut manipulate reticle
    public void HoverOver()
    {
        GamManager.singleton.mainVRCamera.GetComponent<Reticle>().ChangeReticleColor(new Color(200,0,0));
    }
    public void HoverOut()
    {
        GamManager.singleton.mainVRCamera.GetComponent<Reticle>().ChangeReticleColor(new Color(255,255,255));
    }
}
