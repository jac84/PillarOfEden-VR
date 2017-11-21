using UnityEngine;
using System.Collections;
using LeapMotionSimpleControl;
using Leap;
using CustomUtils;

namespace LeapMotionSimpleControl
{
    public class HandsOpenHold : BehaviorHand
    {
        public Spell assignedSpell;
        private PrerequisiteSpell containsPrerequisite = null;
        private bool ResetTimer = false;
        private bool Activated = false;

        // Use this for initialization
        protected void Awake()
        {
            base.Awake();
            CurrentType = GestureManager.GestureTypes.HandsOpenHold;
            containsPrerequisite = GetComponent<PrerequisiteSpell>();
            specificEvent = castSpell;
        }
        private void Start()
        {
            //_gestureManager.TimeBetween2Gestures = 0;
        }
        private void Update()
        {
            if(Activated)
            {
                CheckBreakGesture();
            }
        }

        private void CheckBreakGesture()
        {
            Hand hand = GetSupportedHand();
            if (hand != null)
            {
                if (!_gestureManager.isAllFingersExtended(hand) && Activated)
                {
                    _counterLoading.DelayReset = false;
                    assignedSpell.DeactivateSpell();
                }
            }
        }
        protected override bool checkConditionGesture()
        {
            Hand hand = GetSupportedHand();
            if (hand != null)
            {
                if (_gestureManager.isAllFingersExtended(hand))
                {
                    Activated = true;
                    if (!_counterLoading.DelayReset)
                    {
                        _counterLoading.DelayReset = true;
                    }
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
        private void castSpell()
        {
                Debug.Log("Spell is being cast");
                assignedSpell.ActivateSpell();
        }
    }
}
