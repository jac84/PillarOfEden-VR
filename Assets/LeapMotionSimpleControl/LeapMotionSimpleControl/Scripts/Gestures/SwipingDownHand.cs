/*******************************************************
 * Copyright (C) 2016 Ngan Do - dttngan91@gmail.com
 *******************************************************/
using UnityEngine;
using System.Collections;
using Leap;

namespace LeapMotionSimpleControl
{
	public class SwipingDownHand : BehaviorHand
	{

		// Use this for initialization
		protected void Awake ()
		{
			base.Awake ();
			CurrentType = GestureManager.GestureTypes.SwipingDown;
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		protected override bool checkConditionGesture ()
		{
			Hand hand = GetCurrent1Hand ();
			if (hand != null) {
				if (_gestureManager.isOpenFullHand (hand) && _gestureManager.isPalmNormalSameDirectionWith (hand, -Vector3.up,handForwardDegree)) {
					return true;
				}
			}
			return false;
		}
	}
}