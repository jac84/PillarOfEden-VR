﻿/*******************************************************
 * Copyright (C) 2016 Ngan Do - dttngan91@gmail.com
 *******************************************************/
using UnityEngine;
using System.Collections;
using Leap;

namespace LeapMotionSimpleControl
{
	public class ThumbDownHand : BehaviorHand
	{

		// Use this for initialization
		protected void Awake ()
		{
			base.Awake ();
			CurrentType = GestureManager.GestureTypes.ThumbDown;
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		protected override bool checkConditionGesture ()
		{
			Hand hand = GetCurrent1Hand ();
			if (hand != null) {
				if (_gestureManager.checkPalmNormalInXZPlane (hand) && _gestureManager.checkFingerCloseToHand (hand,deltaCloseFinger) && _gestureManager.isThumbDirection (hand, -Vector3.up,deltaAngleThumb)) {
					return true;
				}
			}
			return false;
		}
	}
}