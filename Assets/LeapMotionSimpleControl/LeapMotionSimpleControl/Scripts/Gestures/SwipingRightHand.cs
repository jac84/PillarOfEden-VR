/*******************************************************
 * Copyright (C) 2016 Ngan Do - dttngan91@gmail.com
 *******************************************************/
using UnityEngine;
using System.Collections;
using Leap;

namespace LeapMotionSimpleControl
{
	public class SwipingRightHand : BehaviorHand
	{

		// Use this for initialization
		protected void Awake ()
		{
			base.Awake ();
			CurrentType = GestureManager.GestureTypes.SwipingRight;
	
		}
	

		protected override bool checkConditionGesture ()
		{
			Hand hand = GetSupportedHand();
			if (hand != null) {
				if (_gestureManager.isOpenFullHand (hand) && _gestureManager.isMoveRight (hand,deltaVelocity,smallestVelocity)) {
					return true;
				}
			}
			return false;
		}
	}
}