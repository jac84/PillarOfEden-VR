using UnityEngine;
using System.Collections;
using Leap;

namespace LeapMotionSimpleControl
{
    public class HandsOpen : BehaviorHand
    {
        public Spell assignedSpell;
        private PrerequisiteSpell containsPrerequisite = null;

        // Use this for initialization
        protected void Awake()
        {
            base.Awake();
            CurrentType = GestureManager.GestureTypes.HandsOpen;
            containsPrerequisite = GetComponent<PrerequisiteSpell>();
            specificEvent = castSpell;
            //_gestureManager.TimeBetween2Gestures = 1;
        }
        protected override bool checkConditionGesture()
        {
            Hand hand = GetSupportedHand();
            if (hand != null)
            {
                if (_gestureManager.isAllFingersExtended(hand))
                {
                    return true;
                }
            }
            return false;
        }
        protected override bool hasPrerequisite()
        {
            if (containsPrerequisite != null)
                return true;
            return false;
        }
        protected override bool checkPrerequisite()
        {
            Hand hand = GetSupportedHand();
            if (hand != null)
            {
                if (containsPrerequisite != null)
                {
                    if (!_prerequisiteMet)
                        _prerequisiteMet = containsPrerequisite.checkPrerequisite(hand);
                    //else
                    //   _prerequisiteMet = !containsPrerequisite.brokePrerequisite(hand);
                }
                else
                {
                    return true;
                }
            }            
            return _prerequisiteMet;
        }
        void castSpell()
        {
            Debug.Log("Arcane Missile Gesture Cast");
            assignedSpell.ActivateSpell();
            _prerequisiteMet = false;
        }
    }
}