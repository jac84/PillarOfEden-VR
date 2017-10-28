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
    [SerializeField] private Spell[] availableSpells;
    [SerializeField] private int spellIndex =0;
    public Transform rightHandPosition;
    public Transform leftHandPosition;
    public Transform spellDirection;

    [SerializeField] private GameObject Target;

    void Start()
    {
        AssignControllerEvents();
        if (availableSpells.Length > 0)
        {
            currentSpell = availableSpells[spellIndex];
            currentSpell.GetGesture().gameObject.SetActive(true);
        }
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
        controllerEvents.GripPressed += new ControllerInteractionEventHandler(LockOn);
        controllerEvents.TriggerPressed += new ControllerInteractionEventHandler(CycleSpells);
    }

    private void LockOn(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("TriggerPressed, Locking on...");
        Target = GamManager.singleton.mainVRCamera.GetComponent<CameraRayCaster>().LockOnEnemy();
    }
    private void CycleSpells(object sender, ControllerInteractionEventArgs e)
    {
        if (availableSpells.Length > 0)
        {
            if (spellIndex < availableSpells.Length -1)
                spellIndex++;
            else
                spellIndex = 0;
            currentSpell.GetGesture().gameObject.SetActive(false);
            currentSpell.DeactivateSpell();
            currentSpell = availableSpells[spellIndex];
            currentSpell.GetGesture().gameObject.SetActive(true);
        }
    }
    public GameObject GetTarget()
    {
        return Target;
    }
    public void SetTarget(GameObject target)
    {
        Target = target;
    }
}
