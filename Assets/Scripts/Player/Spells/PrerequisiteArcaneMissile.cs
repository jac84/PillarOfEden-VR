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
            return true;
        }
        return false;
    }
    public override bool brokePrerequisite(Hand hand)
    {
        return true;
    }
}
