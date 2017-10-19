using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeapMotionSimpleControl;
using Leap;
public class PrerequisiteArcaneMissile : PrerequisiteSpell
{
    [SerializeField] private float deltaCloseFinger;
    public override bool checkPrerequisite(Hand hand)
    {
        if (gestureManager.checkFingerCloseToHand(hand, deltaCloseFinger))
        {
            Debug.Log("Arcane Missile PreReq Met!");
            return true;
        }
        return false;
    }
    public override bool brokePrerequisite(Hand hand)
    {
        Debug.Log("Arcane Missile PreReq Disengaged!");
        return true;
    }
}
