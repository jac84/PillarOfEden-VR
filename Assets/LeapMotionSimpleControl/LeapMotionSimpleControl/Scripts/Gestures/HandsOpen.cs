/*******************************************************
 * Copyright (C) 2016 Ngan Do - dttngan91@gmail.com
 *******************************************************/
using UnityEngine;
using System.Collections;
using Leap;

namespace LeapMotionSimpleControl
{
    public class HandsOpen : BehaviorHand
    {
        public Spell assignedSpell;

        [SerializeField] private PrerequisiteSpell containsPrerequisite = null;

        // Use this for initialization
        protected void Awake()
        {
            base.Awake();
            CurrentType = GestureManager.GestureTypes.HandsOpen;
            containsPrerequisite = GetComponent<PrerequisiteSpell>();
        }

        // Update is called once per frame
        void Update()
        {
        }

        protected override bool checkConditionGesture()
        {
            Hand hand = GetCurrent1Hand();
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
            Hand hand = GetCurrent1Hand();
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
        protected override void castSpell()
        {
            Debug.Log("Arcane Missile Fired!");
            assignedSpell.ActivateSpell();
            _prerequisiteMet = false;
        }
    }
}