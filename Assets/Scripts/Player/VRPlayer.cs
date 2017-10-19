using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class VRPlayer : MonoBehaviour
{
    // Use this for initialization
    [SerializeField] private VRTK_BasicTeleport teleport;
    [SerializeField] private Spell currentSpell;
    [SerializeField] private PlayerHPMP playerBeads;
    [SerializeField] private VRTK_ControllerEvents controllerEvents;

    [SerializeField] private GameObject Target;

    void Start()
    {
        AssignControllerEvents();
    }
    public void UpdatePlayer()
    {
    }
    public Spell GetCurrentSpell()
    {
        return currentSpell;
    }
    public PlayerHPMP GetBeads()
    {
        return playerBeads;
    }

    //Assign Controller Events
    void AssignControllerEvents()
    {
        controllerEvents.TriggerPressed += new ControllerInteractionEventHandler(LockOn);
    }

    private void LockOn(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("TriggerPressed, Locking on...");
        GamManager.singleton.mainVRCamera.GetComponent<CameraRayCaster>().LockOnEnemy();
    }
}
