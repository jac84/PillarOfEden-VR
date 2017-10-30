﻿using System.Collections;
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
    private int spellIndex = 0;
    private bool shieldActivated;
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
    private void Update()
    {
        CheckShield();
        playerBeads.UpdateHPMP();
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
            if (spellIndex < availableSpells.Length - 1)
                spellIndex++;
            else
                spellIndex = 0;
            currentSpell.GetGesture().gameObject.SetActive(false);
            currentSpell.DeactivateSpell();
            currentSpell = availableSpells[spellIndex];
            currentSpell.GetGesture().gameObject.SetActive(true);
        }
    }
    private void CheckShield()
    {
        if (controllerEvents != null && Camera.main)
        {
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(controllerEvents.gameObject.transform.position);
            Vector3 controllerRot = controllerEvents.gameObject.transform.localRotation.eulerAngles;
            if ((screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1))
            {
                if (((controllerRot.x > 0 && controllerRot.x < 30) || (controllerRot.x > 330))
                && (controllerRot.y > 30 && controllerRot.y < 160)
                && (controllerRot.z > 110 && controllerRot.z < 260))
                {
                    if (!shieldActivated)
                    {
                        shieldActivated = true;
                        playerBeads.SetCanTakeDamage(false);
                        Debug.Log("Shield Activated");
                        return;
                    }
                }
                else
                {
                    if (shieldActivated)
                    {
                        shieldActivated = false;
                        playerBeads.SetCanTakeDamage(true);
                        Debug.Log("Shield Deactivated");
                    }
                }
            }
            else
            {
                if (shieldActivated)
                {
                    shieldActivated = false;
                    playerBeads.SetCanTakeDamage(true);
                    Debug.Log("Shield Deactivated");
                }
            }
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
