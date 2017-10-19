/*******************************************************
 * Copyright (C) 2016 Ngan Do - dttngan91@gmail.com
 *******************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;

namespace LeapMotionSimpleControl
{
	public class ClapHand : BehaviorHand
	{

		// Use this for initialization
		protected void Awake ()
		{
			base.Awake ();
			CurrentType = GestureManager.GestureTypes.ClapHand;
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		protected override bool checkConditionGesture ()
		{
			List<Hand> currentList = GetCurrent2Hands ();
			if (currentList != null) {
				Hand leftHand = currentList [0].IsLeft ? currentList [0] : currentList [1];
				Hand rightHand = currentList [0].IsRight ? currentList [0] : currentList [1];
				if (leftHand == null || rightHand == null) {
					Debug.Log ("Please present the correct left hand and right hand");
				} else {
					if (_gestureManager.isOpenFullHand (leftHand) && _gestureManager.isOpenFullHand (rightHand)
					  && _gestureManager.isOppositeDirection (leftHand.PalmNormal, rightHand.PalmNormal,handForwardDegree)
					  && _gestureManager.isOppositeDirection (leftHand.PalmVelocity, rightHand.PalmVelocity,handForwardDegree)
					  && _gestureManager.isHandMoveForward (leftHand,handForwardDegree,smallestVelocity) && _gestureManager.isHandMoveForward (rightHand,handForwardDegree, smallestVelocity)) {
						return true;
					}

				}
			}
			return false;
		}


	}
}
