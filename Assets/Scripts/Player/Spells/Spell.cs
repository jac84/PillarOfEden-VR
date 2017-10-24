using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeapMotionSimpleControl;

public abstract class Spell : MonoBehaviour
{
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected VRPlayer player;
    [SerializeField] protected int MPCost;
    [SerializeField] protected BehaviorHand handGesture;

    public abstract void ActivateSpell();
    public abstract void DeactivateSpell();
    public BehaviorHand GetGesture()
    {
        return handGesture;
    }
}
