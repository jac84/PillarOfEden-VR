/*******************************************************
 * Copyright (C) 2016 Ngan Do - dttngan91@gmail.com
 *******************************************************/
using UnityEngine;
using System.Collections;
using Leap;

namespace LeapMotionSimpleControl
{
    public class PointDown : BehaviorHand
    {
        public Spell assignedSpell;
        // Use this for initialization
        protected void Awake()
        {
            base.Awake();
            CurrentType = GestureManager.GestureTypes.PointDown;
            // add your custom event 
            specificEvent = castSpell;
            //_gestureManager.TimeBetween2Gestures = 1;
        }

        protected override bool checkConditionGesture()
        {
            Hand hand = GetSupportedHand();
            if (hand != null)
            {
                if (_gestureManager.isFingerDirection(hand,Finger.FingerType.TYPE_INDEX,Vector3.down,deltaAngleThumb) && _gestureManager.isFingerExtended(hand,Finger.FingerType.TYPE_THUMB))
                {
                    return true;
                }
            }
            return false;
        }

        void castSpell()
        {
            Debug.Log("Smite Gesture Cast");
            assignedSpell.ActivateSpell();
        }
    }
}