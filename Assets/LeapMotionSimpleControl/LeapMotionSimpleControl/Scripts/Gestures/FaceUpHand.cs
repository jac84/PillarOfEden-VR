/*******************************************************
 * Copyright (C) 2016 Ngan Do - dttngan91@gmail.com
 *******************************************************/
using UnityEngine;
using System.Collections;
using Leap;

namespace LeapMotionSimpleControl
{
	public class FaceUpHand : BehaviorHand
	{

		// Use this for initialization
		protected void Awake ()
		{
			base.Awake ();
			CurrentType = GestureManager.GestureTypes.FaceUp;
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		protected override bool checkConditionGesture ()
		{
			Hand hand = GetCurrent1Hand ();
			if (hand != null) {
				if (_gestureManager.isOpenFullHand (hand) && _gestureManager.isStationary (hand,smallestVelocity) && _gestureManager.isPalmNormalSameDirectionWith (hand, Vector3.up,handForwardDegree)) {
					return true;
				}
			}
			return false;
		}
	}
}