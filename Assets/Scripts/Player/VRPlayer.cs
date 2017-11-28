using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class VRPlayer : MonoBehaviour
{
    // Use this for initialization
    [SerializeField] private Spell currentSpell;
    [SerializeField] private PlayerHPMP playerBeads;
    [SerializeField] private VRTK_ControllerEvents controllerEvents;
    [SerializeField] private Spell[] availableSpells;
    [SerializeField] private GameObject shield;
    [Space(10)]
    [SerializeField]
    private GameObject Target;

    [Space(10)]
    [SerializeField]
    private PlayerInventory playerInventory;

    private int spellIndex = 0;
    private bool shieldActivated;
    private bool inventoryOpen;
    public Transform rightHandPosition;
    public Transform leftHandPosition;
    public Transform spellDirection;


    void Start()
    {
        AssignControllerEvents();
        if (availableSpells.Length > 0)
        {
            currentSpell = availableSpells[spellIndex];
            currentSpell.GetGesture().gameObject.SetActive(true);
            if (!GamManager.singleton.IsLeftHanded())
            {
                Debug.Log(rightHandPosition.parent.parent.parent.GetChild(1).name);
                rightHandPosition.parent.parent.parent.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = currentSpell.GetHandMaterial();
            }

        }
        GamManager.singleton.mainVRCamera = spellDirection.gameObject.GetComponent<Camera>();
        GamManager.singleton.player = gameObject.GetComponent<VRPlayer>();
    }
    private void Update()
    {
        CheckShield();
        CheckInventory();
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
        controllerEvents.ButtonOnePressed += new ControllerInteractionEventHandler(HandleButtonOnePress);
    }

    private void HandleButtonOnePress(object sender, ControllerInteractionEventArgs e)
    {
        if(inventoryOpen)
        {
            playerInventory.UseCurrentItem();
        }
    }
    private void LockOn(object sender, ControllerInteractionEventArgs e)
    {
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
            currentSpell.GetGesture().ChangeTimeBetweenGestures();
            if(!GamManager.singleton.IsLeftHanded())
            {
                rightHandPosition.parent.parent.parent.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = currentSpell.GetHandMaterial();
            }
        }
    }
    private void CheckShield()
    {
        if (controllerEvents != null && Camera.main)
        {
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(controllerEvents.gameObject.transform.position);
            Vector3 controllerRot = controllerEvents.gameObject.transform.forward;       
            if ((screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1))
            {
                if (((controllerEvents.gameObject.transform.forward - GamManager.singleton.mainVRCamera.gameObject.transform.right).sqrMagnitude < 0.3f) &&
                    ((controllerEvents.gameObject.transform.right - GamManager.singleton.mainVRCamera.gameObject.transform.forward).sqrMagnitude < 0.3f))
                {
                    playerBeads.SpendMana(.01f);
                    if (!shieldActivated)
                    {
                        shieldActivated = true;
                        shield.SetActive(shieldActivated);
                        return;
                    }
                }
                else
                {
                    if (shieldActivated)
                    {
                        shieldActivated = false;
                        shield.SetActive(shieldActivated);
                    }
                }
            }
            else
            {
                if (shieldActivated)
                {
                    shieldActivated = false;
                    shield.SetActive(shieldActivated);
                }
            }
        }
    }

    private void CheckInventory()
    {
        if (controllerEvents != null && Camera.main)
        {
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(controllerEvents.gameObject.transform.position);
            if ((screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1))
            {
                if ((controllerEvents.gameObject.transform.right - GamManager.singleton.mainVRCamera.gameObject.transform.up).sqrMagnitude < 0.3f)
                {
                    if (!inventoryOpen && playerInventory.GetItemCount() > 0)
                    {
                        inventoryOpen = playerInventory.OpenInventory();
                    }
                    else
                    {
                        if (controllerEvents.GetTouchpadAxis().x > 0 && controllerEvents.touchpadAxisChanged)
                        {
                            playerInventory.NextItemInInventory();
                        }
                        else if (controllerEvents.GetTouchpadAxis().x < 0 && controllerEvents.touchpadAxisChanged)
                        {
                            playerInventory.PreviousItemInInventory();
                        }
                    }
                }
                else
                {
                    if (inventoryOpen)
                    {
                        inventoryOpen = false;
                        playerInventory.CloseInventory();
                        Debug.Log("Inventory Closed");
                    }
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
    public bool GetShieldActivated()
    {
        return shieldActivated;
    }
}
