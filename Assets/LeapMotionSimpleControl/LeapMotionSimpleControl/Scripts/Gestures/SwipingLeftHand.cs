/*******************************************************
 * Copyright (C) 2016 Ngan Do - dttngan91@gmail.com
 *******************************************************/
using UnityEngine;
using System.Collections;
using Leap;

namespace LeapMotionSimpleControl
{
	public class SwipingLeftHand : BehaviorHand
	{
        public Spell assignedSpell;
        // Use this for initialization
        protected void Awake ()
		{
			base.Awake ();
			CurrentType = GestureManager.GestureTypes.SwipingLeft;
			// add your custom event 
			specificEvent = onSwipeEvent;
            //_gestureManager.TimeBetween2Gestures = 1;
        }

		protected override bool checkConditionGesture ()
		{
			Hand hand = GetSupportedHand ();
			if (hand != null) {
				if (_gestureManager.isOpenFullHand (hand) && _gestureManager.isMoveLeft (hand,deltaVelocity,smallestVelocity)) {
					return true;
				}
			}
			return false;
		}

		void onSwipeEvent(){
            assignedSpell.ActivateSpell();
        }
	}
}